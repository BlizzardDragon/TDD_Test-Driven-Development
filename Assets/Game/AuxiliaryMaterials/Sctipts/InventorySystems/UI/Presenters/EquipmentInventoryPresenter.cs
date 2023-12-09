using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EquipmentInventoryPresenter : MonoBehaviour, IEquipmentObserver
{
    [SerializeField] private ViewSlot _weapon;
    [SerializeField] private ViewSlot _shield;
    [SerializeField] private ViewSlot _helm;
    [SerializeField] private ViewSlot _armour;
    [SerializeField] private ViewSlot _gloves;
    [SerializeField] private ViewSlot _boots;

    private Dictionary<EquipmentType, ViewSlotPresenter> _slotPresenters;

    private ItemDistributor _distributor;


    [Inject]
    public void Construct(ItemDistributor distributor, EquipmentInventory equipmentInventory)
    {
        _distributor = distributor;
        equipmentInventory.AddObserver(this);
    }

    private void Awake()
    {
        _slotPresenters = new()
        {
            {EquipmentType.WEAPON,  new ViewSlotPresenter(_weapon, _distributor, SlotPresenterType.Unequipe)},
            {EquipmentType.SHIELD,  new ViewSlotPresenter(_shield, _distributor, SlotPresenterType.Unequipe)},
            {EquipmentType.HELM,  new ViewSlotPresenter(_helm, _distributor, SlotPresenterType.Unequipe)},
            {EquipmentType.ARMOUR,  new ViewSlotPresenter(_armour, _distributor, SlotPresenterType.Unequipe)},
            {EquipmentType.GLOVES,  new ViewSlotPresenter(_gloves, _distributor, SlotPresenterType.Unequipe)},
            {EquipmentType.BOOTS,  new ViewSlotPresenter(_boots, _distributor, SlotPresenterType.Unequipe)},
        };
    }

    public void OnItemAdded(InventoryItem item, EquipmentType equipmentType)
    {
        ViewSlotPresenter slotPresenter = _slotPresenters[equipmentType];
        slotPresenter.SetItem(item);
    }

    public void OnItemRemoved(InventoryItem _, EquipmentType equipmentType)
    {
        ViewSlotPresenter slotPresenter = _slotPresenters[equipmentType];
        slotPresenter.RemoveItem();
    }
}