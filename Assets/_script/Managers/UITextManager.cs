using TMPro;
using UnityEngine;

public class UITextManager : MonoBehaviour
{
    [Header("======| Text Data |======")]
    [Header("")]
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _barNameText;

    public void UpdateUIText()
    {
        _goldText.text = GameData.Instance.Gold.ToString();
        _barNameText.text = GameData.Instance.Name;
    }
}
