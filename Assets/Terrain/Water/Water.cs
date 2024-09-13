using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void Start()
    {

        TerrainManager.Instance.AfterGenerate += Generate;
        //Debug.Log("hooked !");
    }

    private void Generate()
    {
        //Debug.Log("generating water !");
        Vector2Int dimensions = MapGrid.Instance.GetDimensions(true);

        transform.localScale = new Vector3(dimensions.x/10,1,dimensions.y/10);

        Vector2 center = MapGrid.Instance.Center;
        transform.position = new Vector3(center.x,0,center.y);
    }
}
