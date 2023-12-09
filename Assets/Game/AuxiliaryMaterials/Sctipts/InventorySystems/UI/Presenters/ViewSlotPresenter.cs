public enum SlotPresenterType
{
    Unequipe,
    Equipe,
}

public sealed class ViewSlotPresenter
{
    private readonly ViewSlot _viewSlot;
    private InventoryItem _item;
    private ItemDistributor _distributor;
    private SlotPresenterType _type;

    public ViewSlotPresenter(ViewSlot viewSlot, ItemDistributor itemDistributor, SlotPresenterType type)
    {
        _viewSlot = viewSlot;
        _distributor = itemDistributor;
        _type = type;
    }


    public void SetItem(InventoryItem item)
    {
        _item = item;
        _viewSlot.Show(_item.Metadata.Icon);
        _viewSlot.Button.onClick.AddListener(TryRemoveItem);
    }

    public void RemoveItem()
    {
        _viewSlot.Hide();
        _viewSlot.Button.onClick.RemoveListener(TryRemoveItem);
        _item = null;
    }

    private void TryRemoveItem()
    {
        if(_type == SlotPresenterType.Unequipe)
        {
            _distributor.TryUnequip(_item);
        }
        else if(_type == SlotPresenterType.Equipe)
        {
            _distributor.TryEquip(_item);
        }
    }
}