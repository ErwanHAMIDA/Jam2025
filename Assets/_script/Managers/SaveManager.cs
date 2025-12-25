using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public class SavingStruct
    {
        const int SIZE = 4;
        public GameData[] savingSlot;

        public SavingStruct() { savingSlot = new GameData[SIZE]; }
    }


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
        SavingStruct currentSavedData = LoadAllData();

        if (currentSavedData == null) currentSavedData = new SavingStruct();
        currentSavedData.savingSlot[GameData.Instance.slotID] = GameData.Instance;
        string json = JsonConvert.SerializeObject(currentSavedData, Formatting.Indented);
        File.WriteAllText(_savePath, json);
    }

    public SavingStruct LoadAllData()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            return JsonConvert.DeserializeObject<SavingStruct>(json);
        }

        return null;
    }

    public GameData LoadSlotData(int slotID)
    {
        SavingStruct LoadedSlot = LoadAllData();
        if (LoadedSlot == null)
            return null;
        return LoadedSlot.savingSlot[slotID];
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

    public void DeleteSlotData(int slotID)
    {
        SavingStruct savedData = LoadAllData();
        if (savedData == null) return;
        File.Delete(_savePath);

        savedData.savingSlot[slotID] = null;
        string json = JsonConvert.SerializeObject(savedData, Formatting.Indented);
        File.WriteAllText(_savePath, json);
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