namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [RequireComponent(typeof(Footprint))]
    public class Building : MonoBehaviour
    {
        public BuildingSO BuildingSO;
        //{ get => 
        //    {
        //        return new BuildingSO(this);
        //    } 
        //}

        public Footprint footprint {  get; private set; }

        [SerializeField]
        private Transform anchorTransform;
        //private Transform AnchorTransform { get => anchorTransform; }

        public Vector2 Anchor
        {
            get
            {
                return new Vector2(anchorTransform.position.x, anchorTransform.position.z);
            }
        }

        private void Awake()
        {
            footprint = GetComponent<Footprint>();
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.green;
            Handles.DrawSolidDisc(anchorTransform.transform.position, Vector3.up, 0.5f);
        }

    }

}
