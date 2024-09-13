using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSetterControl : MonoBehaviour
{
    [SerializeField] private HeightSetter HeightSetter;

    [SerializeField] private int radiusIncrement = 1;
    [SerializeField] private int minRadius = 1;
    [SerializeField] private int maxRadius;

    [SerializeField][Range(0,1)] private float falloffIncrement = 0.1f;
    [SerializeField][Range(0, 1)] private float minFalloff = 0f;
    [SerializeField][Range(0, 1)] private float maxFalloff = 1f;

    [SerializeField][Range(0, 1)] private float strengthIncrement = 0.1f;
    [SerializeField][Range(1, 10)] private float minStrength = 1f;
    [SerializeField][Range(1, 10)] private float maxStrength = 10f;


    void Update()
    {
        HandleMode();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        HandleRadius();
        HandleFalloff();
        HandleStrength();
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            //Debug.Log("hit");
            HeightSetter.SetBrushActive(true);

            Vector3 hitPositon = hit.point;
            Vector2Int SquarePosition = MapGrid.Instance.GetSquare(hitPositon, false);
            //Debug.Log("mapgrid instance : "+ MapGrid.Instance);
            Vector3 center = MapGrid.Instance.GetCenter(SquarePosition);

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

    private int radius = 1;

    private void HandleRadius()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (scrollDelta != Vector2.zero && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt))
        {
            //Debug.Log("radius : " + radius);
            radius += Mathf.CeilToInt(scrollDelta.y) * radiusIncrement;
            radius = Mathf.Clamp(radius,minRadius,maxRadius);
            HeightSetter.Radius = radius;
        }
    }

    private float falloff = 0.5f;

    private void HandleFalloff()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (scrollDelta != Vector2.zero && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt))
        {
            //Debug.Log("radius : " + radius);
            falloff += scrollDelta.y * falloffIncrement;
            falloff = Mathf.Clamp(falloff, minFalloff, maxFalloff);
            //Debug.Log("setting falloff to : " + falloff);
            if (falloff == 0)
                falloff = 0.000000000001f;
            HeightSetter.Falloff = falloff;
        }
    }

    private float strength = 1;

    private void HandleStrength()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;

        if (scrollDelta != Vector2.zero && Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
        {
            //Debug.Log("radius : " + radius);
            strength += scrollDelta.y * strengthIncrement;
            strength = Mathf.Clamp(strength, minStrength, maxStrength);

            Color color = Color.Lerp(Color.white, Color.red, (strength - minStrength) /(maxStrength-minStrength) );

            HeightSetter.Strength = strength;
            HeightSetter.Color = color;
        }
    }

    private TerrainPaintMode[] modes = new TerrainPaintMode[] { TerrainPaintMode.Raise, TerrainPaintMode.Lower, TerrainPaintMode.Flatten };
    private int modeIndex = 0;

    private void HandleMode()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            modeIndex++;
            modeIndex %= modes.Length;
            TerrainPaintMode mode = modes[modeIndex];
            HeightSetter.TerrainPaintMode = mode;
            //Debug.Log("changed mode to : " + mode);
        }
    }
}
