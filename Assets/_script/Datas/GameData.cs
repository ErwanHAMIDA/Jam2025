using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameData
{
    private static GameData _instance;
    public static GameData Instance => _instance ??= new GameData();
    [JsonProperty] public int Gold { get; private set; }
    [JsonProperty] public string Name { get; private set; }
    [JsonProperty] public Dictionary<Ingredient, int> Inventory { get; private set; }

    private UITextManager _uiTextManager;
    private string _inputBarName;

    public void SetUIManager(UITextManager uiManager)
    {
        _uiTextManager = uiManager;
        _uiTextManager.UpdateUIText();
    }

    public void AddItem(Ingredient ingredient, int number)
    {
        Inventory.Add(ingredient, number);
    }

    public void CopyFrom(GameData loaded)
    {
        SetGold(loaded.Gold);
        SetName(loaded.Name);
    }

    public void SetDataByDefault(int gold , Ingredient[] ingredients, int[] quantity)
    {
        if (ingredients.Length != quantity.Length)
            throw new ArgumentOutOfRangeException("ingredients and quantity not the same count", nameof(ingredients.Length) + nameof(quantity.Length));

        for (int i = 0; i < ingredients.Length; i++)
        {
            AddItem(ingredients[i], quantity[i]);
        }

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
