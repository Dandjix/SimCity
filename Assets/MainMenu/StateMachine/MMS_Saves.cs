using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMS_Saves : MainMenuState
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject saveListItemModel;

    private GameObject[] savesGameObjects = new GameObject[0];

    [SerializeField] private GameObject saveDescription;
    [SerializeField] private TMP_Text toLoadCityName;
    [SerializeField] private TMP_Text toLoadPlayTime;
    [SerializeField] private Image toLoadImage;

    [SerializeField] private Button loadButton;
    [SerializeField] private Button deleteButton;

    [SerializeField] private ConfirmSaveDeletion confirmDeletion;

    private void Start()
    {
        loadButton.onClick.AddListener(Load);
        deleteButton.onClick.AddListener(DeleteButtonClicked);
        SelectedSaveUpdated();

        confirmDeletion.onConfirm += () =>
        {
            confirmDeletion.gameObject.SetActive(false);
            Delete();
        };

        confirmDeletion.onCancel += () =>
        {
            confirmDeletion.gameObject.SetActive(false);
        };
    }

    private void Load()
    {
        if(!File.Exists(selectedPath))
        {
            Debug.LogError("could not load path at " + selectedPath);
            ReadListedSaves();
            return;
        }

        StaticSaveDirections.createNew = false;
        StaticSaveDirections.savePath = selectedPath;

        SceneManager.LoadScene("Game");
    }

    private void DeleteButtonClicked()
    {
        if(confirmDeletion.dontAsk)
        {
            Delete();
            return;
        }

        confirmDeletion.SaveName = SaveNameManipulation.GetSaveName(selectedPath);
        confirmDeletion.gameObject.SetActive(true);
    }

    private void Delete()
    {
        if (!File.Exists(selectedPath))
        {
            Debug.LogError("could not delete at " + selectedPath);
            ReadListedSaves();
            return;
        }

        File.Delete(selectedPath);
        ReadListedSaves();
    }

    private string selectedPath;
    public string SelectedPath
    {
        get => selectedPath;
        set
        {
            selectedPath = value;
            SelectedPathUpdated(value);
        }
    }

    private void SelectedPathUpdated(string value)
    {
        foreach (var gameObject in savesGameObjects)
        {
            var item = gameObject.GetComponent<SaveListItem>();
            item.Selected = item.Path == value;
        }
        if (value != null && value != "")
        {
            string text;
            if(ScuffedCompression.ReadCompressed(value,out text))
            {
                SelectedSave = JsonUtility.FromJson<SaveData>(text);
            }

        }
    }

    private SaveData selectedSave;
    private SaveData SelectedSave { get => selectedSave; set
        {
            selectedSave = value;
            SelectedSaveUpdated();
        } }

    private void SelectedSaveUpdated()
    {
        if(selectedSave == null)
        {
            saveDescription.SetActive(false);
            return;
        }
        saveDescription.SetActive(true);

        toLoadCityName.text = selectedSave.cityName;
        toLoadPlayTime.text = playTimeToString(selectedSave.playTime_ms);

        for (int i = 0; i < savesGameObjects.Length; i++)
        {
            var saveGO = savesGameObjects[i];
            var item = saveGO.GetComponent<SaveListItem>();
            if(item.Path == selectedPath)
            {
                item.Selected = true;
            }
            else
            {
                item.Selected = false;
            }
        }

    }

    private string playTimeToString(int playTime_ms)
    {
        var timeStamp = TimeSpan.FromMilliseconds(playTime_ms);
        string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
            timeStamp.Hours,
            timeStamp.Minutes,
            timeStamp.Seconds,
            timeStamp.Milliseconds);

        return answer;
    }

    public override void Enter(MainMenuState from)
    {
        ReadListedSaves();
        canvas.gameObject.SetActive(true);
    }

    public override void Exit(MainMenuState to)
    {
        canvas.gameObject.SetActive(false);
        SelectedSave = null;
    }

    private void EmptyListedSaves()
    {
        List<GameObject> saveGos = new List<GameObject>();
        for (int i = 0; i < savesGameObjects.Length; i++)
        {
            saveGos.Add(savesGameObjects[i]);
        }

        foreach (GameObject item in saveGos)
        {
            Destroy(item);
        }

        //int childrenCount = scrollContent.childCount;
        //List<Transform> children = new List<Transform>(childrenCount);
        //for (int i = 0; i < childrenCount; i++)
        //{
        //    children.Add(scrollContent.transform.GetChild(i));
        //}
        //foreach (Transform child in children)
        //{
        //    Destroy(child.gameObject);
        //}
    }

    private void ReadListedSaves()
    {
        //Debug.Log("reading listed saves ! ");

        SelectedSave = null;
        EmptyListedSaves();

        string dir = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var files = Directory.GetFiles(dir);

        var saves = files.Where((x) => x.EndsWith(".scsave")).ToArray<string>();

        savesGameObjects = new GameObject[saves.Length];

        for (int i = 0; i < saves.Length; i++)
        {
            //Debug.Log("creating object for : " + saves[i]);
            savesGameObjects[i] = CreateSaveGameObject(saves[i]);
        }
    }

    private GameObject CreateSaveGameObject(string save)
    {
        GameObject gameObject = Instantiate(saveListItemModel);

        var saveListItem = gameObject.GetComponent<SaveListItem>();
        saveListItem.Path = save;

        gameObject.transform.SetParent(scrollContent,false);

        return gameObject;
    }

    private void Update()
    {
        if (confirmDeletion.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                confirmDeletion.gameObject.SetActive(false);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuStateMachine.Set(MMStateName.Main);
        }

    }
}
