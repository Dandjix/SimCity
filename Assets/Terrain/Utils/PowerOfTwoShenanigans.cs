using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerOfTwoShenanigans
{
    public static int higherPowerOf2(int x)
    {
        x--;
        int power = 0;
        while (x > 0)
        {
            x = x / 2;
            //if (x > 0)
                power++;
        }
        return (int)Mathf.Pow(2,power);
    }
}
