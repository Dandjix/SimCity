using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveNameManipulation
{
    public static string GetSaveName(string save)
    {
        var split = save.Split('\\');
        string name = split[split.Length - 1].Split('.')[0];
        return name;
    }
}
