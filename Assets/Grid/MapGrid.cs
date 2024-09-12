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
    /// <summary>
    /// the x dimension of the grid, without the margin
    /// </summary>
    public int DimensionX { get { return dimensionX; } }
        
    [SerializeField] [Range(32,500)] private int dimensionY = 100;
    /// <summary>
    /// the y dimension of the grid, without the margin
    /// </summary>
    public int DimensionY { get { return dimensionY; } }

    [SerializeField] private float gizmosHeight;

    private Vector2 cellDimensions = new Vector2(1, 1);
    //public Vector2 CellDimensions { get {  return cellDimensions; } }

    [SerializeField][Min(1)] private int margin = 5;
    public int Margin { get { return margin; } }

    public IEnumerable<Vector2> GetAllSquaresCenter(bool withMargin = false)
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

    public IEnumerable<Vector2Int> GetAllSquares(bool withMargin = false)
    {
        float xOffset = cellDimensions.x / 2;
        float yOffset = cellDimensions.y / 2;
        if (withMargin)
        {
            for (int i = -margin; i < dimensionX + margin; i++)
            {
                for (int j = -margin; j < dimensionY + margin; j++)
                {
                    yield return new Vector2Int(i, j);
                }
            }
        }
        else
        {
            for (int i = 0; i < dimensionX; i++)
            {
                for (int j = 0; j < dimensionY; j++)
                {
                    yield return new Vector2Int(i, j);
                }
            }
        }
    }

    public Vector3 getCorner(CardinalDirection cardinalDirection,bool includeMargin=false)
    {
        if(includeMargin)
            return GetCornerWithMargin(cardinalDirection);
        //else
            return GetCornerNoMargin(cardinalDirection);
    }

    /// <summary>
    /// returns the coordinates of the corner according to the cardinalDirection
    /// </summary>
    /// <param name="corner"></param>
    /// <returns></returns>
    private Vector3 GetCornerNoMargin(CardinalDirection cardinalDirection)
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
    private Vector3 GetCornerWithMargin(CardinalDirection cardinalDirection)
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
    public Vector2Int GetSquare(Vector3 position,bool marginIncluded=false)
    {
        int x = Mathf.FloorToInt(position.x / cellDimensions.x);
        int y = Mathf.FloorToInt(position.z / cellDimensions.y);
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
    /// <summary>
    /// not taking into account the height
    /// </summary>
    /// <param name="square"></param>
    /// <returns></returns>
    public Vector2 GetCenterNoHeight(Vector2Int square)
    {
        float x = square.x*cellDimensions.x + cellDimensions.x/2;
        float y = square.y*cellDimensions.y + cellDimensions.y/2;
        return new Vector2(x,y);
    }
    /// <summary>
    /// with height taken into account. This probably should only be called at runtime
    /// </summary>
    /// <param name="square"></param>
    /// <returns></returns>
    public Vector3 getCenter(Vector2Int square)
    {
        float x = square.x * cellDimensions.x + cellDimensions.x / 2;
        float y = square.y * cellDimensions.y + cellDimensions.y / 2;
        float height = TerrainManager.Instance.GetHeightAtCenter(square);
        return new Vector3(x,height, y);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="marginIncluded"></param>
    /// <returns>true if the position is within the bounds of the map grid, false otherwise</returns>
    public bool IsInBounds(Vector2Int position, bool marginIncluded = false)
    {
        return IsInBounds(position.x, position.y, marginIncluded);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="marginIncluded"></param>
    /// <returns>true if the position is within the bounds of the map grid, false otherwise</returns>
    public bool IsInBounds(int x, int y, bool marginIncluded=false)
    {
        if(!marginIncluded)
        {
            return x < 0 || y < 0 || x> dimensionX || y> dimensionY;
        }
        // else
        return x < -margin || y < -margin || x > dimensionX + margin || y > dimensionY + margin;
    }



    /// <summary>
    /// bounds a movement to the mapgrid. From is supposed to be in the grid, to may be outside.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public Vector3 Bounded(Vector3 from, Vector3 to,bool marginIncluded=false)
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">The position that we are going to get the closest position of</param>
    /// <returns>The clamped valid grid index Vector2Int</returns>
    public Vector2Int Bounded(Vector2Int position,bool marginIncluded=false)
    {
        int x = position.x;
        int y = position.y;

        if (!marginIncluded)
        {
            if(x < 0) { x = 0; }
            else if(x >= DimensionX) { x = DimensionX-1; }
            if(y < 0) { y = 0; }
            else if (y >= DimensionY) { y = DimensionY - 1; }

            return new Vector2Int(x,y);
        }

        if (x < -margin) { x = -margin; }
        else if (x > dimensionX + margin) { x = dimensionX + margin - 1; }
        if(y < -margin) { y = -margin; }
        else if (y > dimensionY + margin) { y = dimensionY + margin - 1; }

        return new Vector2Int(x, y);
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

            Vector3 from = new Vector3(x,gizmosHeight,ySouth);
            Vector3 to = new Vector3(x,gizmosHeight, yNorth);

            Gizmos.DrawLine(from,to);
        }

        for (int i = -margin +1; i <= dimensionY + margin - 1 ; i++)
        {
            float y, xWest, xEast;
            xWest = -margin;
            xEast = cellDimensions.x * dimensionX + margin;
            y = i * cellDimensions.y;

            Vector3 from = new Vector3(xWest, gizmosHeight, y);
            Vector3 to = new Vector3(xEast, gizmosHeight, y);

            Gizmos.DrawLine(from, to);
        }

        Vector3 southWest = GetCornerNoMargin(CardinalDirection.SouthWest);
        Vector3 northWest = GetCornerNoMargin(CardinalDirection.NorthWest);
        Vector3 northEast = GetCornerNoMargin(CardinalDirection.NorthEast);
        Vector3 southEast = GetCornerNoMargin(CardinalDirection.SouthEast);

        Vector3 southWestWithMargin = GetCornerWithMargin(CardinalDirection.SouthWest);
        Vector3 northWestWithMargin = GetCornerWithMargin(CardinalDirection.NorthWest);
        Vector3 northEastWithMargin = GetCornerWithMargin(CardinalDirection.NorthEast);
        Vector3 southEastWithMargin = GetCornerWithMargin(CardinalDirection.SouthEast);

        southWest.y = gizmosHeight;
        northWest.y = gizmosHeight;
        northEast.y = gizmosHeight;
        southEast.y = gizmosHeight;
        
        southWestWithMargin.y = gizmosHeight;
        northWestWithMargin.y = gizmosHeight;
        northEastWithMargin.y = gizmosHeight;
        southEastWithMargin.y = gizmosHeight;

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
