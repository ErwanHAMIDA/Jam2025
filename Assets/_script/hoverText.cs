using UnityEngine;
using UnityEngine.EventSystems;

public class hoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
    }
}
