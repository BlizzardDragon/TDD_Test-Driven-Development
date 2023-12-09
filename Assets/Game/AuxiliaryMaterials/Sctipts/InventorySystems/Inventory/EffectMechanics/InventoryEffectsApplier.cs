using Entities;
using Game.GameEngine.Mechanics;

public sealed class InventoryEffectsApplier : IInventoryObserver
{
    private IEntity _player;

    public InventoryEffectsApplier(IEntity player)
    {
        _player = player;
    }


    public void OnItemAdded(InventoryItem item)
    {
        if (IsEffectable(item))
        {
            var effect = GetEffect(item);
            _player.Get<IComponent_Effector>().Apply(effect);
        }
    }

    public void OnItemRemoved(InventoryItem item)
    {
        if (IsEffectable(item))
        {
            var effect = GetEffect(item);
            _player.Get<IComponent_Effector>().Discard(effect);
        }
    }

    private static bool IsEffectable(InventoryItem item)
    {
        return item.Flags.HasFlag(InventoryItemFlags.EFFECTIBLE);
    }

    private static IEffect GetEffect(InventoryItem item)
    {
        return item.GetComponent<IComponent_GetEffect>().Effect;
    }
}
