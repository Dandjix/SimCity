using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMS_Saves : MainMenuState
{
    [SerializeField] private Canvas canvas;



    public override void Enter(MainMenuState from)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void Exit(MainMenuState to)
    {
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuStateMachine.Set(MMStateName.Main);
        }
    }
}
