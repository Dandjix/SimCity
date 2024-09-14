using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMS_Options : MainMenuState
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
}
