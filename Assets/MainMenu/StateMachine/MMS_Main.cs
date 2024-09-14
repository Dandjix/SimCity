using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMS_Main : MainMenuState
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private Button NewCityButton;
    [SerializeField] private Button SavesButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button QuitButton;

    public override void Enter(MainMenuState from)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void Exit(MainMenuState to)
    {
        canvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        NewCityButton.onClick.AddListener(() => MainMenuStateMachine.Set(MMStateName.New));
        SavesButton.onClick.AddListener(() => MainMenuStateMachine.Set(MMStateName.Saves));
        OptionsButton.onClick.AddListener(() => MainMenuStateMachine.Set(MMStateName.Options));
        QuitButton.onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
