using TMPro;
using UnityEngine;

public class StockBarName : MonoBehaviour
{
    private TMP_InputField _inputField;
    static public string _barName;

    public void ReadInput()
    {
        _barName = _inputField.text;
        Debug.Log("Input: " + _barName);
    }
}
