using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticSaveDirections
{
    //if you want to create a new city
    public static bool createNew = true;
    public static string cityName;
    public static int seed = 0;
    public static int dimensionX = 500;
    public static int dimensionY = 500;
    public static int margin = 150;

    //if you want to load a save
    public static string savePath = "";
}
