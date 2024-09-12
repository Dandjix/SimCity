using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSetterControl : MonoBehaviour
{
    [SerializeField] private HeightSetter HeightSetter;

    [SerializeField] private float zoomIncrement = 1f;
    [SerializeField] private float minRadius = 1f;
    [SerializeField] private float maxRadius = 10f;


    void Update()
    {
        HandleMode();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        HandleZoom();
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            //Debug.Log("hit");
            HeightSetter.SetBrushActive(true);

            Vector3 hitPositon = hit.point;
            Vector2Int SquarePosition = MapGrid.Instance.GetSquare(hitPositon, false);
            Vector3 center = MapGrid.Instance.getCenter(SquarePosition);

            HeightSetter.SetBrushCenter(center);
            //Debug.Log("center : " + center);
            //brush.transform.position = center;

            if (Input.GetMouseButton(0))
            {
                HeightSetter.Use();
            }
        }
        else
        {
            //Debug.Log("no hit");
            HeightSetter.SetBrushActive(false);
        }
    }

    private void Start()
    {
        radius = minRadius;
        HeightSetter.Radius = radius;
    }

    private float radius = 1;

    private void HandleZoom()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (scrollDelta != Vector2.zero && Input.GetKey(KeyCode.LeftControl))
        {
            //Debug.Log("radius : " + radius);
            radius += scrollDelta.y * zoomIncrement;
            radius = Mathf.Clamp(radius,minRadius,maxRadius);
            HeightSetter.Radius = radius;
        }
    }

    private TerrainPaintMode[] modes = new TerrainPaintMode[] { TerrainPaintMode.Higher, TerrainPaintMode.Lower, TerrainPaintMode.Flatten };
    private int modeIndex = 0;

    private void HandleMode()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            modeIndex++;
            modeIndex %= modes.Length;
            TerrainPaintMode mode = modes[modeIndex];
            HeightSetter.TerrainPaintMode = mode;
            Debug.Log("changed mode to : " + mode);
        }
    }
}
