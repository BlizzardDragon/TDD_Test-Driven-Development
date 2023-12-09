using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemRecipes", menuName = "ScriptableObjects/CraftRecipes", order = 0)]
public class InventoryItemRecipe : ScriptableObject
{
    public InventoryItemConfig ResultItem;
    public InventoryItemIngredient[] Ingredients;
}