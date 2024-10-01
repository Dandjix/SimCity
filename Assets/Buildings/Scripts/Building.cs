namespace Buildings
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Footprint))]
    public class Building : MonoBehaviour
    {
        public BuildingSO BuildingSO;

        public Footprint footprint {  get; private set; }

        private void Awake()
        {
            footprint = GetComponent<Footprint>();
        }


    }

}
