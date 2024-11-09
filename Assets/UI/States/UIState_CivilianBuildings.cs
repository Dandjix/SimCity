namespace UIInGameStateMachine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Buildings;
    using IngameUI;

    public class UIState_CivilianBuildings : UIStateInGame
    {
        [SerializeField] private Canvas canvas;

        public override void Enter(UIStateInGame from)
        {
            canvas.gameObject.SetActive(true);
        }

        public override void Exit(UIStateInGame to)
        {
            canvas.gameObject.SetActive(false);
        }

        private BuildingCategoryOption categoryButton;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIInGameStateMachine.Set(UIInGameStateMachine.UIState_Civilian);
            }
        }
    }

}