using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera worldCamera;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateIngredient(GameObject ingredient)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(transform.position);

        worldPos.z = 0f;

        Instantiate(ingredient, worldPos, Quaternion.identity);
    }
}
