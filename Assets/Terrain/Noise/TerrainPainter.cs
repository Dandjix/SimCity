using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainPainter : MonoBehaviour
{
    [SerializeField] private TerrainType[] terrainTypes;

    public void Color(float[,] heights,int width,int length)
    {
        Color[] colorMap = new Color[width * length];


        System.Array.Sort(terrainTypes, (a, b) => a.height.CompareTo(b.height));

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                for (int i = 0; i < terrainTypes.Length; i++)
                {
                    if (heights[x, y] <= terrainTypes[i].height)
                    {
                        colorMap[x * heights.GetLength(1) + y] = terrainTypes[i].color;
                        break;
                    }
                }
            }
        }

        Texture2D texture = TextureProcessing.TextureFromColorMap(colorMap,width,length);

        //GetComponent<Renderer>().material.mainTexture = texture;

    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        [Range(0, 1)] public float height;
        public Color color;
    }
}
