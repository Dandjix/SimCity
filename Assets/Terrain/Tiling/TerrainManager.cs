using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// Creates terrain and allows to access the heights.
/// </summary>
[ExecuteAlways]
public class TerrainManager : MonoBehaviour
{
    public static TerrainManager Instance { get 
        {
            if(instance == null)
                return Object.FindFirstObjectByType<TerrainManager>();
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



    [SerializeField] int chunkSizeX = 128;
    [SerializeField] int chunkSizeY = 128;

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


    [SerializeField] private GeneratorData[] generatorsData = new GeneratorData[0];
    //public Vector2 offset;

    [SerializeField] private Material baseMaterial;

    public float[,] Heights { get; private set; }

    public float GetHeightAtCenter(Vector2Int square)
    {
        return GetHeightAtCenter(square.x,square.y);
    }


    public float GetHeightAtCenter(int x,int y)
    {
        x += mapGrid.Margin;
        y += mapGrid.Margin;

        float h = (Heights[x, y] + Heights[x + 1, y] + Heights[x, y + 1] + Heights[x + 1, y + 1]) / 4;

        h = heightCurve.Evaluate(h) * height + heightOffset;

        //h *= height;
        //h += heightOffset;
        return h;
    }

    public bool autoUpdate = false;

    public void Generate()
    {
        //Debug.Log("generating ...");



        Vector3 BLCorner = mapGrid.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = mapGrid.getCorner(CardinalDirection.NorthEast, true);

        //Debug.Log("BLC : "+BLCorner);
        //Debug.Log("TRCorner : " + TRCorner);

        int totalX = (int)(- BLCorner.x + TRCorner.x);
        int totalY = (int)(- BLCorner.z + TRCorner.z);

        int numberOnX = (int)Mathf.Ceil((float)totalX / chunkSizeX);
        int numberOnY = (int)Mathf.Ceil((float)totalY / chunkSizeY);

        //int numberOnX = 4;
        //int numberOnY = 4;

        foreach (var generator in generatorsData)
        {
            DestroyImmediate(generator.generator);
            DestroyImmediate(generator.material);
        }

        foreach(Transform generator in transform)
        {
            DestroyImmediate(generator.gameObject);
        }
        generatorsData = new GeneratorData[numberOnX * numberOnY];

        //Debug.Log("numbers : "+numberOnX+" , "+numberOnY);

        var globalHeights = Noise.GenerateHeights(chunkSizeX*numberOnX+1, chunkSizeY*numberOnY+1, seed, scale, octaves, persistence, lacunarity, minHeight, maxHeight, globalOffset);

        Heights = globalHeights;

        //Debug.Log("lengths : " + globalHeights.GetLength(0) + ", " + globalHeights.GetLength(1));

        for (int i = 0; i < numberOnX; i++)
        {
            for (int j = 0; j < numberOnY; j++)
            {
                Vector3 position = new Vector3(BLCorner.x + i * chunkSizeX,heightOffset, BLCorner.z + j * chunkSizeY);
                //Vector2 offset = new Vector2((position.z),(position.x));
                GameObject generator = Instantiate( Resources.Load<GameObject>("Terrain/TerrainChunk"));
                generator.transform.parent = transform;
                generator.transform.position = position;
                //generator.layer = LayerMask.NameToLayer("Terrain");
                //generator.transform.localScale = new Vector3(1,height,1);


                Material material = new Material(baseMaterial);

                GeneratorData data = new GeneratorData();
                data.generator = generator;
                data.material = material;

                //generator.GetComponent<TerrainDisplay>().SetMaterial(data.material);
                generatorsData[j * numberOnX + i] = data;

                float[,] heightsForChunk = new float[chunkSizeX+1, chunkSizeY+1];



                for (int x = 0; x < chunkSizeX+1; x++)
                {
                    for (int y = 0; y < chunkSizeY+1; y++)
                    {
                        heightsForChunk[x,y] = globalHeights[i*chunkSizeX +x,j*chunkSizeY+y];
                    }
                }


                generator.GetComponent<TerrainGenerator>().Generate(
                    heightsForChunk,
                    height,
                    heightCurve,
                    material);
            }
        }

        //Debug.Log("Done.");
    }
}

[System.Serializable]
struct GeneratorData
{
    public GameObject generator;
    public Material material;
}
