using UnityEngine;

public class EquipmentTypeComponent
{
    [SerializeField] private EquipmentType _type;
    
    public EquipmentType GetEquipmentType() => _type;
    public EquipmentType SetEquipmentType(EquipmentType type) => _type = type;
}