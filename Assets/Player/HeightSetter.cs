using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class HeightSetter : MonoBehaviour
{
    [SerializeField] private Texture2D brushTexture;

    private TerrainPaintMode terrainPaintMode;
    public TerrainPaintMode TerrainPaintMode { 
        get => terrainPaintMode; 
        set { 
            terrainPaintMode = value;
            GenerateBrushTextureMaterial();
        } }

    public float height = 0;

    private Vector3 center;

    [Min(1)][SerializeField] private float textureSize = 1;

    [SerializeField]private float radius = 2;
    public float Radius { get { return radius; } set 
        {
            radius = value;
            AdjustSize();
            GenerateBrushTextureMaterial();
        } }

    [SerializeField] float falloff = 1.0f;
    public float Falloff { get { return falloff; } set 
        { 
            falloff = value; 
            GenerateBrushTextureMaterial();
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
                if(TerrainPaintMode== TerrainPaintMode.Flatten)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(size/2, size/2));
                    float normalizedDistance = Mathf.Clamp01(distance / scaledRadius);
                    if(normalizedDistance < 1)
                        brushTexture.SetPixel(x, y, Color.white);
                    else
                        brushTexture.SetPixel(x, y, new Color(0,0,0,0));
                }
                else
                {
                    // Calculate the distance from the center of the texture
                    float distance = Vector2.Distance(new Vector2(x, y), new Vector2(size / 2, size / 2));

                    // Normalize the distance
                    float normalizedDistance = Mathf.Clamp01(distance / scaledRadius);

                    // Calculate the falloff using a power function to smooth the edges
                    float falloffValue = Mathf.Pow(1 - normalizedDistance, falloff);

                    // Set the pixel color based on the falloff value
                    brushTexture.SetPixel(x, y, new Color(falloffValue, falloffValue, falloffValue, falloffValue));
                    //brushTexture.SetPixel(x, y, Color.red);
                }


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
        Vector2Int SquarePosition = new Vector2Int((int)center.x,(int)center.z);

        TerrainManager.Instance.SetHeight(SquarePosition.x, SquarePosition.y, height);
        TerrainManager.Instance.RegenChangedChunks();
    }

}

public enum TerrainPaintMode
{
    Higher,
    Lower,
    Flatten
}