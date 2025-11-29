using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    private string SavePath;


     void Awake()
    {
        SavePath = Application.persistentDataPath + "/save.json";
    }
    public void save(GameState Data)
    {

        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(SavePath, json);
    }

    public GameState LoadData()
    {

        if (File.Exists(SavePath)) { 
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<GameState>(json);
       
        }

        return null;
    }

}

