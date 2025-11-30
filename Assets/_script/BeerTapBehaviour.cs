using UnityEngine;

public class BeerTapBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;

    public void Open()
    {
        transform.GetChild(0).transform.position.Set(0.0f,0.3f,0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = OpenSprite;
    }

    public void Close()
    {
        transform.GetChild(0).transform.position.Set(0.0f, 0.0f, 0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CloseSprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
