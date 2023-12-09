using System;

public sealed class InventoryItemCrafter
{
    private Inventory _inventory;

    public InventoryItemCrafter(Inventory inventory)
    {
        _inventory = inventory;
    }

    private bool CanCraft(InventoryItemRecipe recipe)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            for (int i = 0; i < ingredient.Amount; i++)
            {
                int currenAmount = _inventory.GetItemsCount(ingredient.Item.Prototype.Name);
                if (currenAmount < ingredient.Amount)
                {
                    return false;
                }
            }
        }

        return true;
    }

    internal void Craft(InventoryItemRecipe recipe)
    {
        if (!CanCraft(recipe))
        {
            throw new Exception("Not enougth resources!");
        }

        foreach (var ingredient in recipe.Ingredients)
        {
            _inventory.RemoveItems(ingredient.Item.Prototype.Name, ingredient.Amount);
        }
        
        var resultItem = recipe.ResultItem.Prototype.Clone();
        _inventory.TryAddItem(resultItem);
    }
}
