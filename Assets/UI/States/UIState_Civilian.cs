
namespace UIInGameStateMachine
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UIState_Civilian : UIStateInGame
    {
        [SerializeField] Canvas Canvas;

        public override void Enter(UIStateInGame from)
        {
            Canvas.gameObject.SetActive(true);
        }

        public override void Exit(UIStateInGame to)
        {
            if(
                to.GetType() == typeof(UIState_Escape) ||
                to.GetType() == typeof(UIState_Military) ||
                to.GetType() == typeof(UIState_Quit)
                )
            {
                Canvas.gameObject.SetActive(false);
            }

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIInGameStateMachine.Set(UIStateName.UIState_Escape);
            }
        }
    }
}