using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] GameObject charcterPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        newCharacter.GetComponent<CharacterBehaviour>().CharacterCreation(0b010001); // 0b010000 + 0b000001
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newCharacter = Instantiate(charcterPrefab);
            charcterPrefab.gameObject.SetActive(true);
            newCharacter.GetComponent<CharacterBehaviour>().CharacterCreation(0b010001); // 0b010000 + 0b000001
        }
    }
}
