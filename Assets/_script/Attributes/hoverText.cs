using UnityEngine;
using UnityEngine.EventSystems;

public class hoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject _text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.SetActive(false);
    }
}
