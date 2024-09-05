using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static TerrainMeshData GenerateTerrainMeshData(float[,] heightMap,float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int length = heightMap.GetLength(1);

        TerrainMeshData terrainMeshData = new TerrainMeshData(length, width);
        int vertexIndex = 0;
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                float height = heightCurve.Evaluate(heightMap[y, x])*heightMultiplier;
                terrainMeshData.vertices[vertexIndex] = new Vector3(x, height, y);
                terrainMeshData.uvs[vertexIndex] = new Vector2(x/(float)length, y/(float)length);
                if(x<length-1 && y<width-1)
                {
                    terrainMeshData.AddTriangle(vertexIndex,vertexIndex+length+1, vertexIndex + 1);
                    terrainMeshData.AddTriangle(vertexIndex,vertexIndex+length,vertexIndex+length+1);
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