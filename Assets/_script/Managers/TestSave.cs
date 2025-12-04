using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class GameState
{
    public Dictionary<string, int> valuesAndScene = new Dictionary<string, int>();

    public void AddValueAndScene(string key, int value)
    {
        valuesAndScene.Add(key, value);
        Debug.Log(valuesAndScene.Count);
    }
}

public class TestSave : MonoBehaviour
{
    private string SavePath;
    private GameState gameState;

    void Start()
    {
        SavePath = Application.persistentDataPath + "/save.json";
        gameState = new GameState();
        gameState.AddValueAndScene("Kiwistiti Shop", 999);
        gameState.AddValueAndScene("Kiwiscctiti Shop", 9979);
        gameState.AddValueAndScene("Kiwistccciti Shop", 9199);
    }
    public void Save()
    {
        gameState.AddValueAndScene("cs", 1);

        string json = JsonConvert.SerializeObject(gameState, Formatting.Indented);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Sauvegarde réussie : {SavePath}");
    }


    public GameState LoadData()
    {

        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonConvert.DeserializeObject<GameState>(json);
        }

        return null;
    }

}
