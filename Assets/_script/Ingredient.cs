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

//public struct IngredientStats
//{
//    IngredientType type;
//    int value;

//    public int GetValue()
//    {
//        return value;
//    }

//    public IngredientType GetIngredientType()
//    {
//        return type;
//    }

//    public void SetIngredientType (IngredientType type)
//    {
//        this.type = type;
//    }

//    public void SetValue (int value)
//    {
//        this.value = value;
//    }
//}


public class Ingredient : MonoBehaviour
{
    Dictionary<IngredientType,int> stats = new Dictionary<IngredientType,int>();

    public void InitIngredient(Dictionary<IngredientType, int> givenStats)
    {
        stats = givenStats;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Dictionary<IngredientType, int> test = new Dictionary<IngredientType, int>
        {
            { IngredientType.TEMP, 5 },
            { IngredientType.SWEET, 10 },
            { IngredientType.ALCOHOL, -4 },
            { IngredientType.SPARKLING, 0 }
        };
        InitIngredient(test);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Dictionary<IngredientType, int> GetIngredientStats()
    {
        return stats;
    }
    
}
