using System;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterSpe
{
    public int itemValue;
    public Dictionary<string, int> idealStats;
};

public class Gambling : MonoBehaviour
{
    float _rate;
    float _range;
    int _difficulty;
    int _maxDifficulty;
    CharacterSpe _characterSpe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterSpe characterSpe;
        characterSpe.itemValue = 100;
        characterSpe.idealStats = new Dictionary<string, int>();
        characterSpe.idealStats["sweet"] = 50;
        characterSpe.idealStats["temp"] = 50;

        Dictionary <string,int> resultCocktail = new Dictionary<string, int>();
        resultCocktail["sweet"] = 64;
        resultCocktail["temp"] = 36;

        GenerateInformations(characterSpe);
        CalculatePayment(resultCocktail);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateInformations(CharacterSpe specialisations)
    {
        _maxDifficulty = 4;
        _difficulty = 3;// UnityEngine.Random.Range(2, maxDifficulty + 1);
        _rate = _difficulty - 0.5f;
        _range = (_maxDifficulty - _rate) * 5;
        
        _characterSpe = specialisations;
        Debug.Log(string.Format("Difficulty : {0} Rate : {1} Range : {2}", _difficulty, _rate, _range));
    }

    float GetSuccessRate(Dictionary<string, int> givenStats)
    {
        float statsSize = givenStats.Count;
        float deltaAvg = 0;
        float deltaSum = 0;

        foreach(KeyValuePair<string, int> pair in givenStats)
        {
            deltaSum += Mathf.Abs(pair.Value - _characterSpe.idealStats[pair.Key]);
        }

        deltaAvg = deltaSum / statsSize;
        Debug.Log(string.Format("deltaSum : {0}\n deltaAvg : {1}", deltaSum, deltaAvg));

        if (deltaAvg < _range)
        {
            return _rate;
        }

        Debug.Log("coucou");
        return _rate / (deltaAvg / 4);
    }
    public float CalculatePayment(Dictionary<string, int> givenStats)
    {
        float value = GetSuccessRate(givenStats);
        Debug.Log(string.Format("Payment : {0}, Value : {1}", _characterSpe.itemValue * value, value));
        return _characterSpe.itemValue * value;
    }
}
