using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class HeightSetter : MonoBehaviour
{

    public float height = 0;

    public float radius = 2;

    [SerializeField] private GameObject marker;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Terrain");

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            //Debug.Log("hit !");
            Vector3 hitPositon = hit.point;
            //Debug.Log("hitPos : " + hitPositon);
            Vector2Int SquarePosition = MapGrid.Instance.GetSquare(hitPositon, false);
            //Debug.Log("SquarePos : "+SquarePosition);
            Vector3 center = MapGrid.Instance.getCenter(SquarePosition);
            //Debug.Log("center : " + center);
            marker.transform.position = center;
            //Debug.Log("setting height at : " + SquarePosition + " to : " + height);
            TerrainManager.Instance.SetHeight(SquarePosition.x, SquarePosition.y, height);
            TerrainManager.Instance.RegenChangedChunks();
        }
    }
}
