using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

public class Inventory
{
    [ShowInInspector, ReadOnly]
    private List<InventoryItem> _items = new();
    private readonly List<IInventoryObserver> _observers = new();


    public bool TrySetup(params InventoryItem[] newItems)
    {
        if (!CanAddItems(newItems)) return false;

        _items = new();

        AddItems(newItems);

        return true;
    }

    public void AddItem(InventoryItem newItem)
    {
        if (newItem == null)
        {
            throw new ArgumentNullException($"Item {newItem} is null!");
        }

        if (_items.Contains(newItem))
        {
            throw new ArgumentException($"Item {newItem.Name} is already listed!");
        }

        _items.Add(newItem);
        OnItemAdded(newItem);
    }

    public void AddItems(params InventoryItem[] newItems)
    {
        if (newItems == null || newItems.Length == 0)
        {
            throw new Exception($"Items is empty!");
        }

        foreach (var newItem in newItems)
        {
            AddItem(newItem);
        }
    }

    public bool TryAddItem(InventoryItem item)
    {
        if (item == null) return false;

        if (!_items.Contains(item))
        {
            _items.Add(item);
            OnItemAdded(item);
            return true;
        }

        return false;
    }

    public bool TryAddItems(params InventoryItem[] newItems)
    {
        if (CanAddItems(newItems))
        {
            AddItems(newItems);
            return true;
        }

        return false;
    }

    public bool CanAddItems(params InventoryItem[] newItems)
    {
        if (newItems == null || newItems.Length == 0) return false;

        for (int i = 0; i < newItems.Length; i++)
        {
            if (_items.Contains(newItems[i])) return false;

            for (int j = i + 1; j < newItems.Length; j++)
            {
                if (newItems[i] == newItems[j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void RemoveItem(InventoryItem item)
    {
        if (_items.Remove(item))
        {
            OnItemRemoved(item);
        }
        else
        {
            throw new InvalidOperationException($"Item {item} was not removed");
        }
    }

    public void RemoveItems(string name, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            TryRemoveItem(name);
        }
    }

    public void RemoveItems(params InventoryItem[] items)
    {
        if (items == null || items.Length == 0)
        {
            throw new Exception($"Items is empty!");
        }

        foreach (var item in items)
        {
            RemoveItem(item);
        }
    }

    public bool TryRemoveItem(string name)
    {
        if (FindItem(name, out var item))
        {
            TryRemoveItem(item);
            return true;
        }

        return false;
    }

    public bool TryRemoveItem(InventoryItem item)
    {
        if (item == null) return false;

        if (_items.Remove(item))
        {
            OnItemRemoved(item);
            return true;
        }

        return false;
    }

    public bool TryRemoveItems(InventoryItem[] items)
    {
        if (HasItems(items))
        {
            RemoveItems(items);
        }

        return false;
    }

    public IReadOnlyList<InventoryItem> GetItems()
    {
        return _items;
    }

    public int GetItemsCount()
    {
        return _items.Count;
    }

    public int GetItemsCount(string name)
    {
        return _items.Count(it => it.Name == name);
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (var intentoryItem in _items)
        {
            if (intentoryItem == item)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(string name)
    {
        foreach (var intentoryItem in _items)
        {
            if (intentoryItem.Name == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItems(params InventoryItem[] newItems)
    {
        if (newItems == null || newItems.Length == 0) return false;

        foreach (var newItem in newItems)
        {
            if (!_items.Contains(newItem))
            {
                return false;
            }
        }

        return true;
    }

    public void AddObserver(IInventoryObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IInventoryObserver observer)
    {
        _observers.Remove(observer);
    }

    private bool FindItem(string name, out InventoryItem result)
    {
        foreach (var intentoryItem in _items)
        {
            if (intentoryItem.Name == name)
            {
                result = intentoryItem;
                return true;
            }
        }

        result = null;
        return false;
    }

    private void OnItemAdded(InventoryItem item)
    {
        foreach (var observer in _observers)
        {
            observer.OnItemAdded(item);
        }
    }

    private void OnItemRemoved(InventoryItem item)
    {
        foreach (var observer in _observers)
        {
            observer.OnItemRemoved(item);
        }
    }
}
