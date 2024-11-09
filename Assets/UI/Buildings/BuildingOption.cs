namespace IngameUI
{
    using Buildings;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class BuildingOption : MonoBehaviour
    {
        [SerializeField] private GameObject outline;
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buildingName;

        [SerializeField]
        private BuildingSO buildingSO;
        public BuildingSO BuildingSO
        {
            get
            {
                return buildingSO;
            }
            set
            {
                buildingSO = value;
                UpdateAppearance();
            }
        }

        private bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                //Debug.Log("Selected : "+buildingSO.name);
                selected = value;
                UpdateIsSelected();
            }
        }

        private void Start()
        {
            UpdateAppearance();
            UpdateIsSelected();

            button.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            //Debug.Log("clicked : " + buildingSO.name);
            BuildingSelector.Instance.SetSelectedBuildingOption(this);
        }

        private void UpdateAppearance()
        {
            if(buildingSO == null)
                return;
            image.sprite = buildingSO.menuIcon;
            buildingName.text = buildingSO.name;
        }

        private void UpdateIsSelected()
        {
            outline.SetActive(selected);
        }
    }

}
