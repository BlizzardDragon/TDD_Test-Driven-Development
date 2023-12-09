using System.Collections.Generic;

public class InventoryItemConsumer
{
    private Inventory _inventory;
    private readonly List<IInventoryItemConcumeHandler> _concumeHandlers;

    public InventoryItemConsumer(Inventory inventory)
    {
        _inventory = inventory;
    }


    public void AddConcumeHandle(IInventoryItemConcumeHandler concumeHandler)
    {
        _concumeHandlers.Add(concumeHandler);
    }

    public void RemoveConcumeHandle(IInventoryItemConcumeHandler concumeHandler)
    {
        _concumeHandlers.Remove(concumeHandler);
    }

    public void TryConsumeItem(InventoryItem item)
    {
        if (item.HasFlag(InventoryItemFlags.CONSUMABLE) && _inventory.HasItem(item.Name))
        {
            ConsumeItem(item);
        }
    }

    private void ConsumeItem(InventoryItem item)
    {
        _inventory.TryRemoveItem(item);

        foreach (var handler in _concumeHandlers)
        {
            handler.OnConsumeItem(item);
        }
    }
}