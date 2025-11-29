using UnityEngine;
using UnityEngine.U2D;

public class IngredientInShaker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(GetComponent<Shaker>().GetDrinkState() == false)
        {
            GetComponent<Shaker>()?.AddIngredient(col.gameObject.GetComponent<Ingredient>());
            col.gameObject.SetActive(false);
        }
    }
}

