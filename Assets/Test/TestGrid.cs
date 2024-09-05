using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    //[SerializeField] private new Camera camera;
    [SerializeField] private GameObject marker;
    private MapGrid mapGrid;
    // Start is called before the first frame update
    void Start()
    {
        mapGrid = MapGrid.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.NameToLayer("Terrain");
        if(Physics.Raycast(ray,out hit,100, layerMask))
        {
            Vector3 hitPositon = hit.point;
            //Debug.Log("hitPos : " + hitPositon);
            Vector2 SquarePosition = mapGrid.getSquare(hitPositon,false);
            //Debug.Log("SquarePos : "+SquarePosition);
            Vector3 center = mapGrid.getCenter(SquarePosition);
            //Debug.Log("center : " + center);
            marker.transform.position = new Vector3(center.x,marker.transform.position.y,center.z);
        }
    }
}
