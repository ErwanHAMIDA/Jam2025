using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject charcterPrefab;
    [SerializeField] GameObject Shaker;
    [SerializeField] GameObject GoldTextCount;
    [SerializeField] GameObject PhysicEnvironmentHolder;
    [SerializeField] Camera WorldCam;

    float pixelWidth;
    float pixelHeight;

    float sceneWidth;
    float sceneHeight;

    float originWidth = 15.36705f;
    float originHeight = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        newCharacter.GetComponent<CharacterBehaviour>().CharacterCreation(0b010001); // 0b010000 + 0b000001

    }

    void SpawnNewClient()
    {
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        newCharacter.GetComponent<CharacterBehaviour>().CharacterCreation(0b010001); // 0b010000 + 0b000001
        GetComponent<Gambling>().GenerateInformations(newCharacter.GetComponent<CharacterBehaviour>().GetCharactersSpecifications());
    }

    public void ValidCocktail() 
    {
        if (Shaker.GetComponent<Shaker>().CanBeServed() == false)
            return;

        int price = 0;
        Dictionary<IngredientType,int> ShakerStats = Shaker.GetComponent<Shaker>().GetCocktailStats();
        price = (int)GetComponent<Gambling>().CalculatePayment(ShakerStats);
        GameData.Gold += price;
    }

    // Update is called once per frame
    void Update()
    {
        // TEMP
        //if (Input.GetKeyDown(KeyCode.E))
        //    SpawnNewClient();

        //GoldTextCount.GetComponent<TextMeshPro>().SetText(GameData.Gold.ToString());
    }

    
}
