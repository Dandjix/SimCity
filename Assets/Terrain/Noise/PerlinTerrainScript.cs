using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NoiseTerrainScript : MonoBehaviour
{
    private Terrain terrain;

    [SerializeField] private MapGrid mapGrid;

    [SerializeField] private float height = 20;
    [SerializeField] private float heightOffset = -4;

    public Vector2 perlinOffset = Vector2.zero;
    public float scale = 20;

    private void Start()
    {

        terrain = GetComponent<Terrain>();
        //Debug.Log("in awake : " + terrain);
    }

    private Vector3 GetPosition()
    {
        MapGrid grid = mapGrid;

        Vector3 southWestCorner = grid.getCorner(CardinalDirection.SouthWest, true);

        return new Vector3(southWestCorner.x,heightOffset,southWestCorner.z);
    }

    private Vector2Int GetSize()
    {
        MapGrid grid = mapGrid;

        int terrainLength = grid.DimensionX + grid.Margin * 2;
        int terrainWidth = grid.DimensionY + grid.Margin * 2;

        return new Vector2Int(terrainLength, terrainWidth);
    }

    /// <summary>
    /// resolution can only be a power of 2 + 1. So we generate the terrain making it bigger and then we only do the height for the visible parts.
    /// </summary>
    /// <returns></returns>
    private Vector2Int GetExtendedSize()
    {
        Vector2Int size = GetSize();

        int x = PowerOfTwoShenanigans.higherPowerOf2(size.x)+1;
        int y = PowerOfTwoShenanigans.higherPowerOf2(size.y)+1;

        return new Vector2Int(x, y);
    }

    public void Generate()
    {
        //Debug.Log("generating ...");
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
    }

    private TerrainData GenerateTerrainData(TerrainData terrainData)
    {
        Vector2Int size = GetSize();

        transform.position = GetPosition();

        Vector2Int extendedSize = GetExtendedSize();
        //Debug.Log("extended size : " + extendedSize);
        int resolution = Mathf.Max(extendedSize.x, extendedSize.y);
        terrainData.heightmapResolution = resolution;

        terrainData.SetHeights(0, 0, GenerateHeights());

        terrainData.size = new Vector3(resolution, height, resolution);

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        Vector2Int size = GetSize();
        float[,] heights = new float[size.y+1, size.x+1];
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                heights[i, j] = CalculateHeight(j, i);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        Vector2Int size = GetSize();
        Vector2Int extendedSize = GetExtendedSize();
        float xCoord = ((float)x) / (8) * scale + perlinOffset.x;
        float yCoord = ((float)y) / (8) * scale + perlinOffset.y;
        //i have no clue why i gotta divide but it doesnt work otherwise
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
    
    public void Save(string path)
    {

    }


}
