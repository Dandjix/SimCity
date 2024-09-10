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
        Color[] colorMap = new Color[(xSize-2) * (ySize-2)];

        var terrainTypes = TerrainTypesBank.Instance.TerrainTypes;

        System.Array.Sort(terrainTypes, (a, b) => a.height.CompareTo(b.height));

        for (int x = 0; x < xSize-2; x++)
        {
            for (int y = 0; y < ySize-2; y++)
            {
                //try
                //{
                    //try
                    //{
                        //height = heights[x + 1, y + 1];

                    float height = (heights[x+1, y+1] + heights[x+2, y+1] + heights[x+1, y+2] + heights[x+2, y+2])/4;
                    //}
                    //catch(System.Exception e)
                    //{
                    //    height = 0;
                    //}

                    for (int i = 0; i < terrainTypes.Length; i++)
                    {
                        if (height <= terrainTypes[i].height)
                        {
                            Color color = terrainTypes[i].color;
                            //Debug.Log("x : " + x + "l 0 : " + heights.GetLength(0));
                            //Debug.Log("y : " + y + "l 1 : " + heights.GetLength(1));
                            //Debug.Log("cmap : " + colorMap.Length);
                            colorMap[y * (ySize-2) + x] = color;
                            break;
                        }
                    }
                //}
                //catch(System.Exception e)
                //{
                //    colorMap[y * ySize + x] = new Color(0,0,0);
                //}

            }
        }

        Texture2D texture = TextureProcessing.TextureFromColorMap(colorMap,xSize-2,ySize-2);
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
