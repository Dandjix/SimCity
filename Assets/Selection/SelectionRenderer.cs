using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SelectionRenderer : MonoBehaviour
{
    //public static SelectionRenderer Instance { get; private set; }

    //private void Start()
    //{
    //    Instance = this;
    //}

    [SerializeField] private float height = 25;

    [SerializeField] private SelectionLine selectionLine;


    [SerializeField] private DecalProjector projector;



    private void Start()
    {
        //Debug.Log("enablin");
        //projector?.gameObject.SetActive(true);
        selectionLine.OnSelectionChanged += SelectionChanged;
        selectionLine.OnSelectionMade += SelectionMade;
    }

    private void OnDestroy()
    {
        selectionLine.OnSelectionChanged -= SelectionChanged;
        selectionLine.OnSelectionMade -= SelectionMade;
    }

    private void SelectionChanged(LineData data)
    {
        if (data == null)
        {
            //Debug.Log("called the ball");
            projector.gameObject.SetActive(false);
            return;
        }
        projector.gameObject.SetActive(true);

        //Debug.Log("selection changed : "+data.from+", "+data.to);

        Vector3 fromV3 = new Vector3(data.from.x, 0, data.from.y);
        Vector3 toV3 = new Vector3(data.to.x, 0, data.to.y);

        Vector3 center = Vector3.Lerp(fromV3, toV3,0.5f);

        Vector2 size = new Vector2(Mathf.Abs(fromV3.x - toV3.x), Mathf.Abs(fromV3.z - toV3.z));

        Rectangle(center, size);
    }

    private void SelectionMade(LineData data)
    {
        //Debug.Log("selection made.");
    }

    private void Rectangle(Vector3 center, Vector2 size)
    {
        //Debug.Log("rectangle : " + center + ", size : " + size);

        projector.transform.position = new Vector3(center.x+0.5f, height,center.z+0.5f);

        projector.size = new Vector3(size.x+1, size.y+1, projector.size.z );
    }
}
