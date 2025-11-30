using UnityEngine;
using UnityEngine.U2D;

public class IngredientInShaker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.CompareTag("Ingredient"))
        {
            if (col.gameObject.CompareTag("Shaker"))
            {
                if (col.GetComponent<Shaker>().GetDrinkState() == false)
                {
                    col.GetComponent<Shaker>()?.AddIngredient(gameObject.GetComponent<Ingredient>());
                    gameObject.SetActive(false);
                }
            }
            else if (col.gameObject.CompareTag("Mixeur"))
            {
                if (col.GetComponent<Mixer>().GetMixerState() == false)
                {
                    col.GetComponent<Mixer>()?.PutIngredient(gameObject.GetComponent<Ingredient>());
                    gameObject.SetActive(false);
                }
            }
        } else if (gameObject.CompareTag("Shaker"))
        {
            if(col.gameObject.CompareTag("Mixeur"))
            {
                GetComponent<Shaker>()?.AddIngredient(col.GetComponent<Mixer>().GetIngredient());
                col.GetComponent<Mixer>().EmptyMixer();
            }
        }
    }
}

