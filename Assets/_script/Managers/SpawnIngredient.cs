using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    [SerializeField] private Camera worldCamera;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Vector3 _spawnCoord;
    
    public void InstantiateIngredient(GameObject ingredient)
    {
        //Vector3 worldPos = worldCamera.ScreenToWorldPoint(_spawnPoint.transform.position + _offset);

        _spawnCoord.z = 0f;

        GameObject newIngredient = Instantiate(ingredient, _spawnCoord, Quaternion.identity);
        newIngredient.tag = "Ingredient";
        newIngredient.AddComponent<IngredientInShaker>();
    }
}
