using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionLine))]
public class PathGridSetter : MonoBehaviour
{
    private SelectionLine line;

    private void Start()
    {
        line = GetComponent<SelectionLine>();
    }

    private System.Nullable<Vector2Int> from = null;
    private Vector2Int to;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

                Vector3 hitPosition = hit.point;

                from = MapGrid.Instance.GetSquare(hitPosition);
                to = from.Value;

                line.SetFrom(from.Value);
                line.SetTo(to);
            }

        }
        else if (Input.GetMouseButton(0) && from != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

                Vector3 hitPosition = hit.point;

                to = MapGrid.Instance.GetSquare(hitPosition);

                line.SetFrom(from.Value);
                line.SetTo(to);
            }
        }
        else if (!Input.GetMouseButtonUp(0) && from != null)
        {
            line.Commit();
        }
    }
}
