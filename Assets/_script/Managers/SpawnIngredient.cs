using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Vector3 _spawnCoord;

    public void InstantiateIngredient(GameObject ingredientPrefab)
    {
        Vector3 spawnPosition = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0f;

        GameObject newIngredient = Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity);
        newIngredient.tag = "Ingredient";
        newIngredient.AddComponent<IngredientInShaker>();
    }

}
