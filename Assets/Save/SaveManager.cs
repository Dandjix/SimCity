using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {  get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void Save(string path)
    {

    }

    public void Load(string path) 
    { 
    
    }
}
