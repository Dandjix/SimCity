using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private string path;
    public string Path
    {
        get => path;
        set { 
            path = value;
            Text = GetSaveName(path);
        }
    }

    private bool selected;
    public bool Selected
    {
        get => selected;
        set
        {
            if (value)
            {
                text.color = Color.green;
            }
            else
            {
                text.color = Color.blue;
            }
            selected = value;
        }
    }

    private string Text
    {
        get => text.text;
        set => text.text = value;
    }

    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(Click);
    }
    private void Click()
    {
        MainMenuStateMachine.Saves.SelectedPath = path;
    }

    private string GetSaveName(string save)
    {
        var split = save.Split('\\');
        string name = split[split.Length - 1].Split('.')[0];
        return name;
    }
}
