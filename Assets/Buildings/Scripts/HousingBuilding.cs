using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingBuilding : MonoBehaviour
{
    /// <summary>
    /// The max capacity of this housing building.
    /// </summary>
    [SerializeField] private int peopleCapacity;

    private void OnEnable()
    {
        
    }

    /// <summary>
    /// how attractive this building is for each civilian type
    /// </summary>
    //public Dictionary<CivilianType, float> attractiveness; 
}
