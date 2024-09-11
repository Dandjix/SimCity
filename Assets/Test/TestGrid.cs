using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    //[SerializeField] private new Camera camera;
    [SerializeField] private GameObject marker;
    //private MapGrid mapGrid;
    // Start is called before the first frame update
    //void Start()
    //{
    //    var mapGrid = MapGrid.Instance;


    //    foreach (var square in mapGrid.GetAllSquares())
    //    {
    //        Vector3 center = MapGrid.Instance.getCenter(square);
    //        var marker2 = Instantiate(marker);
    //        marker2.transform.position = center;
    //    }
    //}

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        if (Physics.Raycast(ray, out hit, 100,layerMask))
        {
            //Debug.Log("hit !");
            Vector3 hitPositon = hit.point;
            //Debug.Log("hitPos : " + hitPositon);
            Vector2Int SquarePosition = MapGrid.Instance.GetSquare(hitPositon, false);
            //Debug.Log("SquarePos : "+SquarePosition);
            Vector3 center = MapGrid.Instance.getCenter(SquarePosition);
            //Debug.Log("center : " + center);
            marker.transform.position = center;
        }
        //else
        //Debug.Log("no hit");
    }
}
