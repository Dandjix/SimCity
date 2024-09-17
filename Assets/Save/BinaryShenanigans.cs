using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BinaryShenanigans
{
    public static byte[] FloatDoubleArrayToBinary(float[,] data)
    {
        byte[] result;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        writer.Write(data[i, j]);
                    }
                }
            }
            result = memoryStream.ToArray();
        }
        return result;
    }

    private const int sizeOfFloat = 4;

    public static float[,] BinaryToDoubleFloatArray(byte[] data, int dimensionX,int dimensionY)
    {
        if(dimensionX*dimensionY*4 != data.Length)
        {
            throw new System.ArgumentException("data size does not match a float[" + dimensionX + "," + dimensionY + "]");
        }

        int bytesIndex = 0;

        float[,] result = new float[dimensionX,dimensionY];
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                result[i,j] = System.BitConverter.ToSingle(data, bytesIndex);
                bytesIndex += 4;
            }
        }

        return result;
    }
}
