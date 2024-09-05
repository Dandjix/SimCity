using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureProcessing
{
    public static Texture2D TextureFromColorMap(Color[] colorMap,int length, int width)
    {
        Texture2D texture = new Texture2D(length, width);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
