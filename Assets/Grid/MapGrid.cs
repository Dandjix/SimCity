using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
public class MapGrid : MonoBehaviour
{
    public static MapGrid Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //[SerializeField] private Vector2Int dimensions = new Vector2Int(100, 100);
    [SerializeField] [Range(32,500)] private int dimensionX = 100;
    public int DimensionX { get { return dimensionX; } }
        
    [SerializeField] [Range(32,500)] private int dimensionY = 100;
    public int DimensionY { get { return dimensionY; } }

    private Vector2 cellDimensions = new Vector2(1, 1);
    //public Vector2 CellDimensions { get {  return cellDimensions; } }

    [SerializeField] private int margin = 5;
    public int Margin { get { return margin; } }

    public System.Collections.Generic.IEnumerable<Vector2> AllSquaresCenter(bool withMargin = false)
    {
        float xOffset = cellDimensions.x / 2;
        float yOffset = cellDimensions.y / 2;
        if (withMargin)
        {
            for (int i = -margin; i < dimensionX + margin; i++)
            {
                for (int j = -margin; j < dimensionY + margin; j++)
                {
                    yield return new Vector2(i * cellDimensions.x + xOffset, j * cellDimensions.y + yOffset);
                }
            }
        }
        else
        {
            for (int i = 0; i < dimensionX; i++)
            {
                for (int j = 0; j < dimensionY; j++)
                {
                    yield return new Vector2(i * cellDimensions.x + xOffset, j * cellDimensions.y + yOffset);
                }
            }
        }

    }

    public Vector3 getCorner(CardinalDirection cardinalDirection,bool includeMargin=false)
    {
        if(includeMargin)
            return getCornerWithMargin(cardinalDirection);
        //else
            return getCornerNoMargin(cardinalDirection);
    }

    /// <summary>
    /// returns the coordinates of the corner according to the cardinalDirection
    /// </summary>
    /// <param name="corner"></param>
    /// <returns></returns>
    private Vector3 getCornerNoMargin(CardinalDirection cardinalDirection)
    {
        if (cardinalDirection == CardinalDirection.North ||
            cardinalDirection == CardinalDirection.South ||
            cardinalDirection == CardinalDirection.East ||
            cardinalDirection == CardinalDirection.West)
        {
            throw new ArgumentException(cardinalDirection.ToString() + " is not a valid direction for a corner");
        }

        bool north = cardinalDirection == CardinalDirection.NorthEast || cardinalDirection == CardinalDirection.NorthWest;
        bool east = cardinalDirection == CardinalDirection.NorthEast || cardinalDirection == CardinalDirection.SouthEast;

        float x, y;



        if (east)
        {
            x = cellDimensions.x * dimensionX;
        }
        else
        {
            x = 0;
        }

        if (north)
        {
            y = cellDimensions.y * dimensionY;
        }
        else
        {
            y = 0;
        }

        Vector3 res = new Vector3(x, 0, y);

        return res;
    }
    /// <summary>
    /// returns the coordinates of the corner according to the cardinalDirection, with margin applied
    /// </summary>
    /// <param name="cardinalDirection"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private Vector3 getCornerWithMargin(CardinalDirection cardinalDirection)
    {
        if (cardinalDirection == CardinalDirection.North ||
            cardinalDirection == CardinalDirection.South ||
            cardinalDirection == CardinalDirection.East ||
            cardinalDirection == CardinalDirection.West)
        {
            throw new ArgumentException(cardinalDirection.ToString() + " is not a valid direction for a corner");
        }

        bool north = cardinalDirection == CardinalDirection.NorthEast || cardinalDirection == CardinalDirection.NorthWest;
        bool east = cardinalDirection == CardinalDirection.NorthEast || cardinalDirection == CardinalDirection.SouthEast;

        float x, y;



        if (east)
        {
            x = cellDimensions.x * dimensionX + margin;
            
        }
        else
        {
            x = -margin;
        }

        if (north)
        {
            y = cellDimensions.y * dimensionY + margin;
            
        }
        else
        {
            y = -margin;
        }

        Vector3 res = new Vector3(x, 0, y);
        
        return res;
    }
    /// <summary>
    /// gets the square at that position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2Int getSquare(Vector3 position,bool marginIncluded=false)
    {
        int x = (int)Mathf.Floor(position.x / cellDimensions.x);
        int y = (int)Mathf.Floor(position.z / cellDimensions.y);
        if (marginIncluded)
        {
            x = Mathf.Clamp(x, -margin, dimensionX+margin-1);
            y = Mathf.Clamp(y, -margin, dimensionY+margin-1);
        }
        else
        {
            x = Mathf.Clamp(x, 0, dimensionX - 1);
            y = Mathf.Clamp(y, 0, dimensionY - 1);
        }

        return new Vector2Int(x, y);
    }

    public Vector3 getCenter(Vector2 square)
    {
        float x = square.x*cellDimensions.x + cellDimensions.x/2;
        float y = square.y*cellDimensions.y + cellDimensions.y/2;
        return new Vector3(x,0,y);
    }

    /// <summary>
    /// bounds a movement to the mapgrid. From is supposed to be in the grid, to may be outside.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public Vector3 bounded(Vector3 from, Vector3 to,bool marginIncluded=false)
    {
        //TODO: make it so that this catches the linear thing you got what i mean
        //if (to.x < 0 && to.z < 0)
        //    return new Vector3(0, to.y, 0);
        //if (to.x > dimensionX * cellDimensions.x && to.z > dimensionY * cellDimensions.y)
        //    return new Vector3(dimensionX * cellDimensions.x, 0, dimensionY * cellDimensions.y);
        //if(x>)
        float x = to.x;
        float y = to.y;
        float z = to.z;
        if (marginIncluded)
        {
            x = Mathf.Clamp(x, -margin, dimensionX * cellDimensions.x+margin);
            z = Mathf.Clamp(z, -margin, dimensionY * cellDimensions.y+margin);
        }
        else
        {
            x = Mathf.Clamp(x, 0, dimensionX * cellDimensions.x);
            z = Mathf.Clamp(z, 0, dimensionY * cellDimensions.y);
        }


        return new Vector3(x,y,z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        for (int i = -margin+1; i<= dimensionX+margin-1; i++)
        {
            float x, ySouth, yNorth;
            ySouth = -margin;
            yNorth = cellDimensions.y*dimensionY + margin;
            x = i*cellDimensions.x;

            Vector3 from = new Vector3(x,0,ySouth);
            Vector3 to = new Vector3(x,0, yNorth);

            Gizmos.DrawLine(from,to);
        }

        for (int i = -margin +1; i <= dimensionY + margin - 1 ; i++)
        {
            float y, xWest, xEast;
            xWest = -margin;
            xEast = cellDimensions.x * dimensionX + margin;
            y = i * cellDimensions.y;

            Vector3 from = new Vector3(xWest, 0, y);
            Vector3 to = new Vector3(xEast, 0, y);

            Gizmos.DrawLine(from, to);
        }

        Vector3 southWest = getCornerNoMargin(CardinalDirection.SouthWest);
        Vector3 northWest = getCornerNoMargin(CardinalDirection.NorthWest);
        Vector3 northEast = getCornerNoMargin(CardinalDirection.NorthEast);
        Vector3 southEast = getCornerNoMargin(CardinalDirection.SouthEast);

        Vector3 southWestWithMargin = getCornerWithMargin(CardinalDirection.SouthWest);
        Vector3 northWestWithMargin = getCornerWithMargin(CardinalDirection.NorthWest);
        Vector3 northEastWithMargin = getCornerWithMargin(CardinalDirection.NorthEast);
        Vector3 southEastWithMargin = getCornerWithMargin(CardinalDirection.SouthEast);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(southWest, northWest);
        Gizmos.DrawLine(northWest, northEast);
        Gizmos.DrawLine(northEast, southEast);
        Gizmos.DrawLine(southEast, southWest);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(southWestWithMargin, northWestWithMargin);
        Gizmos.DrawLine(northWestWithMargin, northEastWithMargin);
        Gizmos.DrawLine(northEastWithMargin, southEastWithMargin);
        Gizmos.DrawLine(southEastWithMargin, southWestWithMargin);

        //foreach(var center in AllSquaresCenter(true))
        //{
        //    Vector3 threeDcenter = new Vector3(center.x, 0, center.y);
        //    Gizmos.DrawCube(threeDcenter, new Vector3(0.1f,0.1f,0.1f));
        //}


        Gizmos.color = Color.white;
    }
}
