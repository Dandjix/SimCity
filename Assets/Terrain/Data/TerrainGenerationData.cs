using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Terrain/TerrainGenerationData")]
public class TerrainGenerationData : UpdatableTerrainData
{
    [Min(8)][SerializeField] public int chunkSize = 128;

    [SerializeField] public int seed;

    [Min(0)][SerializeField] public float height = 20;
    [SerializeField] public float heightOffset = -4;

    [Min(1)][SerializeField] public float scale = 20;

    [SerializeField] public int octaves;
    [Range(0, 1)][SerializeField] public float persistence;
    [Min(1)][SerializeField] public float lacunarity;

    [SerializeField] public AnimationCurve heightCurve;

    public float maxHeight = 100;
    public float minHeight = 0;


    public Vector2 globalOffset = Vector2.zero;

    [SerializeField] public Material baseMaterial;

    //private void OnValidate()
    //{
    //    if (TerrainManager.Instance.preset?.terrainGenerationData == this)
    //    {
    //        TerrainManager.Instance.Generate();
    //    }
    //}
}
