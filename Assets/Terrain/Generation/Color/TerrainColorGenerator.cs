using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainColorGenerator : MonoBehaviour
{
    //public void CreateMesh()
    //{

    //}

    public Texture2D GenerateTexture(float[,] heights,int xSize,int ySize)
    {
        Color[] colorMap = new Color[xSize * ySize];

        var terrainTypes = TerrainTypesBank.Instance.TerrainTypes;

        System.Array.Sort(terrainTypes, (a, b) => a.height.CompareTo(b.height));

        for (int x = 0; x < xSize-1; x++)
        {
            for (int y = 0; y < ySize-1; y++)
            {
                float height = (heights[x, y] + heights[x+1, y] + heights[x, y+1] + heights[x+1, y+1])/4;
                for (int i = 0; i < terrainTypes.Length; i++)
                {
                    if (height <= terrainTypes[i].height)
                    {
                        Color color = terrainTypes[i].color;
                        //Debug.Log("x : " + x + "l 0 : " + heights.GetLength(0));
                        //Debug.Log("y : " + y + "l 1 : " + heights.GetLength(1));
                        //Debug.Log("cmap : " + colorMap.Length);
                        colorMap[y * ySize + x] = color;
                        break;
                    }
                }
            }
        }

        Texture2D texture = TextureProcessing.TextureFromColorMap(colorMap,xSize,ySize);
        //Debug.Log("texture : " + texture);
        return texture;
    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
