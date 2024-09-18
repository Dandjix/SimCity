using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// dimensionX and dimensionY are not the actual map size, they are just the dimensions of the heights data array for unpacking
/// </summary>
[System.Serializable]
public class SaveData
{
    public string cityName;
    public int playTime_ms;

    public string terrainHeightsBinary;
    public int terrainheightsX;
    public int terrainheightsY;

    public int gridDimensionX;
    public int gridDimensionY;
    public int gridMargin;

    public SaveData(
        string cityName, int playTime_ms,

        string terrainheightsBinary,int terrainheightsX,int terrainheightsY,
        
        int gridDimensionX,int gridDimensionY,int gridMargin)
    {
        this.cityName = cityName;
        this.playTime_ms = playTime_ms;

        this.terrainHeightsBinary = terrainheightsBinary;
        this.terrainheightsX = terrainheightsX;
        this.terrainheightsY = terrainheightsY;

        this.gridDimensionX = gridDimensionX;
        this.gridDimensionY = gridDimensionY;
        this.gridMargin = gridMargin;
    }
}
