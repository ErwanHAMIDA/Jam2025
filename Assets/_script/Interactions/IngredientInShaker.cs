using UnityEngine;
using UnityEngine.U2D;

public class IngredientInShaker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag("Ingredient"))
        {
            if (col.gameObject.CompareTag("Shaker"))
            {
                if (col.GetComponent<Shaker>().GetDrinkState() == false)
                {
                    col.GetComponent<Shaker>()?.AddIngredient(GetComponent<Ingredient>());
                    gameObject.SetActive(false);
                }
            }
            else if (col.CompareTag("Mixeur"))
            {
                if (col.GetComponent<Mixer>().GetMixerState() == false)
                {
                    col.GetComponent<Mixer>()?.PutIngredient(GetComponent<Ingredient>());
                    gameObject.SetActive(false);
                }
            }
        } 
        else if (CompareTag("Shaker"))
        {
            if(col.CompareTag("Mixeur"))
            {
                GetComponent<Shaker>()?.AddIngredient(col.GetComponent<Mixer>().GetIngredient());
                col.GetComponent<Mixer>().EmptyMixer();
            }
        }
    }
}

