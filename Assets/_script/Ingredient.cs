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


public class Ingredient : MonoBehaviour
{
    Dictionary<IngredientType, int> stats;
    
    [SerializeField] [Range(-50,50)] int valueInTemp;
    [SerializeField] [Range(-50,50)] int valueInSweet;
    [SerializeField] [Range(-50,50)] int valueInAlchool;
    [SerializeField] [Range(-50,50)] int valueInSparkling;

    public void InitIngredient(Dictionary<IngredientType, int> givenStats)
    {
        stats = givenStats;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = new Dictionary<IngredientType, int>
        {
            {IngredientType.TEMP, valueInTemp },
            {IngredientType.SWEET, valueInSweet },
            {IngredientType.ALCOHOL, valueInAlchool },
            {IngredientType.SPARKLING, valueInSparkling },
        };
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
