using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Noise
{
    //private static readonly Vector2 perlinOffset = Vector2.zero;
    //private static readonly float scale = 20f;

    public static float[,] GenerateHeights(int sizeX, int sizeY, int seed, float scale, int octaves ,float persistence,float lacunarity,Vector2 offset)
    {
        //Vector2Int size = GetSize();
        float[,] heights = new float[sizeY + 1, sizeX + 1];

        Vector2[] offsetByOctave = new Vector2[octaves];
        System.Random prng = new System.Random(seed); 

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            offsetByOctave[i] = new Vector2(offsetX, offsetY);
        }

        for (int x = 0; x < sizeY+1; x++)
        {
            for (int y = 0; y < sizeX+1; y++)
            {
                float height = 0;
                float amplitude = 1;
                float frequency = 1;
                for (int i = 0; i < octaves; i++)
                {
                    float xCoord = ((float)x) / scale * frequency + offsetByOctave[i].x;

                    float yCoord = ((float)y) / scale * frequency + offsetByOctave[i].y;

                    float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                    height += perlinValue*amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                heights[x, y] = height;
            }
        }
        float endAmplitude = EndAmplitude(octaves,persistence);

        float min = 1-endAmplitude;
        float max = endAmplitude;


        for (int x = 0; x < sizeY+1; x++)
        {
            for (int y = 0; y < sizeX+1; y++)
            {
                heights[x, y] = Mathf.InverseLerp(min, max, heights[x,y]);
            }
        }
        return heights;
    }

    private static float EndAmplitude(int octaves,float persistence)
    {
        float amplitude = Mathf.Pow(persistence,octaves+1);
        return amplitude;
    }
}
