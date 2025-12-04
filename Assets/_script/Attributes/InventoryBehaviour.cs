using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private GameObject _inventoryMenu;
    public void Toggle()
    {
        _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);
    }
}
