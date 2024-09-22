namespace StatusGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnderwaterNotWalkable : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("start of unw");

            IEnumerable<Vector2Int> squares = MapGrid.Instance.GetAllSquares();

            foreach (Vector2Int square in squares)
            {
                var heightAtCenter = TerrainManager.Instance.GetHeightAtCenter(square);
                if (heightAtCenter <= 0)
                {
                    StatusGrid.Instance.SetStatus(StatusType.NotWalkable, square);
                }
                if (heightAtCenter <= 1 && heightAtCenter > -0.1f)
                {
                    StatusGrid.Instance.SetStatus(StatusType.NotBuildable, square);
                }
            }
        }
    }

}

