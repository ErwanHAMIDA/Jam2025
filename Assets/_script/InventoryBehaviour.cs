using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip _clip;
    [SerializeField] GameObject _inventoryMenu;
    public void Toggle()
    {
        _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);
    }
}
