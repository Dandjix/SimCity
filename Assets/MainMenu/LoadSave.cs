using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSave : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas saveSelection;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(SwitchToSaveSelection);
    }

    private void SwitchToSaveSelection()
    {
        mainCanvas.gameObject.SetActive(false);
        saveSelection.gameObject.SetActive(true);
    }
}
