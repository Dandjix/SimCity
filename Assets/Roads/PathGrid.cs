using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PathGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> pathsObjects = new Dictionary<Vector2Int, GameObject>();
    //public Adjacency[,] Ad
    private Adjacency[,] adjacencyGrid;

    private static PathGrid instance;
    public static PathGrid Instance {  get { return instance; } }

    private void Start()
    {
        instance = this;

        adjacencyGrid = new Adjacency[MapGrid.Instance.DimensionX, MapGrid.Instance.DimensionY];
    }

    public bool TryConnect(int x, int y, CardinalDirection[] directions)
    {
        //Debug.Log("connecting : "+x+", "+y+"\nadjGridSize : "+adjacencyGrid.GetLength(0)+", "+adjacencyGrid.GetLength(1));  

        Adjacency newAdjacency = adjacencyGrid[x, y];
        for (int i = 0; i < directions.Length; i++)
        {
            newAdjacency.Connect(directions[i]);
        }


        //Debug.Log("updated adjacency : "+newAdjacency);

        Variant variant;


        DestroyAsset(x, y);

        if (FindVariant(newAdjacency, out variant))
        {
            adjacencyGrid[x, y] = newAdjacency;
            InstantiateAsset(variant,x,y);
            return true;
        }
        return false;
    }
    private void DestroyAsset(int x, int y)
    {
        Vector2Int key = new Vector2Int(x,y);
        GameObject asset = pathsObjects.TryGetValue(key, out asset) ? asset : null;
        if (asset == null)
        {
            //Debug.LogError("you tried deleting an asset where there is none !");
            return;
        }
        Destroy(asset);
        pathsObjects.Remove(key);
    }


    private void InstantiateAsset(Variant variant, int x, int y)
    {
        //Debug.Log("cords : "+x+", "+y);
        Vector3 position;
        switch (heightType)
        {
            case HeightType.world:
                Vector2 center = MapGrid.Instance.GetCenterNoHeight(x, y);
                position = new Vector3(center.x, heightOffset, center.y);
                break;
            default:
            case HeightType.terrain:
                Vector3 centerWithHeight = MapGrid.Instance.GetCenter(x, y);
                position = new Vector3(centerWithHeight.x, centerWithHeight.y + heightOffset, centerWithHeight.z);
                break;
        }

        GameObject asset = Instantiate(variant.gameObject,transform);
        asset.transform.position = position;
        pathsObjects.Add(new Vector2Int(x, y), asset);
    }

    private bool FindVariant(Adjacency adjacency, out Variant result)
    {
        for (int i = 0; i < variants.Length; i++)
        {
            Variant variant = variants[i];

            if(isCorrectVariant(variant,adjacency))
            {
                result = variant;
                return true;
            }

        }
        result = variants[0];
        Debug.Log("no variants fit adjacency : " + adjacency);
        return false;
    }

    private bool isCorrectVariant(Variant variant, Adjacency adjacency)
    {
        foreach (CardinalDirection direction in Enum.GetValues(typeof(CardinalDirection)))
        {
            if(variant.containsDirection(direction) != adjacency.Connected(direction))
            {
                return false;
            }
        }
        return true;
    }

    [SerializeField] private Variant[] variants;
    [SerializeField] private HeightType heightType;
    [SerializeField] private float heightOffset;
}

[Serializable]
struct Variant
{
    [SerializeField] public GameObject gameObject;
    [SerializeField] public CardinalDirection[] directions;

    public bool containsDirection(CardinalDirection direction)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            CardinalDirection dir = directions[i];
            if(dir == direction)
                return true;
        }
        return false;
    }
}

enum HeightType
{
    world,
    terrain
}


struct Adjacency
{
    private byte adjacency;

    public bool Connected(CardinalDirection direction)
    {
        byte mask = (byte)(1 << ((byte)direction));
        return (adjacency & mask) != 0;
    }

    public void Connect(CardinalDirection direction)
    {
        //adjacency = adjacency
        //Debug.Log("direction : " + (byte)direction);
        byte mask =  (byte) (1 << ((byte)direction));
        //Debug.Log("Connect mask : "+mask);
        adjacency = (byte) (adjacency | mask);
    }

    public void Disconnect(CardinalDirection direction)
    {
        //adjacency = adjacency
        //Debug.Log("direction : " + (byte)direction);
        byte mask = (byte)(1 << ((byte)direction));
        byte invertedMask = (byte)~mask;
        //Debug.Log("Connect mask : "+mask);
        adjacency = (byte)(adjacency & invertedMask);
    }

    //public void Print(string name = "adjacency")
    //{
    //    Debug.Log(name + ":" +);
    //}

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder("adjacency(");
        foreach(CardinalDirection direction in Enum.GetValues(typeof(CardinalDirection)))
        {
            stringBuilder.Append(direction.ToString());
            stringBuilder.Append(":");
            stringBuilder.Append(Connected(direction));
            stringBuilder.Append(",");
        }
        stringBuilder.Append(Convert.ToString(adjacency, toBase: 2));

        stringBuilder.Append(")");

        return stringBuilder.ToString();
    }
}