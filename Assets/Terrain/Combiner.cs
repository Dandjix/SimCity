using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Combiner : MonoBehaviour
{
    public GameObject[] toFusion;

    public void Combine()
    {
        //MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        List<MeshFilter> meshFilters = new List<MeshFilter>();
        foreach (GameObject go in toFusion)
        {
            meshFilters.Add(go.GetComponentInChildren<MeshFilter>());
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Count];

        int i = 0;
        while (i < meshFilters.Count)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }
        foreach (GameObject gameObject in toFusion)
        {
            Undo.DestroyObjectImmediate(gameObject);
        }

        Mesh mesh = new Mesh();

        Undo.RecordObject(GetComponent<MeshFilter>(), "Combining meshes");
        //Undo.RecordObjects(toFusion, "Combining meshes");

        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        transform.GetComponent<MeshCollider>().sharedMesh = mesh;
        transform.gameObject.SetActive(true);
    }
}
