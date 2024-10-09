namespace IngameUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Buildings;
    using UIInGameStateMachine;

    public class CategoryButton : MonoBehaviour
    {
        [SerializeField]private Button button;

        public BuildingCategory buildingCategory;

        private void Awake()
        {
            button.onClick.AddListener(Switch);
        }

        private void Switch()
        {
            UIInGameStateMachine.UIState_CivilianBuildings.SelectCategory(buildingCategory);
        }
    }

}
