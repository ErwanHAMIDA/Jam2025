using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    [Header("======| Camera |======")]
    [Header("")]
    [SerializeField] private Camera _worldCam;

    [Header("======| Audio Clips |======")]
    [Header("")]
    [SerializeField] private AudioClip _bellRing;
    [SerializeField] private AudioClip _walkAudio;
    [SerializeField] private AudioClip _payAudio;

    [Header("======| Others |======")]
    [Header("")]
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _physicEnvironmentHolder;
    [SerializeField] private Shaker _shaker;

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
            GameData.Instance.SetDataByDefault();
        }

        SpawnNewClient();
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