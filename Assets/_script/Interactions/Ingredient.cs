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
    [SerializeField][Range(-50, 50)] int valueInTemp;
    [SerializeField][Range(-50, 50)] int valueInSweet;
    [SerializeField][Range(-50, 50)] int valueInAlchool;
    [SerializeField][Range(-50, 50)] int valueInSparkling;
    [SerializeField] AudioClip hitSound;

    public AudioClip HitSound { set; get; }  
    Dictionary<IngredientType,int> stats = new Dictionary<IngredientType,int>();

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SFXManager.Instance.PlaySFXClip(hitSound, transform, 1);
    }

    public Dictionary<IngredientType, int> GetIngredientStats()
    {
        return stats;
    }
    
}
