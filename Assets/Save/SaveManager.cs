using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {  get; private set; }

    private void Start()
    {
        Instance = this;

        if (StaticSaveDirections.createNew)
        {
            City.Instance.Name = "Folsom";

            MapGrid.Instance.DimensionX = StaticSaveDirections.dimensionX;
            MapGrid.Instance.DimensionY = StaticSaveDirections.dimensionY;
            MapGrid.Instance.Margin = StaticSaveDirections.margin;

            Debug.Log("Dim x : "+ MapGrid.Instance.DimensionX);
            Debug.Log("Dim y : " + MapGrid.Instance.DimensionY);
            Debug.Log("margin : " + MapGrid.Instance.Margin);

            int seed = StaticSaveDirections.seed;
            TerrainManager.Instance.Seed = seed;
            TerrainManager.Instance.Generate();


        }
        else
        {
            //MapGrid.Instance.DimensionX = StaticSaveDirections.dimensionX;
            //MapGrid.Instance.DimensionY = StaticSaveDirections.dimensionY;
            //MapGrid.Instance.Margin = StaticSaveDirections.margin;
        }
    }

    public bool Save()
    {
        string saveName = "save1.json";
        return Save(saveName);
    }

    public bool Save(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName;
        if (File.Exists(path))
        {
            Debug.LogError("could not save to " + saveName + " : it already exists");
            return false;
        }

        string data = JsonUtility.ToJson(TerrainManager.Instance.Heights);

        File.WriteAllText(path, data);
        return true;
    }

    private bool Load(string path)
    {
        return true;
    }
}
