using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmSaveDeletion : MonoBehaviour
{
    [SerializeField] private TMP_Text saveName;

    [SerializeField] private Button confirm;
    [SerializeField] private Button cancel;
    [SerializeField] private Toggle dontAskToggle;

    public string SaveName { set 
        { 
            saveName.text = value;
        } }

    public bool dontAsk = false;

    public System.Action onConfirm;
    public System.Action onCancel;

    private void Start()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        if (dontAskToggle.isOn)
        {
            dontAsk = true;
        }
        onConfirm?.Invoke();
    }
    private void Cancel()
    {
        onCancel?.Invoke();
    }

}
