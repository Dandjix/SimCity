using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    //public Adjacency[,] Ad
    private Adjacency[,] adjacencyGrid;

    private static PathGrid instance;
    public static PathGrid Instance {  get { return instance; } }

    private void Start()
    {
        instance = this;

        adjacencyGrid = new Adjacency[MapGrid.Instance.DimensionX, MapGrid.Instance.DimensionY];
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
        return false;
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

    public void Print(string name = "adjacency")
    {
        Debug.Log(name + ":" + Convert.ToString(adjacency,toBase:2));
    }
}