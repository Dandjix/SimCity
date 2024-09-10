using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static TerrainMeshData GenerateTerrainMeshData(float[,] heightMap)
    {
        int borderedSize = heightMap.GetLength(0);
        //int borderedSize = heightMap.GetLength(1);
        int meshSize = borderedSize - 2;

        //float topLeftX = (meshSize-1)/-2f;
        //float topLeftZ = (meshSize - 1) / 2f;

        TerrainMeshData terrainMeshData = new TerrainMeshData(meshSize);

        int[,] vertexIndicesMap = new int[borderedSize,borderedSize];
        int meshVertexIndex = 0;
        int borderedVertexIndex = -1;

        for (int x = 0; x < borderedSize; x++)
        {
            for (int y = 0; y < borderedSize; y++)
            {
                bool isBorderVertex = y == 0 || y == borderedSize - 1 || x == 0 || x == borderedSize - 1;

                if (isBorderVertex)
                {
                    vertexIndicesMap[x, y] = borderedVertexIndex;
                    borderedVertexIndex--;
                }
                else
                {
                    vertexIndicesMap[x,y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }
        }


        for (int x = 0; x < borderedSize; x++)
        {
            for (int y = 0; y < borderedSize; y++)
            {
                int vertexIndex = vertexIndicesMap[x, y];
                //float height = heightCurve.Evaluate(heightMap[x, y])*heightMultiplier;

                //terrainMeshData.vertices[vertexIndex] = new Vector3(x, height, y);

                Vector2 percent = new Vector2((x - 1) / (float)meshSize, (y - 1) / (float)meshSize);

                float height = heightMap[x, y];

                //Vector3 vertexPosition = new Vector3(x, height, y);
                Vector3 vertexPosition = new Vector3(percent.x*meshSize, height, percent.y*meshSize);
                //terrainMeshData.uvs[vertexIndex] = new Vector2(x/(float)borderedSize, y/(float)borderedSize);
                terrainMeshData.AddVertex(vertexPosition,percent,vertexIndex);

                if (x<borderedSize-1 && y<borderedSize-1)
                {
                    int a = vertexIndicesMap[x, y];
                    int b = vertexIndicesMap[x+1, y];
                    int c = vertexIndicesMap[x, y+1];
                    int d = vertexIndicesMap[x+1, y+1];


                    terrainMeshData.AddTriangle(a,c, d);
                    terrainMeshData.AddTriangle(d,b, a);
                }
                vertexIndex++;
            }
        }

        return terrainMeshData;
    }
}


public class TerrainMeshData
{
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    Vector3[] borderVertices;
    int[] borderTriangles;

    int triangleIndex = 0;
    int borderTriangleIndex = 0;

    public TerrainMeshData(int verticesPerLine)
    {
        vertices = new Vector3[verticesPerLine * verticesPerLine];
        uvs = new Vector2[verticesPerLine * verticesPerLine];
        triangles = new int[(verticesPerLine - 1) * (verticesPerLine - 1) * 6];

        borderVertices = new Vector3[verticesPerLine * 4 + 4];
        borderTriangles = new int[24 * verticesPerLine];
    }

    public void AddVertex(Vector3 vertexPosition, Vector2 uv, int vertexIndex)
    {
        if(vertexIndex < 0)//border vertex
        {
            borderVertices[-vertexIndex - 1] = vertexPosition;
        }
        else
        {
            vertices[vertexIndex] = vertexPosition;
            uvs[vertexIndex] = uv;
        }
    }

    public void AddTriangle(int a, int b, int c)
    {
        if (a < 0 || b < 0 || c < 0)
        {
            borderTriangles[borderTriangleIndex] = a;
            borderTriangles[borderTriangleIndex + 1] = b;
            borderTriangles[borderTriangleIndex + 2] = c;
            borderTriangleIndex += 3;
        }
        else
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

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
        int borderTriangleCount = borderTriangles.Length / 3;
        for (int i = 0; i < borderTriangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            int vertexIndexA = borderTriangles[normalTriangleIndex];
            int vertexIndexB = borderTriangles[normalTriangleIndex + 1];
            int vertexIndexC = borderTriangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
            if(vertexIndexA>=0)
            {
                vertexNormals[vertexIndexA] += triangleNormal;
            }
            if(vertexIndexB>=0)
            {
                vertexNormals[vertexIndexB] += triangleNormal;
            }
            if(vertexIndexC>=0)
            {
                vertexNormals[vertexIndexC] += triangleNormal;
            }
            



        }

        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    private Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = (indexA<0)?borderVertices[-indexA-1] : vertices[indexA];
        Vector3 pointB = (indexB < 0) ? borderVertices[-indexB - 1] : vertices[indexB];
        Vector3 pointC = (indexC < 0) ? borderVertices[-indexC - 1] : vertices[indexC];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC).normalized;
    }
}