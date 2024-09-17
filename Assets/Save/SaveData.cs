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
}
