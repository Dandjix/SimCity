namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Buildings;
    using UIInGameStateMachine;

    public class CategoryOption : MonoBehaviour
    {
        [SerializeField]private Button button;

        [SerializeField] private Image image;

        private BuildingCategory buildingCategory;
        public BuildingCategory BuildingCategory
        {
            get { return buildingCategory; }
            set {
                buildingCategory = value;
                UpdateAppearance();
            }
        }

        private bool selected;
        public bool Selected 
        { get 
            {
                return selected;
            }
            set
            {
                button.interactable = !value;
                selected = value;
            }
        }

        private void Awake()
        {
            button.onClick.AddListener(Switch);
        }

        private void Switch()
        {
           BuildingSelector.Instance.SetCategory(this);
        }

        private void UpdateAppearance()
        {
            //if (BuildingCategory == null)
            //{
            //    Debug.LogError("set building category to null");
            //    return;
            //}
            image.sprite = BuildingCategory.icon;
            image.color = BuildingCategory.color;
        }
    }

}
