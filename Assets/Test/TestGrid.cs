using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    //[SerializeField] private new Camera camera;
    [SerializeField] private GameObject marker;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    //private MapGrid mapGrid;
    // Start is called before the first frame update
    void Start()
    {
        var mapGrid = MapGrid.Instance;


        foreach (var square in mapGrid.GetAllSquares())
        {
            float height = TerrainManager.Instance.GetHeightAtBottomLeft(square.x, square.y);
            var marker2 = Instantiate(marker);
            marker2.transform.position = new Vector3(square.x,height,square.y);
        }
    }

    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //LayerMask layerMask = LayerMask.GetMask("Terrain");
        //if (Physics.Raycast(ray, out hit, 100,layerMask))
        //{
        //    //Debug.Log("hit !");
        //    Vector3 hitPositon = hit.point;
        //    //Debug.Log("hitPos : " + hitPositon);
        //    Vector2Int SquarePosition = MapGrid.Instance.GetSquare(hitPositon, true);
        //    //Debug.Log("SquarePos : "+SquarePosition);
        //    Vector3 center = MapGrid.Instance.getCenter(SquarePosition);
            
        //    //Debug.Log("center : " + center);
        //    marker.transform.position = center;

        //    if(MapGrid.Instance.IsInBounds(SquarePosition))
        //    {
        //        marker.GetComponent<Renderer>().material = green;
        //    }
        //    else
        //    {
        //        marker.GetComponent<Renderer>().material = red;
        //    }
        //}

        //else
        //Debug.Log("no hit");
    }
}
