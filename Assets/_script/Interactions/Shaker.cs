using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] GameObject _statsDisplayer;
    [SerializeField] Sprite _openedSprite;
    [SerializeField] Sprite _closedSprite;

    private Dictionary<IngredientType, int> _drinkStats = new Dictionary<IngredientType, int>(4)
    {
        {   IngredientType.TEMP, 50         },
        {   IngredientType.SWEET, 50        },
        {   IngredientType.ALCOHOL, 50      },
        {   IngredientType.SPARKLING, 50    }
    };
    private bool _isFinished = false;
    private bool _canBeServed = false;
    private int _shakeCount = 0;
    private float _posY;

    void Start()
    {
        _posY = transform.position.y;
        Debug.Log(_drinkStats);
    }

    void Update()
    {
        Shake();
        if (_isFinished)
            GetComponent<SpriteRenderer>().sprite = _closedSprite;
        else
            GetComponent<SpriteRenderer>().sprite = _openedSprite;
    }
    public void AddIngredient(Ingredient toAdd)
    {
        Dictionary<IngredientType,int> tmp = toAdd.GetIngredientStats();

        foreach (KeyValuePair<IngredientType,int> it in tmp)
        {
            _drinkStats[it.Key] += it.Value;
        }
        _statsDisplayer.GetComponent<StatsDisplay>().AddStats(tmp);
    }

    public void AddFromTireuse(Ingredient toAdd)
    {
        AddIngredient(toAdd);
    }

    public void ClearDrink()
    {
        _drinkStats[IngredientType.TEMP] = 50;
        _drinkStats[IngredientType.SWEET] = 50;
        _drinkStats[IngredientType.ALCOHOL] = 50;
        _drinkStats[IngredientType.SPARKLING] = 50;
    }

    public void FinishDrink()
    {
        _isFinished = true;
    }

    #region Helpers
    public bool GetDrinkState()
    {
        return _isFinished;
    }

    public bool CanBeServed()
    {
        return _canBeServed;
    }
    public Dictionary<IngredientType, int> GetCocktailStats()
    {
        return _drinkStats;
    }
    #endregion

    public void Shake()
    {
        if (_shakeCount % 2 == 0)
        {
            if (transform.position.y - _posY >= 1f)
            {
                IncrementShakeCount();
            }
        }
        else
        {
            if (_posY - transform.position.y >= 1f)
            {
                IncrementShakeCount();
            }
        }
        if (_shakeCount >= 6)
        {
            _canBeServed = true;
        }
    }
    private void IncrementShakeCount()
    {
        _shakeCount++;
        _posY = transform.position.y;
    }

    public void ResetShaker()
    {
        ClearDrink();
        ClearShaker();

        _posY = transform.position.y;
    }

    private void ClearShaker()
    {
        _shakeCount = 0;
        _canBeServed = false;
        _isFinished = false;
    }
}
