using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;
    [SerializeField] private GameObject _spawnPoint;
    
    public void InstantiateIngredient(GameObject ingredient)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(_spawnPoint.transform.position);

        worldPos.z = 0f;

        GameObject newIngredient = Instantiate(ingredient, worldPos, Quaternion.identity);
        newIngredient.tag = "Ingredient";
        newIngredient.AddComponent<IngredientInShaker>();
    }
}
