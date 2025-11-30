using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{
    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;

    public void Open()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = OpenSprite;
    }

    public void Close()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CloseSprite;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
