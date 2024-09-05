using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainTiler : MonoBehaviour
{
    [SerializeField] int chunkSizeX = 128;
    [SerializeField] int chunkSizeY = 128;

    [SerializeField] public MapGrid mapGrid;

    [SerializeField] public int seed;

    [Min(0)][SerializeField] public float height = 20;
    [SerializeField] public float heightOffset = -4;

    [Min(1)][SerializeField] public float scale = 20;

    [SerializeField] public int octaves;
    [Range(0, 1)][SerializeField] public float persistence;
    [Min(1)][SerializeField] public float lacunarity;

    [SerializeField] private GameObject[] generators = new GameObject[0];
    //public Vector2 offset;

    public bool autoUpdate = false;

    public void Generate()
    {
        Debug.Log("generating ...");



        Vector3 BLCorner = MapGrid.Instance.getCorner(CardinalDirection.SouthWest, true);
        Vector3 TRCorner = MapGrid.Instance.getCorner(CardinalDirection.NorthEast, true);

        int totalX = (int)(BLCorner.x - TRCorner.x);
        int totalY = (int)(BLCorner.y - TRCorner.y);

        //int numberOnX = (int)Mathf.Ceil((float)totalX / chunkSizeX);
        //int numberOnY = (int)Mathf.Ceil((float)totalY / chunkSizeY);

        int numberOnX = 4;
        int numberOnY = 4;

        foreach (var generator in generators)
        {
            DestroyImmediate(generator);
        }
        generators = new GameObject[numberOnX * numberOnY];

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
                generators[i * numberOnX + j] = generator;
                generator.GetComponent<TerrainGenerator>().Generate(seed,scale,octaves,persistence,lacunarity,offset,new Vector2Int(chunkSizeX,chunkSizeY));
            }
        }

        Debug.Log("Done.");
    }
}
