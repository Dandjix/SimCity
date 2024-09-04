using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] [Min(1)] private float speed;
    [SerializeField] private new Camera camera;
    [SerializeField] [Range(0,1)] private float progress;
    public float Progress { get { return progress; } }

    [SerializeField] Vector3 zoomedOutPosition;
    [SerializeField] Quaternion zoomedOutRotation;

    [SerializeField] Vector3 zoomedInPosition;
    [SerializeField] Quaternion zoomedInRotation;
 
    void Start()
    {
        ApplyZoom();
    }

    // Update is called once per frame
    void Update()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        if (scrollInput == 0)
        {
            return;
        }
        scrollInput =  scrollInput * speed * Time.deltaTime;
        /*        Debug.Log("scrollInput : " + scrollInput);*/
        progress = Mathf.Clamp(progress+scrollInput,0,1);

        ApplyZoom();
    }

    void ApplyZoom()
    {
        Vector3 position = Vector3.Lerp(zoomedOutPosition, zoomedInPosition, progress);
        Quaternion rotation = Quaternion.Lerp(zoomedOutRotation, zoomedInRotation, progress);

        camera.transform.rotation = rotation;
        camera.transform.localPosition = position;
    }
}
