using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class HeightSetter : MonoBehaviour
{
    [SerializeField] private GameObject marker;

    [SerializeField] private Texture2D brushTexture;

    [SerializeField] private float strength = 1;
    public float Strength { 
        get => strength; 
        set => strength = value; }

    private float minHeight;
    private float maxHeight;

    public System.Action OnParameterChanged;

    private void OnEnable()
    {
        brush.SetActive(true);
    }

    private void OnDisable()
    {
        if(brush ==  null)
            return;

        brush?.SetActive(false);
    }

    private Color color = Color.white;
    public Color Color { 
        get => color;
        set
        {
            color = value;
            GenerateBrushTextureMaterial();
            OnParameterChanged?.Invoke();
        }
                
    }

    private TerrainPaintMode terrainPaintMode;
    public TerrainPaintMode TerrainPaintMode { 
        get => terrainPaintMode; 
        set { 
            terrainPaintMode = value;
            OnParameterChanged?.Invoke();
        } }

    private Vector3 center;

    [Min(1)][SerializeField] private float textureSize = 1;

    [SerializeField]private int radius = 2;
    public int Radius { get { return radius; } set 
        {
            radius = value;
            AdjustSize();
            GenerateBrushTextureMaterial();
            OnParameterChanged?.Invoke();
        } }

    [SerializeField] float falloff = 1.0f;
    public float Falloff { get { return falloff; } set 
        { 
            falloff = value; 
            GenerateBrushTextureMaterial();
            OnParameterChanged?.Invoke();
        } }

    [SerializeField] private GameObject brush;

    [SerializeField] private Material brushMaterial;

    [SerializeField] private UnityEngine.Rendering.Universal.DecalProjector projector;

    public void GenerateBrushTextureMaterial()
    {
        float scaledRadius = radius * textureSize;
        int size = Mathf.CeilToInt(scaledRadius * 2);
        brushTexture = new Texture2D(size+1, size+1);
        brushTexture.wrapMode = TextureWrapMode.Clamp;
        //Material brushMaterial = new Material(brushMaterial);

        for (int x = 0; x < size+1; x++)
        {
            for (int y = 0; y < size+1; y++)
            {
                //if(TerrainPaintMode== TerrainPaintMode.Flatten)
                //{
                //    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(size/2, size/2));
                //    float normalizedDistance = Mathf.Clamp01(distance / scaledRadius);
                //    if(normalizedDistance < 1)
                //        brushTexture.SetPixel(x, y, Color.white);
                //    else
                //        brushTexture.SetPixel(x, y, new Color(0,0,0,0));
                //}
                //else
                //{

                // Calculate the distance from the center of the texture
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(size / 2, size / 2));

                // Normalize the distance
                float normalizedDistance = Mathf.Clamp01(distance / scaledRadius);

                // Calculate the falloff using a power function to smooth the edges
                float falloffValue = Mathf.Pow(1 - normalizedDistance, falloff);

                // Set the pixel color based on the falloff value


                brushTexture.SetPixel(x, y, new Color(falloffValue*color.r, falloffValue*color.g, falloffValue*color.b, falloffValue));

                //brushTexture.SetPixel(x, y, Color.red);
                //}


            }
        }
        brushTexture.Apply();
        brushMaterial.SetTexture("Base_Map", brushTexture);
        //brushMaterial.SetColor(Color.red);
        //Debug.Log("set texture !");
        //return material;
    }

    public void AdjustSize()
    {
        //brush.transform.localScale = new Vector3(radius/2,1,radius/2);

        projector.size = new Vector3(radius*2, radius*2,projector.size.z);
    }

    private void Start()
    {
        minHeight = TerrainManager.Instance.GetMinHeight();
        maxHeight = TerrainManager.Instance.GetMaxHeight();

        OnParameterChanged?.Invoke();
        GenerateBrushTextureMaterial();
    }

    public void SetBrushActive(bool active)
    {
        brush.SetActive(active);
    }

    public void SetBrushCenter(Vector3 position)
    {
        center = position;
        brush.transform.position = position;
        //Debug.Log("set transform to " + position);
    }

    public void Use()
    {
        float factor = Time.deltaTime * strength;
        switch(terrainPaintMode)
        {
            case (TerrainPaintMode.Lower):
                LowerHeights(factor);
                break;
            case (TerrainPaintMode.Raise):
                RaiseHeights(factor);
                break;
            case (TerrainPaintMode.Flatten):
                FlattenHeights(factor);
                break;
        }

    }

    private void LowerHeights(float factor)
    {
        int size = radius * 2;

        int centerX = Mathf.FloorToInt(center.x);
        int centerY = Mathf.FloorToInt(center.z);
        Vector2Int centerInt = new Vector2Int(centerX, centerY);

        int BLX = centerX - radius;
        int BLY = centerY - radius;
        Vector2Int bottomLeft = new Vector2Int(BLX, BLY);

        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                Vector2Int vertex = new Vector2Int(BLX + x, BLY + y);

                if (!MapGrid.Instance.IsBottomLeftInBounds(vertex))
                {
                    continue;
                }

                float distance = Vector2Int.Distance(vertex, centerInt);

                float normalizedDistance = Mathf.Clamp01(distance / radius);

                float falloffValue = Mathf.Pow(1 - normalizedDistance, falloff);

                float increment = falloffValue * factor;
                float height = TerrainManager.Instance.GetHeightAtBottomLeft(vertex.x,vertex.y) - increment;
                SetHeight(vertex, height);
            }
        }
        TerrainManager.Instance.ApplyHeights();
    }

    private void RaiseHeights(float factor)
    {
        int size = radius * 2;

        int centerX = Mathf.FloorToInt(center.x);
        int centerY = Mathf.FloorToInt(center.z);
        Vector2Int centerInt = new Vector2Int(centerX, centerY);

        int BLX = centerX - radius;
        int BLY = centerY - radius;
        Vector2Int bottomLeft = new Vector2Int(BLX, BLY);

        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                Vector2Int vertex = new Vector2Int(BLX + x, BLY + y);

                if (!MapGrid.Instance.IsBottomLeftInBounds(vertex))
                {
                    continue;
                }

                float distance = Vector2Int.Distance(vertex, centerInt);

                float normalizedDistance = Mathf.Clamp01(distance / radius);

                float falloffValue = Mathf.Pow(1 - normalizedDistance, falloff);

                float increment = falloffValue * factor;
                float height = TerrainManager.Instance.GetHeightAtBottomLeft(vertex.x, vertex.y) + increment;
                SetHeight(vertex, height);
            }
        }
        TerrainManager.Instance.ApplyHeights();
    }

    private void FlattenHeights(float factor)
    {
        int size = radius * 2;

        int centerX = Mathf.FloorToInt(center.x);
        int centerY = Mathf.FloorToInt(center.z);
        Vector2Int centerInt = new Vector2Int(centerX, centerY);

        int BLX = centerX - radius;
        int BLY = centerY - radius;
        Vector2Int bottomLeft = new Vector2Int(BLX, BLY);

        float heightAtCenter = TerrainManager.Instance.GetHeightAtBottomLeft(centerX,centerY);

        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                Vector2Int vertex = new Vector2Int(BLX + x, BLY + y);

                if (!MapGrid.Instance.IsBottomLeftInBounds(vertex))
                {
                    continue;
                }

                float heightAtVertex = TerrainManager.Instance.GetHeightAtBottomLeft(vertex.x, vertex.y);

                float distance = Vector2Int.Distance(vertex, centerInt);

                float normalizedDistance = Mathf.Clamp01(distance / radius);

                float falloffValue = Mathf.Pow(1 - normalizedDistance, falloff);

                float increment = falloffValue * factor;

                float height;

                float heightDifference = heightAtCenter - heightAtVertex;

                if (increment >= Mathf.Abs(heightDifference))
                {
                    height = heightAtCenter;
                }
                else if(heightDifference < 0)
                {
                    height = heightAtVertex - increment;
                }
                else
                {
                    height = heightAtVertex + increment;
                }

                SetHeight(vertex, height);
            }
        }

        TerrainManager.Instance.ApplyHeights();
    }


    private void SetHeight(Vector2Int vertex, float height)
    {
        height = Mathf.Clamp(height,minHeight,maxHeight);
        TerrainManager.Instance.SetHeight(vertex, height);
    }
}

public enum TerrainPaintMode
{
    Raise,
    Lower,
    Flatten
}