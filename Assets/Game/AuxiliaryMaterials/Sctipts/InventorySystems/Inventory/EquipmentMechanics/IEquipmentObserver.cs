public interface IEquipmentObserver
{
    void OnItemAdded(InventoryItem item, EquipmentType equipmentType);
    void OnItemRemoved(InventoryItem item, EquipmentType equipmentType);
}