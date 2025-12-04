using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    Ingredient MixedIngredient;
    bool isFull = false;

    public void PutIngredient(Ingredient ingredient)
    {
        MixedIngredient = ingredient;
        isFull = true;
    }

    public void MixIngredient()
    {
        if (isFull == true)
        {
            GetComponent<Draggable>().enabled = true;
            //GetComponentInChildren<Image>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetMixerState()
    {
        return isFull;
    }

    public Ingredient GetIngredient()
    {
        return MixedIngredient;
    }

    public void EmptyMixer()
    {
        isFull = false;
        MixedIngredient = null;
        GetComponent<Draggable>().enabled = false;
        GetComponentInChildren<Image>().color = Color.white;
    }
}
