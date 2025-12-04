using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class BeerTapBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private AudioClip _pourAudio;

    private bool _isCollidingWithShaker = false;
    private bool _isOpen = false;
    private float _goUpCD = 0.0f;
    private float _goUpMax = 2.0f;


    public void Open()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(0.0f,0.3f,0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _openSprite;
        _isOpen = true;
    }

    public void Close()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _closeSprite;
        _isOpen = false; 
    }

    public void Toggle()
    {
        if (_isOpen)
        {
            Close();
        }
        else
        {
            //SFXManager.Instance.PlaySFXClip(pourAudio, transform, 1);
            Open();
        }
    }

    public void TryAddIngredient(Ingredient ingredient)
    {
        if (_isCollidingWithShaker == false)
            return;

        _shaker.GetComponent<Shaker>().AddFromTireuse(ingredient);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shaker")
        {
            _isCollidingWithShaker = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shaker")
        {
            _isCollidingWithShaker = false;
        }
    }

    void Update()
    {
        if (_isOpen == false)
        {
            _goUpCD += Time.deltaTime;
            if (_goUpCD >= _goUpMax)
            {
                Open();
                _goUpCD = 0.0f;
            }
        }
    }
}
