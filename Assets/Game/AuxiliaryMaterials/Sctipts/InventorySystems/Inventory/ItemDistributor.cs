public class ItemDistributor
{
    private readonly Inventory _mainInventory;
    private readonly EquipmentInventory _equipmentInventory;

    public ItemDistributor(Inventory mainInventory, EquipmentInventory equipmentInventory)
    {
        _mainInventory = mainInventory;
        _equipmentInventory = equipmentInventory;
    }


    public bool TryEquip(InventoryItem item)
    {
        if (_mainInventory.HasItem(item) && _equipmentInventory.TryAddEquipment(item))
        {
            _mainInventory.RemoveItem(item);
            return true;
        }

        return false;
    }

    public bool TryEquip(params InventoryItem[] items)
    {
        if (_mainInventory.HasItems(items) && _equipmentInventory.TryAddEquipments(items))
        {
            _mainInventory.RemoveItems(items);
            return true;
        }

        return false;
    }

    public bool TryUnequip(InventoryItem item)
    {
        if (_equipmentInventory.TryRemoveEquipment(item))
        {
            _mainInventory.AddItem(item);
            return true;
        }

        return false;
    }

    public bool TryUnequip(EquipmentType type)
    {
        if (_equipmentInventory.TryRemoveEquipment(type, out InventoryItem item))
        {
            _mainInventory.AddItem(item);
            return true;
        }

        return false;
    }

    public bool TryUnequip(params InventoryItem[] items)
    {
        if (_equipmentInventory.TryRemoveEquipments(items))
        {
            _mainInventory.AddItems(items);
            return true;
        }

        return false;
    }

    public bool TryUnequip(params EquipmentType[] types)
    {
        if (_equipmentInventory.TryRemoveEquipments(types, out InventoryItem[] items))
        {
            _mainInventory.AddItems(items);
            return true;
        }

        return false;
    }

    public bool TryUnequipAllEquipment()
    {
        if(_equipmentInventory.TryUnequipAllEquipment(out InventoryItem[] items))
        {
            _mainInventory.AddItems(items);
            return true;
        }

        return false;
    }
}
