using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIState_Quit : UIStateInGame
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private Button saveAndQuit;
    [SerializeField] private Button quitWithoutSaving;

    public bool toDesktop = false;

    private void Start()
    {
        saveAndQuit.onClick.AddListener(SaveAndQuit);
        quitWithoutSaving.onClick.AddListener(QuitWithoutSaving);
    }

    private void SaveAndQuit()
    {
        if (toDesktop)
        {
            Quit();
        }
        else
        {
            LoadMainMenu();
        }
    }

    private void QuitWithoutSaving()
    {
        if (toDesktop)
        {
            Quit();
        }
        else
        {
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Quit()
    {
        Debug.Log("quitting ...");
        Application.Quit();
        Debug.Log("done !");
    }

    public override void Enter(UIStateInGame from)
    {
        canvas.gameObject.SetActive(true);
    }

    public override void Exit(UIStateInGame to)
    {
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIInGameStateMachine.Set(UIStateName.UIState_Escape);
        }
    }
}
