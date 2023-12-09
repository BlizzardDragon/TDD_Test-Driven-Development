using UnityEngine;

public static class Substitute
{
    public static InventoryItemConfig CreateItem(string itemName)
    {
        var newItem = ScriptableObject.CreateInstance<InventoryItemConfig>();
        newItem.Prototype = new InventoryItem(itemName, InventoryItemFlags.NONE, null, new object[0]);
        return newItem;
    }

    public static InventoryItemRecipe CreateRecipe(
        InventoryItemConfig result,
        params InventoryItemIngredient[] ingredients)
    {
        var recipe = ScriptableObject.CreateInstance<InventoryItemRecipe>();
        recipe.Ingredients = ingredients;
        recipe.ResultItem = result;
        return recipe;
    }
}