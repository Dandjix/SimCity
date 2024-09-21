using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionLine))]
public class RoadCreator : MonoBehaviour
{
    [SerializeField] private PathGrid roadsPathGrid;

    private SelectionLine selectionLine;

    void Awake()
    {
        selectionLine = GetComponent<SelectionLine>();
    }

    private void OnEnable()
    {
        selectionLine.OnSelectionMade += CreateRoads;
    }

    private void OnDisable()
    {
        selectionLine.OnSelectionMade -= CreateRoads;
    }

    private void CreateRoads(LineData lineData)
    {
        Axis axis = lineData.GetAxis();
        Debug.Log("creating roads !");
        //roadsPathGrid.
    }

}
