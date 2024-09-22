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

        var heights = TerrainManager.Instance.Heights;

        var heightsX = heights.GetLength(0);
        var heightsY = heights.GetLength(1);

        byte[] heightsBinary = BinaryShenanigans.Float2DArrayToBinary(heights);


        string terrainHeightsData = System.Convert.ToBase64String(heightsBinary);


        byte[] back = System.Convert.FromBase64String(terrainHeightsData);



        int gridDimensionX = MapGrid.Instance.DimensionX;
        int gridDimensionY = MapGrid.Instance.DimensionY;
        int margin = MapGrid.Instance.Margin;


        var jsonObject = new SaveData
        (
            City.Instance.Name,
            PlayTime.Instance.PlayTime_ms,

            terrainHeightsData,
            heightsX,
            heightsY,

            gridDimensionX,
            gridDimensionY,
            margin
            
        );
        //Debug.Log("terrainHeightsData after putting it in the obj/4 : " + jsonObject.terrainHeightsBinary.Length/4);


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
        try
        {
            if (!File.Exists(path))
            {
                Debug.LogError("could not load save at " + path + " : file does not exist");
                return false;
            }

            string text;
            if (!ScuffedCompression.ReadCompressed(path, out text))
            {
                return false;
            }

            SaveData data = JsonUtility.FromJson<SaveData>(text);

            //Debug.Log("lol, lmao : " + data.terrainHeightsBinary.Substring(data.terrainHeightsBinary.Length-100,99));

            //Debug.Log("length of string/4 : " + data.terrainHeightsBinary.Length/4);



            byte[] heightsBinary = System.Convert.FromBase64String(data.terrainHeightsBinary);

            //Debug.Log("length binary : " + heightsBinary.Length);

            MapGrid.Instance.DimensionX = data.gridDimensionX;
            MapGrid.Instance.DimensionY = data.gridDimensionY;
            MapGrid.Instance.Margin = data.gridMargin;



            int heightsDimensionX = data.terrainheightsX;
            int heightsDimensionY = data.terrainheightsY;

            float[,] heights = BinaryShenanigans.BinaryToFloat2DArray(heightsBinary, heightsDimensionX, heightsDimensionY);

            TerrainManager.Instance.Generate(heights);

            PlayTime.Instance.Initialize(data.playTime_ms);

            City.Instance.Name = data.cityName;

            //Debug.Log("loaded : "+MapGrid.Instance.DimensionX+", "+MapGrid.Instance.DimensionY);

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Load error : " + ex);
            return false;
        }
      
    }

    private void Generate()
    {
        City.Instance.Name = StaticSaveDirections.cityName;

        MapGrid.Instance.DimensionX = StaticSaveDirections.dimensionX;
        MapGrid.Instance.DimensionY = StaticSaveDirections.dimensionY;
        MapGrid.Instance.Margin = StaticSaveDirections.margin;


        TerrainManager.Instance.Seed = StaticSaveDirections.seed;
        TerrainManager.Instance.Generate();

        PlayTime.Instance.Initialize(0);

        //Debug.Log("generated : " + MapGrid.Instance.DimensionX + ", " + MapGrid.Instance.DimensionY);
    }
}
