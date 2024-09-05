using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainColorGenerator;

[ExecuteAlways]
public class TerrainTypesBank : MonoBehaviour
{
    public static TerrainTypesBank Instance { get; private set; }

    [SerializeField] private TerrainType[] terrainTypes;
    public TerrainType[] TerrainTypes { get { return terrainTypes; } }

    void Start()
    {
        Instance = this;    
    }

}
