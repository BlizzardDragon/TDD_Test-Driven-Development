using VContainer;

public class EquipmentStatsApplier : IEquipmentObserver
{
    private Player _player;


    [Inject]
    public void Construct(Player player, EquipmentInventory equipmentInventory)
    {
        _player = player;
        equipmentInventory.AddObserver(this);
    }

    public void OnItemAdded(InventoryItem item, EquipmentType _)
    {
        var equipment = item.GetComponent<CharacterStatsComponent>();

        if (equipment.TryGetParameter<int>(CharacterStatsTypes.HIT_POINTS, out var hitPoints))
        {
            _player.AddHitPoints(hitPoints);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.DAMAGE, out var damage))
        {
            _player.AddDamage(damage);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.SPEED, out var speed))
        {
            _player.AddSpeed(speed);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.ARMOR, out var armor))
        {
            _player.AddArmor(armor);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.BLOCKED_DAMAGE, out var blockedDamage))
        {
            _player.AddBlockedDamage(blockedDamage);
        }
    }

    public void OnItemRemoved(InventoryItem item, EquipmentType _)
    {
        var equipment = item.GetComponent<CharacterStatsComponent>();

        if (equipment.TryGetParameter<int>(CharacterStatsTypes.HIT_POINTS, out var hitPoints))
        {
            _player.RemoveHitPoints(hitPoints);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.DAMAGE, out var damage))
        {
            _player.RemoveDamage(damage);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.SPEED, out var speed))
        {
            _player.RemoveSpeed(speed);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.ARMOR, out var armor))
        {
            _player.RemoveArmor(armor);
        }
        if (equipment.TryGetParameter<int>(CharacterStatsTypes.BLOCKED_DAMAGE, out var blockedDamage))
        {
            _player.RemoveBlockedDamage(blockedDamage);
        }
    }
}