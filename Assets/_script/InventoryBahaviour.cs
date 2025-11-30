using UnityEngine;

public class InventoryBahaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioClip _clip;
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        SFXManager.Instance.PlaySFXClip(_clip, transform, 1);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
