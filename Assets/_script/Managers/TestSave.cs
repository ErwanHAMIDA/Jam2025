using System;
using System.Collections.Generic;
using System.IO;
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
    private string _savePath;
    private GameState _gameState;

    void Start()
    {
        _savePath = Application.persistentDataPath + "/save.json";
        _gameState = new GameState();

        _gameState.AddValueAndScene("Kiwistiti Shop", 999);
        _gameState.AddValueAndScene("Kiwiscctiti Shop", 9979);
        _gameState.AddValueAndScene("Kiwistccciti Shop", 9199);
    }
    public void Save()
    {
        _gameState.AddValueAndScene("cs", 1);

        string json = JsonConvert.SerializeObject(_gameState, Formatting.Indented);
        File.WriteAllText(_savePath, json);

        Debug.Log($"Sauvegarde réussie : {_savePath}");
    }


    public GameState LoadData()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            return JsonConvert.DeserializeObject<GameState>(json);
        }

        return null;
    }
}
