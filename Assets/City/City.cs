using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public static City Instance {  get; private set; }

    private new string name;
    public string Name { get { return name; } set { name = value; } }

    public int iron = 0;

    private void Start()
    {
        Instance = this;
    }
}
