using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
[RequireComponent(typeof (MeshCollider))]
public class TerrainDisplay : MonoBehaviour
{
    private new Renderer renderer;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    //private Material material;

    //public void SetMaterial(Material material)
    //{
    //    meshRenderer.material = material;
    //}

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void DrawMesh(TerrainMeshData meshData,Texture2D texture, Material material)
    {
        var mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        material.mainTexture = texture;
        meshRenderer.sharedMaterial = material;
        //meshRenderer.sharedMaterial.mainTexture = texture;
        meshCollider.sharedMesh = mesh;
    }
}
