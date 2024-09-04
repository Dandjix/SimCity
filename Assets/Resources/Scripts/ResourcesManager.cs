using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance { get; private set; }
    /// <summary>
    /// the amount of resources the player has
    /// </summary>
    public Dictionary<Resource,int> Resources = new Dictionary<Resource,int>();

    private void Awake()
    {
        Resources = new Dictionary<Resource, int>();
        foreach (Resource resource in Enum.GetValues(typeof(Resource)))
        {
            Resources.Add(resource, 0);
        }
        Instance = this;
    }

    void Start()
    {
        Resources[Resource.Credits] = 1000;
        Resources[Resource.Uranium] = 100;
    }
}
