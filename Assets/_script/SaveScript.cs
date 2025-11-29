using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Dictionary<string, int> valuesAndScene = new Dictionary<string, int>();
    public void AddValueAndScene(string key, int value)
    {
        valuesAndScene.Add(key, value);
        Debug.Log(valuesAndScene.Count);
    }
}
