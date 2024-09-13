using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float[,] terrainVertices;

    public GameData(float[,] terrainVertices)
    {
        this.terrainVertices = terrainVertices;
    }
}
