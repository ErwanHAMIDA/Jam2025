using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public struct Item
{
    public string name;
    public string description;
    public int price;
    public Sprite thumbnail;
}
public class Shop : MonoBehaviour
{
    Dictionary<string, int> _buyableItems;
    GameObject _buttonPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _buyableItems = new Dictionary<string, int>() {
            { "ice",        99999 },
            { "mint",       99999 },
            { "orange",     99999 },
            { "lemon",      99999 },
            { "pepper",     99999 },
            { "gasoline",   99999 },
            { "rum",        99999 },
            { "beer",       99999 },
            { "lemonade",   99999 },
            { "tea",        99999 },
        };

        CreateShop();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateShop()
    {
        foreach (KeyValuePair<string, int> pair in _buyableItems)
        {
            
        }
    }
   public void Buy(string name)
   {

   }
}
