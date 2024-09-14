using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMS_New : MainMenuState
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button createButton;


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
        createButton.onClick.AddListener(CreateCity)
    }

    private void CreateCity()
    {
        SceneManager.LoadScene("Game");
    }
}
