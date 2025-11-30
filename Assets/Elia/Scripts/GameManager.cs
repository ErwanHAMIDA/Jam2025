using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject charcterPrefab;
    [SerializeField] GameObject Shaker;
    [SerializeField] GameObject GoldTextCount;
    [SerializeField] GameObject PhysicEnvironmentHolder;
    [SerializeField] Camera WorldCam;
    [SerializeField] AudioClip bellRing;
    [SerializeField] AudioClip walkAudio;
    [SerializeField] AudioClip payAudio;
    GameObject currentCharacter;

    public AudioClip PayAudio { get; }
    public AudioClip WalkAudio { get; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData.Gold = 100;
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        CharacterBehaviour characterBehaviour = newCharacter.GetComponent<CharacterBehaviour>();
        characterBehaviour.CharacterCreation(0b010001); // 0b010000 + 0b000001
        characterBehaviour.WalkAudio = walkAudio;
        characterBehaviour.PayAudio = payAudio;
        currentCharacter = newCharacter;
        GetComponent<Gambling>().GenerateInformations(newCharacter.GetComponent<CharacterBehaviour>().GetCharactersSpecifications());
    }

    public void SpawnNewClient()
    {
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        CharacterBehaviour characterBehaviour = newCharacter.GetComponent<CharacterBehaviour>();
        characterBehaviour.CharacterCreation(0b010001); // 0b010000 + 0b000001
        characterBehaviour.WalkAudio = walkAudio;
        characterBehaviour.PayAudio = payAudio;
        currentCharacter = newCharacter;
        GetComponent<Gambling>().GenerateInformations(newCharacter.GetComponent<CharacterBehaviour>().GetCharactersSpecifications());
    }

    public void ValidCocktail() 
    {
        if (Shaker.GetComponent<Shaker>().CanBeServed() == false)
            return;

        SFXManager.Instance.PlaySFXClip(bellRing, transform, 1);
        StartCoroutine(ManageCocktail());

    }

    IEnumerator ManageCocktail()
    {
        int price = 0;
        Dictionary<IngredientType, int> ShakerStats = Shaker.GetComponent<Shaker>().GetCocktailStats();
        price = (int)GetComponent<Gambling>().CalculatePayment(ShakerStats);
        currentCharacter.GetComponent<CharacterBehaviour>().ReceiveShaker(price);
        GameData.Gold += price;

        yield return new WaitForSeconds(1.25f);
        SpawnNewClient();
    }

    // Update is called once per frame
    void Update()
    {
        // TEMP
        //if (Input.GetKeyDown(KeyCode.E))
        //    SpawnNewClient();

        GoldTextCount.GetComponent<TMP_Text>().text = GameData.Gold.ToString();
    }

    
}
