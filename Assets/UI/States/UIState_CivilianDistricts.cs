using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_CivilianDistricts : UIStateInGame
{
    public override void Enter(UIStateInGame from)
    {
        //throw new System.NotImplementedException();
    }

    public override void Exit(UIStateInGame to)
    {
        //throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIInGameStateMachine.Set(UIInGameStateMachine.UIState_Civilian);
        }
    }
}
