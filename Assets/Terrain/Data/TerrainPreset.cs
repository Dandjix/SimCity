using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

[CreateAssetMenu(menuName ="Terrain/TerrainPreset")]
public class TerrainPreset : UpdatableTerrainData
{
    public TerrainGenerationData terrainGenerationData;
    public ClutterData clutterData;
    public ColorData colorData;

    //private void OnValidate()
    //{
    //    if(TerrainManager.Instance.preset == this)
    //    {
    //        TerrainManager.Instance.Generate();
    //    }
    //}

}
