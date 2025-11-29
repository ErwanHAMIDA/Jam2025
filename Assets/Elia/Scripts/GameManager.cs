using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject charcterPrefab;
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
        // Check if shaker is Valid
        int price = 0;
        // ShakerStats = Get ShakerStats
        // price = GetComponent<Gambling>().CalculatePayment(ShakerStats);
        GetComponent<GameData>().Gold += price;
    }

    // Update is called once per frame
    void Update()
    {
        // TEMP
        if (Input.GetKeyDown(KeyCode.E))
            SpawnNewClient();
    }
}
