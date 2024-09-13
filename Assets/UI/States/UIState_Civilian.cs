using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_Civilian : UIState
{
    [SerializeField] Canvas Canvas;

    public override void Enter(UIState from)
    {
        Canvas.gameObject.SetActive(true);
    }

    public override void Exit(UIState to)
    {
        Canvas.gameObject.SetActive(false);
    }
}
