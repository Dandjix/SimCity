using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkBuilding : MonoBehaviour
{
    public abstract Dictionary<CivilianType, int> MaxWorkers();

    public Dictionary<CivilianType,int> workers { get; private set; }

    private void Awake()
    {
        workers = new Dictionary<CivilianType, int>();
        foreach(CivilianType civilianType in Enum.GetValues(typeof(CivilianType)))
        {
            workers.Add(civilianType, 0);
        }
    }
}
