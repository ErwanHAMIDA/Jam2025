using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Item
{
    public string name;
    public string description;
    public int price;
    public Sprite thumbnail;

    public Item(string itemName, string itemDesc, int itemPrice, Sprite itemThumb)
    {
        name = itemName; description = itemDesc; price = itemPrice; thumbnail = itemThumb;
    }
}
public class Shop : MonoBehaviour
{
    List<Item> _buyableItems = new List<Item>();
    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] GameObject _prefabParent;
    [SerializeField] List<Sprite> sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _buyableItems.Add(new Item("ice", "Get it fresh !", 10, sprites[0]));
        _buyableItems.Add(new Item("mint", "breath freshener", 10, sprites[1]));

        CreateShop();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateShop()
    {
        foreach (var item in _buyableItems)
        {
            GameObject newButton = Instantiate(_buttonPrefab, _prefabParent.transform);
            newButton.SetActive(true);
            newButton.transform.Find("Name").GetComponent<TMP_Text>().text = item.name;
            newButton.transform.Find("Description").GetComponent<TMP_Text>().text = item.description;
            newButton.transform.Find("Price").GetComponent<TMP_Text>().text = item.price.ToString();
            newButton.transform.Find("Thumbnail").GetComponent<Image>().sprite = item.thumbnail;

            //newButton.GetComponent<Button>().onClick.AddListener(() => Buy(item.price));
        }
    }
   public void Buy(int price)
   {
        //GameData.Gold -= price;
   }
}
