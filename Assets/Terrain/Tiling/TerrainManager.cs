using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// Creates terrain and allows to access the heights.
/// </summary>
[ExecuteAlways]
public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get 
        {
            if(instance == null)
                return UnityEngine.Object.FindFirstObjectByType<TerrainManager>();
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
        Generate();
    }



    [Min(8)][SerializeField] int chunkSize = 128;

    [SerializeField] private MapGrid mapGrid;

    [SerializeField] public int seed;

    [Min(0)][SerializeField] public float height = 20;
    [SerializeField] public float heightOffset = -4;

    [Min(1)][SerializeField] public float scale = 20;

    [SerializeField] public int octaves;
    [Range(0, 1)][SerializeField] public float persistence;
    [Min(1)][SerializeField] public float lacunarity;

    [SerializeField] public AnimationCurve heightCurve;

    public float maxHeight = 100;
    public float minHeight = 0;

    public Vector2 globalOffset = Vector2.zero;


    [SerializeField][HideInInspector] private GeneratorData[,] generatorsData = new GeneratorData[0,0];
    //public Vector2 offset;

    [SerializeField] private Material baseMaterial;

    public float[,] Heights { get; private set; }

    public float GetHeightAtCenter(Vector2Int square)
    {
        return GetHeightAtCenter(square.x,square.y);
    }

    public float GetHeightAtBottomLeft(int x,int y)
    {
        return Heights[x+1,y+1];
    }

    public float GetHeightAtCenter(int x,int y)
    {
        x += mapGrid.Margin;
        y += mapGrid.Margin;

        float h = (GetHeightAtBottomLeft(x, y) + GetHeightAtBottomLeft(x + 1, y) + GetHeightAtBottomLeft(x, y + 1) + GetHeightAtBottomLeft(x + 1, y + 1)) / 4;

        //h = heightCurve.Evaluate(h) * height + heightOffset;
        //h = height;
        //h *= height;
        //h += heightOffset;
        return h;
    }

    public bool autoUpdate = false;

    public void Generate()
    {
        Clear();



        Vector3 BLCorner = mapGrid.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = mapGrid.getCorner(CardinalDirection.NorthEast, true);

        int totalX = (int)(- BLCorner.x + TRCorner.x);
        int totalY = (int)(- BLCorner.z + TRCorner.z);

        int numberOnX = (int)Mathf.Ceil((float)totalX / chunkSize);
        int numberOnY = (int)Mathf.Ceil((float)totalY / chunkSize);

        generatorsData = new GeneratorData[numberOnX,numberOnY];

        //Debug.Log("numbers : "+numberOnX+" , "+numberOnY);

        //var globalHeights = Noise.GenerateHeights(chunkSize*numberOnX+1, chunkSize*numberOnY+1, seed, scale, octaves, persistence, lacunarity, minHeight, maxHeight, globalOffset, 
        //    heightCurve, heightOffset, height);

        var globalHeights = Noise.GenerateHeights(
            chunkSize * numberOnX + 3,
            chunkSize * numberOnY + 3,
            seed, scale, octaves, persistence, lacunarity, minHeight, maxHeight, globalOffset,
            heightCurve, heightOffset, height);

        Heights = globalHeights;

        //Debug.Log("lengths : " + globalHeights.GetLength(0) + ", " + globalHeights.GetLength(1));

        for (int i = 0; i < numberOnX; i++)
        {
            for (int j = 0; j < numberOnY; j++)
            {
                GenerateChunk(i, j,BLCorner);
            }
        }

        //Debug.Log("Done.");
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

    private void GenerateChunk(int generatorX,int generatorY,Vector3 BLCorner)
    {
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
            material);
    }

    public void SetHeight(int x, int y, float height)
    {
        Heights[x+1,y+1] = height;
    }
}

[System.Serializable]
struct GeneratorData
{
    public GameObject generator;
    public Material material;
}
