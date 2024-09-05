using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainColorGenerator : MonoBehaviour
{
    public void CreateMesh()
    {

    }

    public Texture2D GenerateTexture(float[,] heights,int width,int length)
    {
        Color[] colorMap = new Color[width * length];

        var terrainTypes = TerrainTypesBank.Instance.TerrainTypes;

        System.Array.Sort(terrainTypes, (a, b) => a.height.CompareTo(b.height));

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int i = 0; i < terrainTypes.Length; i++)
                {
                    if (heights[x, y] <= terrainTypes[i].height)
                    {
                        Color color = terrainTypes[i].color;
                        //Debug.Log("x : " + x + "l 0 : " + heights.GetLength(0));
                        //Debug.Log("y : " + y + "l 1 : " + heights.GetLength(1));
                        //Debug.Log("cmap : " + colorMap.Length);
                        colorMap[x * width + y] = color;
                        break;
                    }
                }
            }
        }

        Texture2D texture = TextureProcessing.TextureFromColorMap(colorMap,width,length);
        //Debug.Log("texture : " + texture);
        return texture;
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        [Range(0, 1)] public float height;
        public Color color;
    }
}
