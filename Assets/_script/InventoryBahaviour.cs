using UnityEngine;

public class InventoryBahaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
