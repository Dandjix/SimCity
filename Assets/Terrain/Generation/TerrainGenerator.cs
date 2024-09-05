using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TerrainColorGenerator))]
[RequireComponent (typeof(TerrainDisplay))]
public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private TerrainColorGenerator painter;

    [SerializeField] private TerrainDisplay display;


    public void Generate(int seed, float scale,int octaves, float persistence, float lacunarity, Vector2 offset, Vector2Int size,Material material)
    {
        var heights = Noise.GenerateHeights(size.x, size.y, seed, scale, octaves, persistence, lacunarity, offset);

        Texture2D texture = painter.GenerateTexture(heights,size.x+1,size.y+1);
        TerrainMeshData meshData = MeshGenerator.GenerateTerrainMeshData(heights);

        display.DrawMesh(meshData, texture, material);
    }


}
