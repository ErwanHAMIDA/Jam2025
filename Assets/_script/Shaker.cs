using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shaker : MonoBehaviour
{
    [SerializeField] GameObject StatsDisplayer;

    private Dictionary<IngredientType, int> DrinkStats = new Dictionary<IngredientType, int>(4)
    {
        {IngredientType.TEMP, 50},
        {IngredientType.SWEET, 50},
        {IngredientType.ALCOHOL, 50},
        { IngredientType.SPARKLING, 50 }
    };
    private bool isFinished = false;
    private bool canBeServed = false;
    private int shakeCount = 0;
    private float posY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posY = transform.position.y;
        Debug.Log(DrinkStats);
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }
    public void AddIngredient(Ingredient toAdd)
    {
        Dictionary<IngredientType,int> tmp = toAdd.GetIngredientStats();

        foreach (KeyValuePair<IngredientType,int> it in tmp)
        {
            DrinkStats[it.Key] += it.Value;
        }
        StatsDisplayer.GetComponent<StatsDisplay>().AddStats(tmp);
    }

    public void ClearDrink()
    {
        DrinkStats[IngredientType.TEMP] = 50;
        DrinkStats[IngredientType.SWEET] = 50;
        DrinkStats[IngredientType.ALCOHOL] = 50;
        DrinkStats[IngredientType.SPARKLING] = 50;
    }

    public void FinishDrink()
    {
        GetComponent<Draggable>().gameObject.SetActive(true);
        isFinished = true;
    }

    public bool GetDrinkState()
    {
        return isFinished;
    }

    public bool CanBeServed()
    {
        return canBeServed;
    }

    public void Shake()
    {
        if (shakeCount % 2 == 0)
        {
            if (transform.position.y - posY >= 1f)
            {
                IncrementShakeCount();
            }
        }
        else
        {
            if (posY - transform.position.y >= 1f)
            {
                IncrementShakeCount();
            }
        }
        if (shakeCount >= 6)
        {
            canBeServed = true;
        }
    }
    private void IncrementShakeCount()
    {
        shakeCount++;
        posY = transform.position.y;
    }

    public Dictionary<IngredientType, int> GetCocktailStats()
    {
        // Check if shaker Ready
        return DrinkStats;
    }
}
