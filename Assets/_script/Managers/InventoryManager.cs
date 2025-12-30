using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //[SerializeField] private GameObject _panel;
    [SerializeField] private ScriptableObject[] _ingredients;

    [Header("======| Game Settings |======")]
    [Header("")]
    [SerializeField] private int _gold;
    [SerializeField] private IngredientData[] _startedIngredients;
    [SerializeField] private int[] _startedIngredientQuantity;
    [SerializeField] private int _inventoryCaseNumber = 20;

    [Header("======| References |======")]
    [Header("")]
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _fillSlot;
    [SerializeField] private GameObject _inventoryGrid;

    private List<GameObject> _inventorySlots = new List<GameObject>();

    private void Start()
    {
        GameData loaded = SaveManager.Instance.LoadData();

        if (loaded != null)
        {
            GameData.Instance.CopyFrom(loaded);
        }
        else
        {
            GameData.Instance.SetDataByDefault(_gold, _startedIngredients, _startedIngredientQuantity);
        }

        SetUpInventory();
    }
    public void SetUpInventory()
    {
        var ingredients = new List<IngredientData>(GameData.Instance.Inventory.Keys);
        int itemNumber = ingredients.Count;

        for (int i = 0; i < _inventoryCaseNumber; i++)
        {
            GameObject slot;
            if (i >= itemNumber)
            {
                slot = Instantiate(_emptySlot, _inventoryGrid.transform);
            }
            else
            {
                slot = Instantiate(_fillSlot, _inventoryGrid.transform);
                var ingredient = ingredients[i];
                Image slotImage = slot.GetComponent<Image>();
                if (slotImage != null)
                    slotImage.sprite = ingredient._ingredientTexture;

                // Récupère le composant Button
                Button button = slot.GetComponent<Button>();
                if (button != null)
                {
                    // Nettoie les listeners précédents
                    button.onClick.RemoveAllListeners();
                    // Ajoute un nouveau listener avec l'ingrédient
                    button.onClick.AddListener(() => OnSlotClicked(ingredient));
                }
            }
            _inventorySlots.Add(slot);
        }
    }

    private void OnSlotClicked(IngredientData ingredient)
    {
        // Trouve le SpawnIngredient dans la scène
        SpawnIngredient spawnIngredient = FindObjectOfType<SpawnIngredient>();
        if (spawnIngredient != null && ingredient != null && ingredient.Prefab != null)
        {
            spawnIngredient.InstantiateIngredient(ingredient.Prefab);
        }
    }
}
