using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Item
{
    public string _name;
    public string _description;
    public int _price;
    public Sprite _thumbnail;

    public Item(string itemName, string itemDesc, int itemPrice, Sprite itemThumb)
    {
        _name = itemName; _description = itemDesc; _price = itemPrice; _thumbnail = itemThumb;
    }
}
public class Shop : MonoBehaviour
{

    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] GameObject _prefabParent;
    [SerializeField] GameObject _shopMenu;
    [SerializeField] GameObject _shopBackground;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] AudioClip _clip;

    List<Item> _buyableItems = new List<Item>();
    void Start()
    {
        #region SetupShopItem
        _buyableItems.Add(new Item("Ice", "Get it fresh !", 10, _sprites[0]));
        _buyableItems.Add(new Item("Mint", "breath freshener", 10, _sprites[1]));
        _buyableItems.Add(new Item("Pepper", "Hot sauce", 10, _sprites[2]));
        _buyableItems.Add(new Item("Orange", "Sweet as sugar", 10, _sprites[3]));
        _buyableItems.Add(new Item("Lemon", "Acidic juice", 10, _sprites[4]));
        _buyableItems.Add(new Item("Gasoline", "Give'em hell", 10, _sprites[5]));
        #endregion

        CreateShop();
    }

    void CreateShop()
    {
        foreach (Transform child in _prefabParent.transform)
            Destroy(child.gameObject);

        foreach (var item in _buyableItems)
        {
            GameObject newButton = Instantiate(_buttonPrefab, _prefabParent.transform);

            newButton.SetActive(true);
            newButton.transform.Find("Name").GetComponent<TMP_Text>().text = item._name;
            newButton.transform.Find("Description").GetComponent<TMP_Text>().text = item._description;
            newButton.transform.Find("Price").GetComponent<TMP_Text>().text = item._price.ToString();
            newButton.transform.Find("Thumbnail").GetComponent<Image>().sprite = item._thumbnail;

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
        if (GameData.Instance.Gold - item._price < 0) return;

        GameData.Instance.AddGold(-item._price);
        Debug.Log("Buy :" + item._name);
   }
}
