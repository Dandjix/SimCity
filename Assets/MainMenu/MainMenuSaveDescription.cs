using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuSaveDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text cityName;
    [SerializeField] private TMP_Text playTime;
    [SerializeField] private Image cityPreview;

    public string SavePath
    {
        set
        {
            if(value == "" || value==null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
        }
    }
}
