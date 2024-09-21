using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_CivilianRoads : UIStateInGame
{
    [SerializeField] private GameObject RoadSelectionManager;
    //[SerializeField] private SelectionRenderer selectionRenderer;
    //[SerializeField] private SelectionLine selectionLine;

    public override void Enter(UIStateInGame from)
    {
        RoadSelectionManager.SetActive(true);
    }

    public override void Exit(UIStateInGame to)
    {
        RoadSelectionManager.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIInGameStateMachine.Set(UIInGameStateMachine.UIState_Civilian);
        }
    }
}
