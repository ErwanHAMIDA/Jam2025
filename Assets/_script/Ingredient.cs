using System.Collections.Generic;
using UnityEngine;


public enum IngredientType
{
     TEMP,
     SWEET,
     ALCOHOL,
     SPARKLING,

     COUNT
}

public struct IngredientStats
{
    IngredientType type;
    int value;

    public int GetValue()
    {
        return value;
    }

    public IngredientType GetIngredientType()
    {
        return type;
    }

    public void SetIngredientType (IngredientType type)
    {
        this.type = type;
    }

    public void SetValue (int value)
    {
        this.value = value;
    }
}


public class Ingredient : MonoBehaviour
{
    List<IngredientStats> stats;

    public void InitIngredient(List<IngredientStats> givenStats)
    {
        stats = givenStats;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<IngredientStats> test = new List<IngredientStats>();
        test.Add(new IngredientStats());
        test[0].SetIngredientType(IngredientType.TEMP);
        test[0].SetValue(2);

        test.Add(new IngredientStats());
        test[1].SetIngredientType(IngredientType.SWEET);
        test[1].SetValue(-4);

        test.Add(new IngredientStats());
        test[2].SetIngredientType(IngredientType.ALCOHOL);
        test[2].SetValue(1);

        test.Add(new IngredientStats());
        test[3].SetIngredientType(IngredientType.SPARKLING);
        test[3].SetValue(0);

        InitIngredient(test);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<IngredientStats> GetIngredientStats()
    {
        return stats;
    }
    
}
