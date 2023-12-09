public static class EquipmentTypeComponentCreator
{
    public static EquipmentTypeComponent Create(EquipmentType type)
    {
        var component = new EquipmentTypeComponent();
        component.SetEquipmentType(type);  
        return component;
    }
}