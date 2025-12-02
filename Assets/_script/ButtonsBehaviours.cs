using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{
    [SerializeField] Sprite OpenSprite;
    [SerializeField] Sprite CloseSprite;

    public void Open()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = OpenSprite;
    }

    public void Close()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CloseSprite;
    }
}
