
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Creates terrain and allows to access the heights.
/// </summary>
[ExecuteAlways]
public class TerrainManager : MonoBehaviour
{
    public Action BeforeGenerate;
    public Action AfterGenerate;

    [Min(8)][SerializeField] public int chunkSize = 128;

    public static TerrainManager Instance { get 
        {
            if(instance == null)
                return FindFirstObjectByType<TerrainManager>();
            return instance;
        }
        set
        {
            instance = value;
        } 
    }

    private static TerrainManager instance;

    private void Awake()
    {
        instance = this;

    }

    //private void Start()
    //{
    //    //Debug.Log("generating terrain !");
    //    Generate();
    //}

    public float GetMinHeight()
    {
        return heightCurve.Evaluate(0) * height + heightOffset;
    }

    public float GetMaxHeight()
    {
        return heightCurve.Evaluate(1) * height + heightOffset;
    }

    public TerrainPreset preset;


    public int Seed = 0;

    public float height { get { return preset.terrainGenerationData.height; } }
    public float heightOffset { get { return preset.terrainGenerationData.heightOffset; } }

    public float scale { get { return preset.terrainGenerationData.scale; } }

    public int octaves { get { return preset.terrainGenerationData.octaves; } }
    public float persistence { get { return preset.terrainGenerationData.persistence; } }

    public float lacunarity { get { return preset.terrainGenerationData.lacunarity; } }

    public AnimationCurve heightCurve { get { return preset.terrainGenerationData.heightCurve; } }

    public float maxHeight { get { return preset.terrainGenerationData.maxHeight; } }
    public float minHeight { get { return preset.terrainGenerationData.minHeight; } }

    public Vector2 globalOffset { get { return preset.terrainGenerationData.globalOffset; } }

    private Material baseMaterial { get { return preset.terrainGenerationData.baseMaterial; } }


    //[SerializeField] public int seed;

    //[Min(0)][SerializeField] public float height = 20;
    //[SerializeField] public float heightOffset = -4;

    //[Min(1)][SerializeField] public float scale = 20;

    //[SerializeField] public int octaves;
    //[Range(0, 1)][SerializeField] public float persistence;
    //[Min(1)][SerializeField] public float lacunarity;

    //[SerializeField] public AnimationCurve heightCurve;

    //public float maxHeight = 100;
    //public float minHeight = 0;




    private int chunksOnX, chunksOnY;

    //[SerializeField] private Material baseMaterial;

    //public Vector2 offset;

    [SerializeField][HideInInspector] private GeneratorData[,] generatorsData = new GeneratorData[0,0];




    public float[,] Heights { get; private set; }

    public float GetHeightAtBottomLeft(Vector2Int square)
    {
        return GetHeightAtBottomLeft(square.x, square.y);
    }

    public float GetHeightAtBottomLeft(int x,int y)
    {
        return Heights[x+MapGrid.Instance.Margin+1, y + MapGrid.Instance.Margin+1];
    }

    public float GetHeightAtCenter(Vector2Int square)
    {
        return GetHeightAtCenter(square.x, square.y);
    }

    public float GetHeightAtCenter(int x,int y)
    {
        //x += MapGrid.Instance.Margin;
        //y += MapGrid.Instance.Margin;

        float h = (GetHeightAtBottomLeft(x, y) + GetHeightAtBottomLeft(x + 1, y) + GetHeightAtBottomLeft(x, y + 1) + GetHeightAtBottomLeft(x + 1, y + 1)) / 4;

        //h = heightCurve.Evaluate(h) * height + heightOffset;
        //h = height;
        //h *= height;
        //h += heightOffset;
        return h;
    }

    public bool autoUpdate = false;
    /// <summary>
    /// Generates a map using Noise and the presets and whatever
    /// </summary>
    public void Generate()
    {
        Vector3 BLCorner = MapGrid.Instance.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = MapGrid.Instance.getCorner(CardinalDirection.NorthEast, true);

        int totalX = (int)(-BLCorner.x + TRCorner.x);
        int totalY = (int)(-BLCorner.z + TRCorner.z);

        int chunksOnX = Mathf.CeilToInt((float)totalX / chunkSize);
        int chunksOnY = Mathf.CeilToInt((float)totalY / chunkSize);

        var heights = Noise.GenerateHeights(
            chunkSize * chunksOnX + 3,
            chunkSize * chunksOnY + 3,
            Seed, scale, octaves, persistence, lacunarity, minHeight, maxHeight, globalOffset,
            heightCurve, heightOffset, height);

        Generate(heights);
    }

    /// <summary>
    /// Generates a map using the provided heights
    /// </summary>
    public void Generate(float[,] heights)
    {
        Vector3 BLCorner = MapGrid.Instance.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = MapGrid.Instance.getCorner(CardinalDirection.NorthEast, true);

        int totalX = (int)(-BLCorner.x + TRCorner.x);
        int totalY = (int)(-BLCorner.z + TRCorner.z);

        chunksOnX = Mathf.CeilToInt((float)totalX / chunkSize);
        chunksOnY = Mathf.CeilToInt((float)totalY / chunkSize);

        //var globalHeights = Noise.GenerateHeights(
        //    chunkSize * chunksOnX + 3,
        //    chunkSize * chunksOnY + 3,
        //    Seed, scale, octaves, persistence, lacunarity, minHeight, maxHeight, globalOffset,
        //    heightCurve, heightOffset, height);

        int desiredX = chunkSize * chunksOnX + 3;
        int desiredY = chunkSize * chunksOnY + 3;


        if (heights.GetLength(0)!= desiredX || heights.GetLength(1)!= desiredY)
        {
            throw new ArgumentException("the heights provided have dimensions : (" + heights.GetLength(0) + "," + heights.GetLength(1) + ") where they should have dimensions : (" + desiredX + "," + desiredY + ").");
        }

        BeforeGenerate?.Invoke();

        Clear();





        generatorsData = new GeneratorData[chunksOnX,chunksOnY];

        Heights = heights;

        //Debug.Log("lengths : " + globalHeights.GetLength(0) + ", " + globalHeights.GetLength(1));

        for (int i = 0; i < chunksOnX; i++)
        {
            for (int j = 0; j < chunksOnY; j++)
            {
                GenerateChunk(i, j);
            }
        }
        //Debug.Log("calling action !");
        AfterGenerate?.Invoke();
    }

    public void Clear()
    {
        for (int i = 0; i < generatorsData.GetLength(0); i++)
        {
            for (int j = 0; j < generatorsData.GetLength(1); j++)
            {
                var generator = generatorsData[i, j];
                if (generator.generator == null)
                {
                    //Debug.Log("generator is null, ignoring");
                    continue;
                }
                DestroyImmediate(generator.generator.gameObject);
                DestroyImmediate(generator.material);
            }
        }

        //destroy all children
        List<Transform> generators = new List<Transform>();
        foreach (Transform generator in transform)
        {
            generators.Add(generator);
        }
        foreach (Transform generator in generators)
        {
            DestroyImmediate(generator.gameObject);
        }
    }

    private HashSet<Vector2Int> changedChunks = new HashSet<Vector2Int>();

    private void GenerateChunk(int generatorX,int generatorY)
    {
        Vector3 BLCorner = MapGrid.Instance.getCorner(CardinalDirection.SouthWest, true);

        Vector3 position = new Vector3(BLCorner.x + generatorX * chunkSize, 0, BLCorner.z + generatorY * chunkSize);
        //Vector2 offset = new Vector2((position.z),(position.x));


        //generator.layer = LayerMask.NameToLayer("Terrain");
        //generator.transform.localScale = new Vector3(1,height,1);

        GeneratorData data;
        data = generatorsData[generatorX, generatorY];
        Material material;
        GameObject generator;
        if (data.generator == null)
        {
            material = new Material(baseMaterial);
            generator = Instantiate(Resources.Load<GameObject>("Terrain/TerrainChunk"));
            data.generator = generator;
            data.material = material;
        }
        else
        {
            material = data.material;
            generator = data.generator;
        }

        generator.transform.parent = transform;
        generator.transform.position = position;

        generatorsData[generatorX,generatorY] = data;

        float[,] heightsForChunk = new float[chunkSize + 3, chunkSize + 3];



        for (int x = 0; x < heightsForChunk.GetLength(0); x++)
        {
            for (int y = 0; y < heightsForChunk.GetLength(1); y++)
            {
                heightsForChunk[x, y] = Heights[generatorX * (chunkSize) + x, generatorY * (chunkSize) + y];
            }
        }


        generator.GetComponent<TerrainGenerator>().Generate(
            heightsForChunk,
            material,
            preset.colorData);
    }

    /// <summary>
    /// Sets the height for the bottom left corner vertice of that position. You must call ApplyHeights to regenerate the terrain chunks changed.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="height"></param>
    public void SetHeight(Vector2Int position, float height)
    {
        SetHeight(position.x,position.y, height);
    }

    /// <summary>
    /// Sets the height for the bottom left corner vertice of that position. You must call ApplyHeights to regenerate the terrain chunks changed.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="height"></param>
    public void SetHeight(int x, int y, float height)
    {
        x += MapGrid.Instance.Margin;
        y += MapGrid.Instance.Margin;

        Heights[x+1,y+1] = height;

        int fromX = 0, toX = 0;
        int fromY = 0, toY = 0;

        if ((x) % chunkSize == 0 && x>0)
        {
            fromX = -1;
            //Debug.Log("on the x- limit");
        }
        if ((y) % chunkSize == 0 && y>0)
        {
            fromY = -1;
            //Debug.Log("on the y- limit");

        }


        if ((x+1)%chunkSize == 0 && x<(chunksOnX-1))
        {
            toX = 1;
            //Debug.Log("on the x+ limit");
        }
        if ((y+1) % chunkSize == 0 && y<(chunksOnY-1))
        {
            toY = 1;
            //Debug.Log("on the y+ limit");
        }



        int chunkX = x /chunkSize;
        int chunkY = y /chunkSize;

        for (int i = fromX; i <= toX; i++)
        {
            for (int j = fromY; j <= toY; j++)
            {
                //Debug.Log("regen at " + (chunkX + i) + ", " + (chunkY + j));
                //GenerateChunk(chunkX+i, chunkY+j);
                changedChunks.Add(new Vector2Int(chunkX + i, chunkY + j));
            }
        }
    }

    public void ApplyHeights()
    {
        foreach (var coordinates in changedChunks)
        {
            GenerateChunk(coordinates.x, coordinates.y);
        }
        changedChunks.Clear();
    }
}

[System.Serializable]
struct GeneratorData
{
    public GameObject generator;
    public Material material;
}
