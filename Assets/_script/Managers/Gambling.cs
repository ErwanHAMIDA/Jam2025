using System.Collections.Generic;
using UnityEngine;

public struct CharacterSpe
{
    public int itemValue;
    public Dictionary<IngredientType, int> idealStats;
};

public class Gambling : MonoBehaviour
{
    private float _rate;
    private float _range;
    private int _difficulty;
    private int _maxDifficulty;
    private CharacterSpe _characterSpe;

    public void GenerateInformations(CharacterSpe specialisations)
    {
        _maxDifficulty = 4;
        _difficulty = 3;
        _rate = _difficulty - 0.5f;
        _range = (_maxDifficulty - _rate) * 5;
        
        _characterSpe = specialisations;
        Debug.Log(string.Format("Difficulty : {0} Rate : {1} Range : {2}", _difficulty, _rate, _range));
    }

    float GetSuccessRate(Dictionary<IngredientType, int> givenStats)
    {
        float statsSize = givenStats.Count;
        float deltaAvg = 0;
        float deltaSum = 0;

        foreach(KeyValuePair<IngredientType, int> pair in givenStats)
        {
            deltaSum += Mathf.Abs(pair.Value - _characterSpe.idealStats[pair.Key]);
        }

        deltaAvg = deltaSum / statsSize;
        Debug.Log(string.Format("deltaSum : {0}\n deltaAvg : {1}", deltaSum, deltaAvg));

        return deltaAvg < _range ? _rate : _rate / (deltaAvg / 4);
    }
    public float CalculatePayment(Dictionary<IngredientType, int> givenStats)
    {
        float value = GetSuccessRate(givenStats);
        Debug.Log(string.Format("Payment : {0}, Value : {1}", _characterSpe.itemValue * value, value));
        return _characterSpe.itemValue * value;
    }
}
