using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [Header("======| Game Settings |======")]
    [Header("")]
    [SerializeField] private int _gold;
    [SerializeField] private Ingredient[] _startedIngredients;
    [SerializeField] private int[] _startedIngredientQuantity;
    [SerializeField] private int _inventoryCaseNumber = 20;

    [Header("======| Camera |======")]
    [Header("")]
    [SerializeField] private Camera _worldCam;

    [Header("======| Audio Clips |======")]
    [Header("")]
    [SerializeField] private AudioClip _bellRing;
    [SerializeField] private AudioClip _walkAudio;
    [SerializeField] private AudioClip _payAudio;

    [Header("======| References |======")]
    [Header("")]
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _physicEnvironmentHolder;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _fillSlot;
    [SerializeField] private GameObject _inventoryGrid;

    [SerializeField] private UITextManager uiTextManager;

    private GameObject _currentCharacter;
    private CharacterBehaviour _characterBehaviour;

    private GameData _gameData;

    private void Awake()
    {
        GameData.Instance.SetUIManager(uiTextManager);
    }

    void Start()
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

        SetupInventory();
        SpawnNewClient();
    }

    private void SetupInventory()
    {
        int itemNumber = GameData.Instance.Inventory.Count;

        for (int i = 0; i < _inventoryCaseNumber; i++)
        {
            if (i >= itemNumber)
                Instantiate(_emptySlot, _inventoryGrid.transform);
            else
                Instantiate(_fillSlot,  _inventoryGrid.transform);
        }
    }

    public void SaveGame()
    {
        SaveManager.Instance.Save();
    }

    public void SpawnNewClient()
    {
        GameObject newCharacter = Instantiate(_characterPrefab);
        _characterBehaviour = newCharacter.GetComponent<CharacterBehaviour>();

        _currentCharacter = newCharacter;
        _characterPrefab.gameObject.SetActive(true);

        _characterBehaviour.CharacterCreation(0b010001); // 0b010000 + 0b000001
        _characterBehaviour.WalkAudio = _walkAudio;
        _characterBehaviour.PayAudio = _payAudio;

        GetComponent<Gambling>().GenerateInformations(_characterBehaviour.GetCharactersSpecifications());
    }

    public void ValidCocktail() 
    {
        if (_characterBehaviour.IsWaitingForDrink() == false)
            return;

        if (_shaker.GetComponent<Shaker>().CanBeServed() == false)
            return;

        StartCoroutine(ManageCocktail());
    }

    IEnumerator ManageCocktail()
    {
        int price = 0;
        Dictionary<IngredientType, int> ShakerStats = _shaker.GetCocktailStats();
        price = (int)GetComponent<Gambling>().CalculatePayment(ShakerStats);
        _characterBehaviour.ReceiveShaker(price);

        GameData.Instance.AddGold(price);

        yield return new WaitForSeconds(3.0f);
        SpawnNewClient();
    }

    
}