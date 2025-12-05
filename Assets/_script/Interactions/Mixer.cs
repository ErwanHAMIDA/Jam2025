using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    private Ingredient _mixedIngredient;
    private bool _isFull = false;

    public void PutIngredient(Ingredient ingredient)
    {
        _mixedIngredient = ingredient;
        _isFull = true;
    }

    public void MixIngredient()
    {
        if (_isFull == true)
        {
            GetComponent<Draggable>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    public void EmptyMixer()
    {
        _isFull = false;
        _mixedIngredient = null;
        GetComponent<Draggable>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    #region Helpers
    public bool GetMixerState()
    {
        return _isFull;
    }

    public Ingredient GetIngredient()
    {
        return _mixedIngredient;
    }
    #endregion
}
