using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionLine))]
public class SelectionClick : MonoBehaviour
{
    private SelectionLine selectionLine;

    private void Start()
    {
        selectionLine = GetComponent<SelectionLine>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(ray, out hit,1000, layerMask))
            {
                Vector2Int square = MapGrid.Instance.GetSquare(hit.point);
                selectionLine.SetFrom(square);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectionLine.Commit();
        }
        else if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                Vector2Int square = MapGrid.Instance.GetSquare(hit.point);
                //Debug.Log("square : "+square);
                selectionLine.SetTo(square);
            }
        }
    }
}
