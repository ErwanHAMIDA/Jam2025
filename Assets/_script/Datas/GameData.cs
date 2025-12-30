using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SerializableInventoryItem
{
    public string IngredientId;
    public int Quantity;
}

public class GameData
{
    private static GameData _instance;
    public static GameData Instance => _instance ??= new GameData();
    [JsonProperty] public int Gold { get; private set; }
    [JsonProperty] public string Name { get; private set; }

    [JsonIgnore]
    public Dictionary<IngredientData, int> Inventory { get; private set; }

    private UITextManager _uiTextManager;
    private string _inputBarName;

    public GameData()
    {
        Inventory = new Dictionary<IngredientData, int>();
    }

    public void SetUIManager(UITextManager uiManager)
    {
        _uiTextManager = uiManager;
        _uiTextManager.UpdateUIText();
    }

    public void AddItem(IngredientData ingredient, int number)
    {
        Inventory.Add(ingredient, number);
    }

    public void CopyFrom(GameData loaded)
    {
        SetGold(loaded.Gold);
        SetName(loaded.Name);
        SetInventory(loaded.Inventory);
    }

    public void SetDataByDefault(int gold, IngredientData[] ingredients, int[] quantity)
    {
        if (ingredients.Length != quantity.Length)
            throw new ArgumentOutOfRangeException("ingredients and quantity not the same count");

        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null) // <-- Vérifie que l'ingrédient n'est pas null
                AddItem(ingredients[i], quantity[i]);
        }

        SetGold(gold);
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

    public void SetInventory(Dictionary<IngredientData, int> inventory)
    {
        Inventory = new Dictionary<IngredientData, int>(inventory);
    }

}
