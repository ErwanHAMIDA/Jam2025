using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BeerTapBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;
    [SerializeField] Shaker Shaker;

    bool isCollidingWithShaker = false;
    bool isOpen;
    float goUpCD = 0.0f;
    float goUpMax = 2.0f;


    public void Open()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(0.0f,0.3f,0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = OpenSprite;
        isOpen = true;
    }

    public void Close()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CloseSprite;
        isOpen = false; 
    }

    public void Toggle()
    {
        if (isOpen)
        {
            Close();
        }
        else
            Open();
    }

    public void TryAddIngredient(Ingredient ingredient)
    {
        if (isCollidingWithShaker == false)
            return;

        Shaker.GetComponent<Shaker>().AddFromTireuse(ingredient);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shaker")
        {
            isCollidingWithShaker = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shaker")
        {
            isCollidingWithShaker = false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isOpen == false)
        {
            goUpCD += Time.deltaTime;
            if (goUpCD >= goUpMax)
            {
                Open();
                goUpCD = 0.0f;
            }
        }
    }
}
