using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public static City Instance {  get; private set; }

    private string _name;
    public string Name { get { return _name; } set { _name = value; } }
    public new string name { get { return _name; } set { _name = value; } }


    public int iron = 0;

    private void Start()
    {
        Instance = this;
    }
}
