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
        SceneInstantiation();

        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        newCharacter.GetComponent<CharacterBehaviour>().CharacterCreation(0b010001); // 0b010000 + 0b000001

    }

    void SceneInstantiation()
    {
        // Récupère les coins de l'écran en pixels
        Vector3 bottomLeft = WorldCam.ScreenToWorldPoint(new Vector3(0, 0, WorldCam.nearClipPlane));
        Vector3 topRight = WorldCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, WorldCam.nearClipPlane));

        // Calcule la taille de l'écran en unités monde
        sceneWidth = topRight.x - bottomLeft.x;
        sceneHeight = topRight.y - bottomLeft.y;

        pixelWidth = Screen.width;
        pixelHeight = Screen.height;
        // Set Up Physics Elements
        // Bar
        Debug.Log(sceneWidth);
        Debug.Log(sceneHeight);

        PhysicEnvironmentHolder.transform.GetChild(0).transform.localScale = new Vector3(sceneWidth * 16 / originWidth, 3 * sceneHeight / originHeight, 1);
        PhysicEnvironmentHolder.transform.GetChild(0).transform.localPosition = new Vector3(0, -2 * sceneHeight / originHeight, 1);

        // Tireuses
        PhysicEnvironmentHolder.transform.GetChild(1).transform.localPosition = new Vector3(sceneWidth * -7.0f / originWidth, 1 * sceneHeight / originHeight, 1);
        PhysicEnvironmentHolder.transform.GetChild(1).transform.localScale = new Vector3(sceneWidth * 1 / originWidth, 3 * sceneHeight / originHeight, 1);

        PhysicEnvironmentHolder.transform.GetChild(2).transform.localPosition = new Vector3(sceneWidth * -5.75f / originWidth, 1 * sceneHeight / originHeight, 1);
        PhysicEnvironmentHolder.transform.GetChild(2).transform.localScale = new Vector3(sceneWidth * 1 / originWidth, 3 * sceneHeight / originHeight, 1);

        PhysicEnvironmentHolder.transform.GetChild(3).transform.localPosition = new Vector3(sceneWidth * -4.5f / originWidth, 1 * sceneHeight / originHeight, 1);
        PhysicEnvironmentHolder.transform.GetChild(3).transform.localScale = new Vector3(sceneWidth * 1 / originWidth, 3 * sceneHeight / originHeight, 1);
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

    void CheckWindowSize()
    {
        if (pixelHeight != Screen.height || pixelWidth != Screen.width) 
        {
            SceneInstantiation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckWindowSize();

        // TEMP
        //if (Input.GetKeyDown(KeyCode.E))
        //    SpawnNewClient();

        //GoldTextCount.GetComponent<TextMeshPro>().SetText(GameData.Gold.ToString());
    }
}
