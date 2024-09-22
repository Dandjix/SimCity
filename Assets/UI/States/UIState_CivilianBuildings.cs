namespace UIInGameStateMachine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

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
    }

}