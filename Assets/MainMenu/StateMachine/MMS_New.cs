using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MMS_New : MainMenuState
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button createButton;

    [SerializeField] private TMP_Text sizeXDisplay;
    [SerializeField] private TMP_Text sizeYDisplay;

    [SerializeField] private Slider sizeXSlider;
    [SerializeField] private Slider sizeYSlider;


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
        createButton.onClick.AddListener(CreateCity);
        sizeXSlider.onValueChanged.AddListener(OnXSizeChanged);
        sizeYSlider.onValueChanged.AddListener(OnYSizeChanged);

    }

    private void OnXSizeChanged(float value)
    {
        sizeXDisplay.text = ((int)value).ToString();
    }
    private void OnYSizeChanged(float value)
    {
        sizeYDisplay.text = ((int)value).ToString();
    }

    private void CreateCity()
    {
        StaticSaveDirections.createNew = true;
        StaticSaveDirections.dimensionX = (int)sizeXSlider.value;
        StaticSaveDirections.dimensionY = (int)sizeYSlider.value;

        SceneManager.LoadScene("Game");
    }
}
