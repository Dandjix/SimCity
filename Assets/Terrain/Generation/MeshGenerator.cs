using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static TerrainMeshData GenerateTerrainMeshData(float[,] heightMap,float heightMultiplier, AnimationCurve heightCurve)
    {
        int xSize = heightMap.GetLength(0);
        int ySize = heightMap.GetLength(1);

        TerrainMeshData terrainMeshData = new TerrainMeshData(xSize, ySize);
        int vertexIndex = 0;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float height = heightCurve.Evaluate(heightMap[x, y])*heightMultiplier;
                terrainMeshData.vertices[vertexIndex] = new Vector3(x, height, y);
                terrainMeshData.uvs[vertexIndex] = new Vector2(x/(float)xSize, y/(float)ySize);
                if(x<xSize-1 && y<ySize-1)
                {
                    terrainMeshData.AddTriangle(vertexIndex+xSize+1, vertexIndex, vertexIndex + 1);
                    terrainMeshData.AddTriangle(vertexIndex+xSize, vertexIndex, vertexIndex +xSize +1);
                }
                vertexIndex++;
            }
        }

        return terrainMeshData;
    }
}


public class TerrainMeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    public  int triangleIndex = 0;

    public TerrainMeshData(int meshWidth,int meshLength)
    {
        vertices = new Vector3[meshLength*meshWidth];
        uvs = new Vector2[meshLength*meshWidth];
        triangles = new int[(meshLength-1)*(meshWidth-1)*6];
    }

    public void AddTriangle(int a,int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}