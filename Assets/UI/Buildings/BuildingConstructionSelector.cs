namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;

    public class BuildingConstructionSelector : MonoBehaviour
    {
        [SerializeField] private GameObject categoryButton;

        void Start()
        {
            BuildingCategory[] categories = BuildingCategory.getAll();
        }

        public void LoadCategory(BuildingCategory category)
        {

        }
    }
}


