using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_CivilianTerrain : UIStateInGame
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private HeightSetter heightSetter;

    public override void Enter(UIStateInGame from)
    {
        canvas.gameObject.SetActive(true);
        heightSetter.gameObject.SetActive(true);
    }

    public override void Exit(UIStateInGame to)
    {
        canvas.gameObject.SetActive(false);
        heightSetter.gameObject.SetActive(false);
    }

    private void Start()
    {
        heightSetter.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIInGameStateMachine.Set(UIInGameStateMachine.UIState_Civilian);
        }
    }
}
