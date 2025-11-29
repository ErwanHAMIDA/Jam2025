using UnityEngine;
using UnityEngine.U2D;

public class IngredientInShaker : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        col.gameObject.SetActive(false);
    }
}

