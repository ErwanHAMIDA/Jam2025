using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class GameData
{
    private static GameData _instance;

    public int slotID { get; set; }
    public static GameData Instance => _instance ??= new GameData();
    [JsonProperty] public int Gold { get; private set; }
    [JsonProperty] public string Name { get; private set; }

    private UITextManager _uiTextManager;
    private string _inputBarName;

    public void SetUIManager(UITextManager uiManager)
    {
        _uiTextManager = uiManager;
        _uiTextManager.UpdateUIText();
    }

    public void CopyFrom(GameData loaded)
    {
        SetGold(loaded.Gold);
        SetName(loaded.Name);
        slotID = loaded.slotID;
    }

    public void SetDataByDefault()
    {
        SetGold(1000);
        SetName(_inputBarName);
        _uiTextManager.UpdateUIText();
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        _uiTextManager.UpdateUIText();
    }

    public void SetGold(int amount)
    {
        Gold = amount;
        _uiTextManager.UpdateUIText();
    }

    public void SetName(string name)
    {
        Name = name;
        _uiTextManager.UpdateUIText();
    }

    public void SetInputBarName(string name)
    {
        _inputBarName = name;
    }
}
