using TMPro;
using UnityEngine;

public class SetBarName : MonoBehaviour
{
    [SerializeField] private TMP_Text _barNameInGame;
    void Start()
    {
        _barNameInGame.text = StockBarName._barName;
    }
}
