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
    [SerializeField] GameObject _shopMenu;
    [SerializeField] GameObject _shopBackground;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] AudioClip clip;

    void Start()
    {
        _buyableItems.Add(new Item("Ice", "Get it fresh !", 10, sprites[0]));
        _buyableItems.Add(new Item("Mint", "breath freshener", 10, sprites[1]));
        _buyableItems.Add(new Item("Pepper", "Hot sauce", 10, sprites[2]));
        _buyableItems.Add(new Item("Orange", "Sweet as sugar", 10, sprites[3]));
        _buyableItems.Add(new Item("Lemon", "Acidic juice", 10, sprites[4]));
        _buyableItems.Add(new Item("Gasoline", "Give'em hell", 10, sprites[5]));

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

            newButton.GetComponent<Button>().onClick.AddListener(delegate { Buy(item); });
        }
    }

    public void Toggle()
    {
        _shopMenu.SetActive(!_shopMenu.activeSelf);
        _shopBackground.SetActive(!_shopBackground.activeSelf);
    }
   public void Buy(Item item)
   {
        GameData.Gold -= item.price;
        Debug.Log("Buy :" + item.name);
   }
}
