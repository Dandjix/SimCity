using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SelectionLine : MonoBehaviour
{
    public event System.Action<LineData> OnSelectionMade;
    public event System.Action<LineData> OnSelectionChanged;

    private Vector2Int ?from;
    private Vector2Int to;

    private Axis lastValidAxis = Axis.Horizontal;

    //private void OnEnable()
    //{
    //    SetFrom(null);
    //}

    private void OnDisable()
    {
        SetFrom(null);
    }

    public void SetFrom(Vector2Int ?from)
    {
        if (from == null)
        {
            OnSelectionChanged?.Invoke(null);
            return;
        }
        //Debug.Log("from : " + from);
        this.from = MapGrid.Instance.Bounded(from.Value);
        to = this.from.Value;
        LineData data = new LineData(this.from.Value, to);
        //Debug.Log("data : "+data);
        OnSelectionChanged?.Invoke(data);
    }

    public void SetTo(Vector2Int to)
    {
        to = MapGrid.Instance.Bounded(to);
        int xDiff = from.Value.x - to.x;
        int yDiff = from.Value.y - to.y;

        //Debug.Log("diff : "+xDiff+", "+yDiff);

        if (Mathf.Abs(xDiff) == Mathf.Abs(yDiff))
        {
            switch (lastValidAxis)
            {
                case Axis.Horizontal:
                    goto Horizontal;

                case Axis.Vertical:
                    goto Vertical;
            }
        }
        else if (Mathf.Abs(xDiff) < Mathf.Abs(yDiff))
        {
            //Debug.Log("ydiff bigger");
            goto Vertical;
        }
        else
        {
            //Debug.Log("xdiff bigger");
            goto Horizontal;
        }

        Horizontal:
            //Debug.Log("Horizontal : ");
            this.to = new Vector2Int(to.x, from.Value.y);

            OnSelectionChanged?.Invoke(new LineData(from.Value, this.to));
            return;

        Vertical:
            //Debug.Log("Vertical : ");
            this.to = new Vector2Int(from.Value.x, to.y);

            OnSelectionChanged?.Invoke(new LineData(from.Value, this.to));
            return;

    }

    public void Commit()
    {
        LineData data = new LineData(from.Value, to);
        OnSelectionMade?.Invoke(data);
        SetFrom(null);
    }
}

public class LineData
{
    public Vector2Int from;
    public Vector2Int to;

    public LineData(Vector2Int from,Vector2Int to)
    {
        this.from = from;
        this.to = to;
    }

    public Axis GetAxis()
    {
        int xDiff = from.x - to.x;
        int yDiff = from.y - to.y;

        if (xDiff != 0 && yDiff != 0)
        {
            throw new System.ArgumentException("invalid line selection :" + from + " to " + to + " not a straight line.");
        }
        if(Mathf.Abs(xDiff)>0)
        {
            return Axis.Horizontal;
        }
        //else
        return Axis.Vertical;
    }

    public Vector2Int[] EachSquare()
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