using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/IngredientData")]
public class IngredientData : ScriptableObject
{
    [Header("Identification")]
    public string _ingredientId;   // Unique id if needed for save/lookup.
    public string _ingredientName; // Display name.
    public GameObject Prefab;

    [Tooltip("Sprite of the card front.")]
    public Sprite _ingredientTexture;

    [Header("Game info")]
    public string _description; // Optional description or lore.
}
