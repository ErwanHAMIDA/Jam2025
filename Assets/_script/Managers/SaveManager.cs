using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    private string _savePath;

    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _savePath = Application.persistentDataPath + "/save.json";
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(GameData.Instance, Formatting.Indented);
        File.WriteAllText(_savePath, json);

        Debug.Log($"Sauvegarde réussie : {_savePath}");
    }

    public GameData LoadData()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            return JsonConvert.DeserializeObject<GameData>(json);
        }

        return null;
    }
}