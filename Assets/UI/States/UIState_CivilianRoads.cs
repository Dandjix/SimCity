using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_CivilianRoads : UIState
{
    public override void Enter(UIState from)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(UIState to)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIStateMachine.Set(UIStateMachine.UIState_Civilian);
        }
    }
}
