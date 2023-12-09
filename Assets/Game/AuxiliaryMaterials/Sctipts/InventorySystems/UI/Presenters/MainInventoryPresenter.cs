using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MainInventoryPresenter : MonoBehaviour, IInventoryObserver
{
    [SerializeField] private ViewSlot[] _itemSlots;
    private ViewSlotPresenter[] _slotPresenters;
    private List<InventoryItem> _items = new();

    private Inventory _mainInventory;
    private ItemDistributor _distributor;


    [Inject]
    public void Construct(Inventory inventory, ItemDistributor distributor)
    {
        _mainInventory = inventory;
        _distributor = distributor;
        inventory.AddObserver(this);
    }

    private void Awake()
    {
        _slotPresenters = new ViewSlotPresenter[_itemSlots.Length];

        for (int i = 0; i < _slotPresenters.Length; i++)
        {
            _slotPresenters[i] = new ViewSlotPresenter(_itemSlots[i], _distributor, SlotPresenterType.Equipe);
        }
    }

    public void OnItemAdded(InventoryItem item)
    {
        _slotPresenters[_items.Count].SetItem(item);
        _items.Add(item);
    }

    public void OnItemRemoved(InventoryItem item)
    {
        _items.Remove(item);

        for (int i = 0; i < _slotPresenters.Length; i++)
        {
            _slotPresenters[i].RemoveItem();
            
            if (i < _items.Count)
            {
                _slotPresenters[i].SetItem(_items[i]);
            }
        }
    }
}