namespace IngameUI
{
    using Buildings;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BuildingOption : MonoBehaviour
    {
        [SerializeField] private GameObject outline;
        [SerializeField] private Image image;
        [SerializeField] private Button button;

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
            BuildingSelector.Instance.SetSelectedBuilding(this);
        }

        private void UpdateAppearance()
        {
            if(buildingSO == null)
                return;
            image.sprite = buildingSO.menuIcon;
        }

        private void UpdateIsSelected()
        {
            outline.SetActive(selected);
        }
    }

}
