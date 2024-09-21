using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionLine))]
public class RoadCreator : MonoBehaviour
{
    [SerializeField] private PathGrid roadsPathGrid;

    private SelectionLine selectionLine;

    void Awake()
    {
        selectionLine = GetComponent<SelectionLine>();
    }

    private void OnEnable()
    {
        selectionLine.OnSelectionMade += CreateRoads;
    }

    private void OnDisable()
    {
        selectionLine.OnSelectionMade -= CreateRoads;
    }

    private void CreateRoads(LineData lineData)
    {
        foreach(var tile in EachTile(lineData))
        {
            roadsPathGrid.TryConnect(tile.x, tile.y, tile.directions);
        }
    }

    private IEnumerable<TileData> EachTile(LineData lineData)
    {
        Axis axis = lineData.GetAxis();
        //Debug.Log("creating roads ! : "+lineData.from+", "+lineData.to);

        Vector2Int[] eachSquare = lineData.EachSquare();

        if (eachSquare.Length == 1)
        {
            yield return new TileData(eachSquare[0].x, eachSquare[0].y, new CardinalDirection[0]);
            yield break;
        }

        for (int i = 0; i < eachSquare.Length; i++)
        {

            Vector2Int square = eachSquare[i];
            CardinalDirection[] directions;

            if (i == 0)
            {
                switch (axis)
                {
                    case Axis.Horizontal:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.West,
                        };
                        break;
                    default: //case Axis.Vertical:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.South,
                        };
                        break;
                }
            }
            else if(i==eachSquare.Length-1)
            {
                switch (axis)
                {
                    case Axis.Horizontal:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.East,
                        };
                        break;
                    default: //case Axis.Vertical:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.North,
                        };
                        break;
                }
            }
            else
            {
                switch (axis)
                {
                    case Axis.Horizontal:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.East,
                        CardinalDirection.West,
                        };
                        break;
                    default: //case Axis.Vertical:
                        directions = new CardinalDirection[]
                        {
                        CardinalDirection.North,
                        CardinalDirection.South,
                        };
                        break;
                }
            }

            yield return new TileData(square.x, square.y, directions);
        }
    }

    private struct TileData
    {
        public TileData(int x, int y, CardinalDirection[] directions)
        {
            this.x = x; 
            this.y = y; 
            this.directions = directions;
        }

        public int x; public int y;
        public CardinalDirection[] directions;
    }
}
