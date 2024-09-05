using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainTiler : MonoBehaviour
{
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

    [SerializeField] private GeneratorData[] generatorsData = new GeneratorData[0];
    //public Vector2 offset;

    [SerializeField] private Material baseMaterial;

    public bool autoUpdate = false;

    public void Generate()
    {
        Debug.Log("generating ...");



        Vector3 BLCorner = mapGrid.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = mapGrid.getCorner(CardinalDirection.NorthEast, true);

        int totalX = (int)(BLCorner.x - TRCorner.x);
        int totalY = (int)(BLCorner.y - TRCorner.y);

        //int numberOnX = (int)Mathf.Ceil((float)totalX / chunkSizeX);
        //int numberOnY = (int)Mathf.Ceil((float)totalY / chunkSizeY);

        int numberOnX = 4;
        int numberOnY = 4;

        foreach (var generator in generatorsData)
        {
            DestroyImmediate(generator.generator);
            DestroyImmediate(generator.material);
        }
        generatorsData = new GeneratorData[numberOnX * numberOnY];

        Debug.Log("numbers : "+numberOnX+" , "+numberOnY);

        for (int i = 0; i < numberOnX; i++)
        {
            for (int j = 0; j < numberOnY; j++)
            {
                Vector3 position = new Vector3(BLCorner.x + i * chunkSizeX,heightOffset, BLCorner.z + j * chunkSizeY);
                Vector2 offset = new Vector2(position.x,position.z);
                GameObject generator = Instantiate( Resources.Load<GameObject>("Terrain/TerrainChunk"));
                generator.transform.parent = transform;
                generator.transform.position = position;


                Material material = new Material(baseMaterial);

                GeneratorData data = new GeneratorData();
                data.generator = generator;
                data.material = material;

                //generator.GetComponent<TerrainDisplay>().SetMaterial(data.material);
                generatorsData[i * numberOnX + j] = data;
                 

                generator.GetComponent<TerrainGenerator>().Generate(seed,scale,octaves,persistence,lacunarity,offset,new Vector2Int(chunkSizeX,chunkSizeY),material);
            }
        }

        Debug.Log("Done.");
    }
}

[System.Serializable]
struct GeneratorData
{
    public GameObject generator;
    public Material material;
}
