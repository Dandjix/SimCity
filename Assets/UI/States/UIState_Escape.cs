using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIState_Escape : UIStateInGame
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private Button ExitToMainMenuButton;
    [SerializeField] private Button ExitToDesktopButton;
    private UIStateInGame from;

    private void Start()
    {
        ExitToMainMenuButton.onClick.AddListener(ExitToMainMenu);
        ExitToDesktopButton.onClick.AddListener(ExitToDesktop);
    }

    private void ExitToMainMenu()
    {
        UIInGameStateMachine.UIState_Quit.toDesktop = false;
        UIInGameStateMachine.Set(UIStateName.UIState_Quit);
    }

    private void ExitToDesktop()
    {
        UIInGameStateMachine.UIState_Quit.toDesktop = true;
        UIInGameStateMachine.Set(UIStateName.UIState_Quit);
    }

    public override void Enter(UIStateInGame from)
    {
        this.from = from;
        canvas.gameObject.SetActive(true);
    }

    public override void Exit(UIStateInGame to)
    {
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIInGameStateMachine.Set(from);
        }
    }
}
