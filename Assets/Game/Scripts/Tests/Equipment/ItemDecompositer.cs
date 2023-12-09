public static class ItemDecompositer
{
    public static int GetCharacterStatIntValue(InventoryItem item, CharacterStatsTypes type)
    {
        return item.GetComponent<CharacterStatsComponent>().GetParameter<int>(type);
    }

    public static EquipmentType GetEquipmentType(InventoryItem item)
    {
        return item.GetComponent<EquipmentTypeComponent>().GetEquipmentType();
    }
}