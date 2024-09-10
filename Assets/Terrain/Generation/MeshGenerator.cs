using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static TerrainMeshData GenerateTerrainMeshData(float[,] heightMap)
    {
        int xSize = heightMap.GetLength(0);
        int ySize = heightMap.GetLength(1);

        TerrainMeshData terrainMeshData = new TerrainMeshData(xSize, ySize);
        int vertexIndex = 0;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                //float height = heightCurve.Evaluate(heightMap[x, y])*heightMultiplier;
                float height = heightMap[x, y];
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

    public int triangleIndex = 0;

    public TerrainMeshData(int meshWidth, int meshLength)
    {
        vertices = new Vector3[meshLength * meshWidth];
        uvs = new Vector2[meshLength * meshWidth];
        triangles = new int[(meshLength - 1) * (meshWidth - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.normals = CalculateNormals();
        return mesh;
    }

    private Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length];
        int triangleCount = triangles.Length / 3;
        for (int i = 0; i < triangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            int vertexIndexA = triangles[normalTriangleIndex];
            int vertexIndexB = triangles[normalTriangleIndex + 1];
            int vertexIndexC = triangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
            vertexNormals[vertexIndexA] += triangleNormal;
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    private Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = vertices[indexA];
        Vector3 pointB = vertices[indexB];
        Vector3 pointC = vertices[indexC];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC).normalized;
    }
}