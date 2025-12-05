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
    [SerializeField][Range(-50, 50)] private int _valueInTemp;
    [SerializeField][Range(-50, 50)] private int _valueInSweet;
    [SerializeField][Range(-50, 50)] private int _valueInAlchool;
    [SerializeField][Range(-50, 50)] private int _valueInSparkling;
    [SerializeField] private AudioClip _hitSound;

    private Dictionary<IngredientType,int> _stats = new Dictionary<IngredientType,int>();

    public void InitIngredient(Dictionary<IngredientType, int> givenStats)
    {
        _stats = givenStats;
    }

    void Start()
    {
        _stats = new Dictionary<IngredientType, int>
        {
            {   IngredientType.TEMP, _valueInTemp           },
            {   IngredientType.SWEET, _valueInSweet         },
            {   IngredientType.ALCOHOL, _valueInAlchool     },
            {   IngredientType.SPARKLING, _valueInSparkling },
        };
    }

    #region Helpers
    public Dictionary<IngredientType, int> GetIngredientStats()
    {
        return _stats;
    }
    #endregion
}
