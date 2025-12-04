using TMPro;
using UnityEngine;

public class StockBarName : MonoBehaviour
{
    private TMP_InputField inputField;
    static public string _barName;

    public void ReadInput()
    {
        inputField = GameObject.FindAnyObjectByType<TMP_InputField>();
        _barName = inputField.text;
        Debug.Log("Input: " + _barName);
    }
}
