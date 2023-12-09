using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

public class MainInventoryDebug : MonoBehaviour
{
    [PropertyOrder(-10)]
    [ShowInInspector]
    public int _itemsCount => Application.isPlaying ? _inventory.GetItemsCount() : 0;

    [Space]
    [ShowInInspector]
    private Inventory _inventory;


    [Inject]
    public void Construct(Inventory inventory) => _inventory = inventory;

    [Button]
    public void AddItem(InventoryItemConfig config) => _inventory.TryAddItem(config.Prototype.Clone());

    [Button]
    public void TryRemoveItem(InventoryItemConfig config) => _inventory.TryRemoveItem(config.Prototype.Name);
}