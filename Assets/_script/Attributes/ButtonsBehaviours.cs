using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviours : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private Image _shopImage;

    private bool _isUseShop = false;

    public void UseShopButton()
    {
        _isUseShop = !_isUseShop;

        if (_isUseShop)
            _shopImage.sprite = _openSprite;
        else
            _shopImage.sprite = _closeSprite;
    }
}
