using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public static City Instance {  get; private set; }

    public string Name;

    public int iron = 0;

    private void Start()
    {
        Instance = this;
    }
}
