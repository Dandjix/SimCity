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


    public void Generate(float[,] heights,
        float heightMultiplier,
        AnimationCurve heightCurve,
        Material material)
    {
        //var heights = Noise.GenerateHeights(size.x, size.y, seed, scale, octaves, persistence, lacunarity,minHeight,maxHeight, offset);

        Texture2D texture = painter.GenerateTexture(heights, heights.GetLength(0), heights.GetLength(1));
        TerrainMeshData meshData = MeshGenerator.GenerateTerrainMeshData(heights, heightMultiplier, heightCurve);

        display.DrawMesh(meshData, texture, material);
    }


}
