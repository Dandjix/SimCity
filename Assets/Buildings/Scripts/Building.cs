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

        /// <summary>
        /// the anchor in float values.
        /// </summary>
        public Vector2 Anchor
        {
            get
            {
                return new Vector2(Mathf.Round(anchorTransform.position.x),Mathf.Round(anchorTransform.position.z));
            }
        }

        private void Awake()
        {
            footprint = GetComponent<Footprint>();
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.green;

            Vector3 anchorPosition = new Vector3(Anchor.x, transform.position.y, Anchor.y);

            Handles.DrawSolidDisc(anchorPosition, Vector3.up, 0.5f);
        }

    }

}
