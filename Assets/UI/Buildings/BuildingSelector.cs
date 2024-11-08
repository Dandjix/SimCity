namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;

    public class BuildingSelector : MonoBehaviour
    {
        public static BuildingSelector Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private CategoryOption currentCategoryOption;

        public void SetCategory(CategoryOption categoryOption)
        {
            if(currentCategoryOption != null)
            {
                currentCategoryOption.Selected = false;
            }
            currentCategoryOption = categoryOption;

            currentCategoryOption.Selected = true;


            BuildingCategory category = categoryOption.BuildingCategory;
            //Debug.Log("selecting : " + category.name);
        }

        private BuildingOption currentBuildingOption;

        public void SetSelectedBuilding(BuildingOption buildingOption)
        {
            var building = buildingOption.BuildingSO;
            Debug.Log("selecting : "+building.name);
        }
    }
}


