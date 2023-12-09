using System;
using System.Collections.Generic;
using System.Linq;

public sealed class EquipmentInventory
{
    private readonly List<IEquipmentObserver> _observers = new();
    private readonly Dictionary<EquipmentType, InventoryItem> _items = new()
    {
        {EquipmentType.HELM, null},
        {EquipmentType.ARMOUR, null},
        {EquipmentType.GLOVES, null},
        {EquipmentType.BOOTS, null},
        {EquipmentType.WEAPON, null},
        {EquipmentType.SHIELD, null},
    };


    public bool CanAddEquipment(InventoryItem newItem)
    {
        if (newItem == null) return false;
        if (!newItem.HasFlag(InventoryItemFlags.EQUIPPABLE)) return false;

        var type = newItem.GetComponent<EquipmentTypeComponent>().GetEquipmentType();
        if (HasEquipmentItem(type)) return false;

        return true;
    }

    public bool CanAddEquipments(params InventoryItem[] newItems)
    {
        if (newItems == null || newItems.Length == 0) return false;

        foreach (var newItem in newItems)
        {
            if (!CanAddEquipment(newItem)) return false;
        }

        return true;
    }

    public bool HasEquipmentItem(EquipmentType type)
    {
        if (!_items.ContainsKey(type)) return false;
        if (_items[type] != null) return true;
        return false;
    }

    public bool HasEquipmentItem(InventoryItem item)
    {
        if (item == null) return false;

        foreach (var equipmentItem in _items)
        {
            if (equipmentItem.Value == item) return true;
        }

        return false;
    }

    public bool TryAddEquipment(InventoryItem newItem)
    {
        if (!CanAddEquipment(newItem)) return false;

        AddEquipment(newItem);
        return true;
    }

    public bool TryAddEquipments(params InventoryItem[] newItems)
    {
        if (!CanAddEquipments(newItems)) return false;

        foreach (var newItem in newItems)
        {
            AddEquipment(newItem);
        }

        return true;
    }

    public bool TryRemoveEquipment(string name, out InventoryItem removedItem)
    {
        foreach (var item in _items)
        {
            if (item.Value.Name == name)
            {
                RemoveEquipment(item.Value);
                removedItem = item.Value;
                return true;
            }
        }

        removedItem = null;
        return false;
    }

    public bool TryRemoveEquipment(InventoryItem item)
    {
        if (item == null) return false;

        var type = item.GetComponent<EquipmentTypeComponent>().GetEquipmentType();
        if (!_items.ContainsKey(type)) return false;

        if (_items[type] == item)
        {
            RemoveEquipment(item);
            return true;
        }

        return false;
    }

    public bool TryRemoveEquipment(EquipmentType type, out InventoryItem removedItem)
    {
        if (!_items.ContainsKey(type))
        {
            removedItem = null;
            return false;
        }

        if (_items[type] != null)
        {
            removedItem = _items[type];
            RemoveEquipment(_items[type]);
            return true;
        }

        removedItem = null;
        return false;
    }

    public bool TryRemoveEquipments(InventoryItem[] items)
    {
        if (items == null || items.Length == 0) return false;

        foreach (var item in items)
        {
            if (!HasEquipmentItem(item))
            {
                return false;
            }
        }

        foreach (var item in items)
        {
            RemoveEquipment(item);
        }

        return true;
    }

    public bool TryRemoveEquipments(EquipmentType[] types, out InventoryItem[] items)
    {
        if (types == null || types.Length == 0)
        {
            items = null;
            return false;
        }

        List<InventoryItem> removedItem = new();

        foreach (var type in types)
        {
            if (!HasEquipmentItem(type))
            {
                items = null;
                return false;
            }
            else
            {
                removedItem.Add(_items[type]);
            }
        }

        foreach (var item in removedItem)
        {
            RemoveEquipment(item);
        }

        items = removedItem.ToArray();
        return true;
    }

    public bool TryUnequipAllEquipment(out InventoryItem[] removedItems)
    {
        if (TryGetAllEquipment(out InventoryItem[] allItems))
        {
            removedItems = allItems;

            foreach (var item in allItems)
            {
                RemoveEquipment(item);
            }

            return true;
        }

        removedItems = null;
        return false;
    }

    public bool TryGetAllEquipment(out InventoryItem[] allItems)
    {
        if (IsEmpty())
        {
            allItems = null;
            return false;
        }

        List<InventoryItem> items = new();

        foreach (var item in _items)
        {
            var value = item.Value;
            if (value != null)
            {
                items.Add(value);
            }
        }

        allItems = items.ToArray();
        return true;
    }

    public bool TryGetEquipmentItem(EquipmentType type, out InventoryItem item)
    {
        if (!_items.ContainsKey(type))
        {
            item = null;
            return false;
        }

        if (_items[type] != null)
        {
            item = _items[type];
            return true;
        }

        item = null;
        return false;
    }

    public bool IsEmpty()
    {
        foreach (var item in _items)
        {
            if (item.Value != null)
            {
                return false;
            }
        }

        return true;
    }

    public int GetItemsCount()
    {
        int count = 0;
        foreach (var item in _items)
        {
            if (item.Value != null)
            {
                count++;
            }
        }
        return count;
    }

    public void AddObserver(IEquipmentObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IEquipmentObserver observer) => _observers.Remove(observer);

    private void AddEquipment(InventoryItem newItem)
    {
        var type = newItem.GetComponent<EquipmentTypeComponent>().GetEquipmentType();
        _items[type] = newItem;
        OnAddEquipment(newItem, type);
    }

    private void RemoveEquipment(InventoryItem item)
    {
        var type = item.GetComponent<EquipmentTypeComponent>().GetEquipmentType();
        _items[type] = null;
        OnRemoveEquipment(item, type);
    }

    private void OnAddEquipment(InventoryItem item, EquipmentType type)
    {
        foreach (var observer in _observers)
        {
            observer.OnItemAdded(item, type);
        }
    }

    private void OnRemoveEquipment(InventoryItem item, EquipmentType type)
    {
        foreach (var observer in _observers)
        {
            observer.OnItemRemoved(item, type);
        }
    }
}