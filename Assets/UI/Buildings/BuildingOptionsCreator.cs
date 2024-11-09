
namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;

    public class BuildingOptionsCreator : MonoBehaviour
    {
        [SerializeField] private GameObject buildingOptionGO;

        private List<GameObject> buildingOptionGOs = new List<GameObject>();
        public IReadOnlyList<GameObject> BuildingOptionsGOS { get =>  buildingOptionGOs; }

        private void OnEnable()
        {
            BuildingSelector.Instance.OnCategoryOptionChanged += UpdateOptions;
        }

        private void OnDisable()
        {
            BuildingSelector.Instance.OnCategoryOptionChanged -= UpdateOptions;
        }

        private void UpdateOptions(BuildingCategoryOption category,BuildingSO selectedSO)
        {
            var SOs = BuildingSO.GetAll(category.BuildingCategory);

            //Debug.Log("updating options. There are : " + SOs.Length);

            foreach (var optionGO in buildingOptionGOs)
            {
                Destroy(optionGO);
            }
            buildingOptionGOs.Clear();

            foreach (BuildingSO SO in SOs)
            {
                var newOption = Instantiate(buildingOptionGO,transform);

                newOption.GetComponent<BuildingOption>().BuildingSO = SO;
                buildingOptionGOs.Add(newOption);

                if(SO == selectedSO)
                {
                    BuildingSelector.Instance.SetSelectedBuildingOption(newOption.GetComponent<BuildingOption>());
                }
            }
        }
    }

}

