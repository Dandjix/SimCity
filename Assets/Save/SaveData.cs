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
    public int gridDimensionX;
    public int gridDimensionY;
    public int margin;

    public SaveData(string cityName, int playTime_ms,
        string terrainheightsBinary,int gridDimensionX,int gridDimensionY,int margin)
    {
        this.cityName = cityName;
        this.playTime_ms = playTime_ms;

        this.terrainHeightsBinary = terrainheightsBinary;
        this.gridDimensionX = gridDimensionX;
        this.gridDimensionY = gridDimensionY;
        this.margin = margin;
    }
}
