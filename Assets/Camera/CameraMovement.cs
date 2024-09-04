using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float zoomBoost = 50;


    [SerializeField] Vector2 minCoordinates;
    [SerializeField] Vector2 maxCoordinates;

    private CameraZoom zoom;

    // Start is called before the first frame update
    private void Awake()
    {
        zoom = GetComponent<CameraZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            delta += (Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            delta += (Vector3.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            delta += (Vector3.back);
        }
        if (Input.GetKey(KeyCode.D))
        {
            delta += (Vector3.right);
        }

        float zoomSpeedIncrease = (1 - zoom.Progress) * zoomBoost;

        Vector3 from = transform.position;

        Vector3 to = transform.position + delta * ((speed + zoomSpeedIncrease) * Time.deltaTime);

        transform.position = MapGrid.Instance.bounded(from,to);
    }
}
