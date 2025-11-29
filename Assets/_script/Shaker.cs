using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shaker : MonoBehaviour
{
    private Dictionary<IngredientType, int> DrinkStats = new Dictionary<IngredientType, int>();
    private bool isFinished = false;
    private int shakeCount = 0;
    private float posY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrinkStats.Add(IngredientType.TEMP, 50);
        DrinkStats.Add(IngredientType.SWEET, 50);
        DrinkStats.Add(IngredientType.ALCOHOL, 50);
        DrinkStats.Add(IngredientType.SPARKLING, 50);
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }
    public void AddIngredient(Ingredient toAdd)
    {
        List<IngredientStats> tmp = toAdd.GetIngredientStats();

        foreach (IngredientStats IngStats in tmp)
        {
            DrinkStats[IngStats.GetIngredientType()] += IngStats.GetValue();
        }
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
    }
    private void IncrementShakeCount()
    {
        shakeCount++;
        posY = transform.position.y;
    }
}
