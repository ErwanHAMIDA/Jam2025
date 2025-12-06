using Newtonsoft.Json;
using System.IO;
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
            DontDestroyOnLoad(gameObject);

            // Initialiser le chemin immédiatement dans Awake
            _savePath = Path.Combine(Application.persistentDataPath, "save.json");

            Debug.Log($"SaveManager initialisé. Chemin : {_savePath}");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(GameData.Instance, Formatting.Indented);
        File.WriteAllText(_savePath, json);
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

    public void DeleteData()
    {
        if (File.Exists(_savePath))
            File.Delete(_savePath);
    }

    public bool IsSaveExist()
    {
        return File.Exists(_savePath);
    }
}