using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TerrainPainter))]
public class TerrainGenerator : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private TerrainPainter painter;

    [SerializeField] private MapGrid mapGrid;

    [SerializeField] int seed;

    [SerializeField] private float height = 20;
    [SerializeField] private float heightOffset = -4;

    [Min(1)] [SerializeField] private float scale = 20;

    [SerializeField] private int octaves;
    [Range(0,1)][SerializeField] private float persistence;
    [Min(1)][SerializeField] private float lacunarity;

    private TerrainDisplay display;

    public Vector2 offset;

    public bool autoUpdate = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        painter = GetComponent<TerrainPainter>();
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

    private void GenerateTerrainData()
    {
        Vector2Int size = GetSize();

        transform.position = GetPosition();

        var heights = Noise.GenerateHeights(size.x, size.y, seed, scale, octaves, persistence, lacunarity, offset);

        painter.Color(heights,size.x,size.y);
    }


}
