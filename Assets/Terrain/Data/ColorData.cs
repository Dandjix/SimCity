using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainColorGenerator;

[CreateAssetMenu(menuName = "Terrain/ColorData")]
public class ColorData : UpdatableTerrainData
{
    [SerializeField] private TerrainType[] terrainTypes = new TerrainType[0];
    public TerrainType[] TerrainTypes { get { return terrainTypes; } }

    private void OnValidate()
    {
        System.Array.Sort(terrainTypes, (TerrainType a, TerrainType b) => a.height.CompareTo(b.height));
    }
}
