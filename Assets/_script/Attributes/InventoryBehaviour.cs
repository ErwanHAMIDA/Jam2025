using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryMenu;
    public void Toggle()
    {
        _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);
    }
}
