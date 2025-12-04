using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviours : MonoBehaviour
{
    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;
    [SerializeField] Image _shopImage;

    private bool _isUseShop = false;

    public void UseShopButton()
    {
        _isUseShop = !_isUseShop;

        if (_isUseShop)
            _shopImage.sprite = OpenSprite;
        else
            _shopImage.sprite = CloseSprite;
    }
}
