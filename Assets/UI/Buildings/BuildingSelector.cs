namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;

    public class BuildingSelector : MonoBehaviour
    {
        [SerializeField] private BuildingOptionsCreator creator;

        public static BuildingSelector Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }



        public event System.Action<BuildingCategoryOption,BuildingSO> OnCategoryOptionChanged;

        private BuildingCategoryOption categoryOption;
        public BuildingCategoryOption CategoryOption
        {
            get => categoryOption;
            private set
            {
                categoryOption = value;

                if(SelectedBuildingOptions.ContainsKey(categoryOption.BuildingCategory))
                {
                    OnCategoryOptionChanged?.Invoke(categoryOption, SelectedBuildingOptions[categoryOption.BuildingCategory]);
                }
                else
                {
                    OnCategoryOptionChanged?.Invoke(categoryOption, null);
                }
            }
        }

        public void SetCategoryOption(BuildingCategoryOption categoryOption)
        {
            if(CategoryOption != null)
            {
                CategoryOption.Selected = false;
            }
            CategoryOption = categoryOption;

            CategoryOption.Selected = true;

            if (SelectedBuildingOptions.ContainsKey(categoryOption.BuildingCategory))
            {
                var SO = SelectedBuildingOptions[categoryOption.BuildingCategory];
                foreach(var GO in creator.BuildingOptionsGOS)
                {
                    var option = GO.GetComponent<BuildingOption>();
                    if(option.BuildingSO == SO)
                    {
                        SetSelectedBuildingOption(option);
                        break;
                    }
                }
            }
            else
            {
                SetSelectedBuildingOption(null);
            }





            BuildingCategory category = categoryOption.BuildingCategory;
            //Debug.Log("selecting : " + category.name);
        }

        public event System.Action<BuildingOption> OnBuildingOptionChanged;

        private BuildingOption buildingOption;
        public BuildingOption BuildingOption
        {
            get => buildingOption;
            private set
            {
                buildingOption = value;
                OnBuildingOptionChanged?.Invoke(buildingOption);
            }
        }

        public void SetSelectedBuildingOption(BuildingOption buildingOption)
        {
            if (BuildingOption != null)
            {
                BuildingOption.Selected = false;
            }

            if(buildingOption == null)
            {
                return;
            }

            buildingOption.Selected = true;

            SelectedBuildingOptions.Remove(categoryOption.BuildingCategory);
            SelectedBuildingOptions.Add(categoryOption.BuildingCategory,buildingOption.BuildingSO);

            //var building = buildingOption.BuildingSO;
            //Debug.Log("selecting : "+building.name);

            BuildingOption = buildingOption;
        }

        private Dictionary<BuildingCategory,BuildingSO> SelectedBuildingOptions =  new Dictionary<BuildingCategory, BuildingSO>();
    }
}


