using UnityEngine;
using VContainer;

public class MainInventoryInstaller : MonoBehaviour
{
    [SerializeField] private InventoryItemConfig[] _startItems;
    private Inventory _inventory;


    [Inject]
    public void Construct(Inventory inventory)
    {
        _inventory = inventory;
    }

    private void Start()
    {
        foreach (var item in _startItems)
        {
            _inventory.TryAddItem(item.Prototype.Clone());
        }
    }
}
