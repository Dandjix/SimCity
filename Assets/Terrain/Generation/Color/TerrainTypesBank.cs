using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainColorGenerator;

[ExecuteAlways]
public class TerrainTypesBank : MonoBehaviour
{
    private static TerrainTypesBank instance;
    public static TerrainTypesBank Instance { get
        {
            if (instance == null)
            {
                var bank = GameObject.FindFirstObjectByType<TerrainTypesBank>();
                return bank;
            }
            return instance;
        }
        private set 
        { 
            instance = value;
        } 
    }



    void Start()
    {
        Instance = this;    
    }
}
