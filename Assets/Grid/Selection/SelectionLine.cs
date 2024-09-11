using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SelectionLine : MonoBehaviour
{
    public System.Action<LineData> OnSelectionMade;

    private Vector2Int from;
    private Vector2Int to;

    private Axis lastValidAxis = Axis.Horizontal;


    public void SetFrom(Vector2Int from)
    {
        this.from = MapGrid.Instance.Bounded(from);
    }


    public void Commit()
    {
        LineData data = new LineData(from, to);
        OnSelectionMade?.Invoke(data);
    }

    public void SetTo(Vector2Int to)
    {
        to = MapGrid.Instance.Bounded(to);
        int xDiff = from.x - to.x;
        int yDiff = from.y - to.y;

        if (Mathf.Abs(xDiff) == Mathf.Abs(yDiff))
        {
            switch (lastValidAxis)
            {
                case Axis.Horizontal:
                    goto Horizontal;

                case Axis.Vertical:
                    goto Vertical;
            }
            if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
            {
                goto Vertical;
            }
            else
            {
                goto Horizontal;
            }

        Horizontal:
            this.to = new Vector2Int(to.x, from.y);
            return;

        Vertical:
            this.to = new Vector2Int(to.y, from.x);
            return;
        }
    }
}

public struct LineData
{
    Vector2Int from;
    Vector2Int to;

    public LineData(Vector2Int from,Vector2Int to)
    {
        this.from = from;
        this.to = to;
    }

    Vector2Int[] EachSquare()
    {
        Vector2Int step;
        int xDiff = from.x - to.x;
        int yDiff = from.y - to.y;
        int length;
        if (xDiff == 0 && yDiff == 0)
        {
            var res = new Vector2Int[1];
            res[0] = from;
            return res;
        }
        if (xDiff != 0 && yDiff != 0)
        {
            throw new System.ArgumentException("invalid line selection :" + from + " to " + to + " not a straight line.");
        }

        if (yDiff < 0)
        {
            step = new Vector2Int(0, 1);
            length = -yDiff;
        }
        else if (yDiff > 0)
        {
            step = new Vector2Int(0, -1);
            length = yDiff;
        }
        else if (xDiff < 0)
        {
            step = new Vector2Int(1, 0);
            length = -xDiff;
        }
        else
        {
            step = new Vector2Int(-1, 0);
            length = xDiff;
        }

        Vector2Int pos = from;
        Vector2Int[] positionsInLine = new Vector2Int[length];

        for (int i = 0; i < length; i++)
        {
            positionsInLine[i] = step;
            pos = pos + step;
        }

        return positionsInLine;
    }
}