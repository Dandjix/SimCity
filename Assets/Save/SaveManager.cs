using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {  get; private set; }

    private void Start()
    {
        Instance = this;

        if (StaticSaveDirections.createNew)
        {
            Generate();
        }
        else
        {
            if(!Load(StaticSaveDirections.savePath))
            {
                Debug.LogError("failed to load save. Returning to main menu.");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private string formattedTimeStamp()
    {
        System.DateTime dt = System.DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd-hh-mm-ss");
        return time;
    }

    public bool Save()
    {
        string time = formattedTimeStamp();
        string saveName = City.Instance.name+"("+ time + ").scsave";
        bool success = Save(saveName);
        //bool success = Save("save2.json");
        return success;
    }


    public bool Save(string saveName)
    {
        string path = Application.persistentDataPath + "/saves/" + saveName;


        if(!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        if (File.Exists(path))
        {
            Debug.LogError("could not save to " + saveName + " : it already exists");
            return false;
        }

        //return false;



        string terrainHeightsData = System.Convert.ToBase64String(BinaryShenanigans.FloatDoubleArrayToBinary(TerrainManager.Instance.Heights));

        int gridDimensionX = MapGrid.Instance.DimensionX;
        int gridDimensionY = MapGrid.Instance.DimensionY;
        int margin = MapGrid.Instance.Margin;

        var jsonObject = new SaveData
        {
            cityName = City.Instance.Name,
            terrainHeightsBinary = terrainHeightsData,
            gridDimensionX = gridDimensionX,
            gridDimensionY = gridDimensionY,
            margin = margin
            
        };

        string jsonString = JsonUtility.ToJson(jsonObject);

        bool success = ScuffedCompression.WriteCompressed(path, jsonString);

        //File.WriteAllText(path, jsonString);
        if (success)
        {
            Debug.Log("written data to : "+path);
        }

        return success;
    }

    private bool Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("could not load save at "+path+" : file does not exist");
            return false;
        }

        string text;
        if(!ScuffedCompression.ReadCompressed(path,out text))
        {
            return false;
        }

        SaveData data = JsonUtility.FromJson<SaveData>(text);

        byte[] heightsBinary = Encoding.UTF8.GetBytes(data.terrainHeightsBinary);




        MapGrid.Instance.DimensionX = data.gridDimensionX;
        MapGrid.Instance.DimensionY = data.gridDimensionY;
        MapGrid.Instance.Margin = data.margin;

        int heightsDimensionX = data.gridDimensionX + 3;
        int heightsDimensionY = data.gridDimensionY + 3;
        float[,] heights = BinaryShenanigans.BinaryToDoubleFloatArray(heightsBinary, heightsDimensionX, heightsDimensionY);

        TerrainManager.Instance.Generate(heights);

        PlayTime.Instance.Initialize(data.playTime_ms);

        return true;
    }

    private void Generate()
    {
        //Debug.Log("city : " + City.Instance);
        City.Instance.Name = "Folsom";

        MapGrid.Instance.DimensionX = StaticSaveDirections.dimensionX;
        MapGrid.Instance.DimensionY = StaticSaveDirections.dimensionY;
        MapGrid.Instance.Margin = StaticSaveDirections.margin;
        TerrainManager.Instance.Seed = StaticSaveDirections.seed;

        TerrainManager.Instance.Generate();

        PlayTime.Instance.Initialize(0);
    }
}
