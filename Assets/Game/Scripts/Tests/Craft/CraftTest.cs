using NUnit.Framework;

[TestFixture]
public class CraftTest
{
    private InventoryItemConfig _woodItem;
    private InventoryItemConfig _stoneItem;
    private InventoryItemConfig _steelItem;

    private InventoryItemConfig _axeItem;
    private InventoryItemConfig _swordItem;

    private InventoryItemRecipe _axeRecipe;
    private InventoryItemRecipe _swordRecipe;

    private Inventory _inventory;
    private InventoryItemCrafter _crafter;


    [SetUp]
    public void SetUp()
    {
        _inventory = new();
        _crafter = new(_inventory);

        _woodItem = Substitute.CreateItem("Wood");
        _stoneItem = Substitute.CreateItem("Stone");
        _steelItem = Substitute.CreateItem("Steel");
        _axeItem = Substitute.CreateItem("Axe");
        _swordItem = Substitute.CreateItem("Sword");

        _axeRecipe = Substitute.CreateRecipe(_axeItem, new[]
        {
            new InventoryItemIngredient()
            {
                Item = _woodItem,
                Amount = 2,
            },
            new InventoryItemIngredient()
            {
                Item = _stoneItem,
                Amount = 1,
            },
        });

        _swordRecipe = Substitute.CreateRecipe(_swordItem, new[]
        {
            new InventoryItemIngredient()
            {
                Item = _woodItem,
                Amount = 1,
            },
            new InventoryItemIngredient()
            {
                Item = _steelItem,
                Amount = 1,
            },
        });
    }

    [Test]
    public void AxeCrafting()
    {
        // Axe: 2 wood, 1 stone.

        // Arrange: (установка)
        _inventory.TryAddItem(_woodItem.Prototype.Clone());
        _inventory.TryAddItem(_woodItem.Prototype.Clone());
        _inventory.TryAddItem(_stoneItem.Prototype.Clone());

        // Act: (действие)
        _crafter.Craft(_axeRecipe);

        // Assert: (проверка на результат)
        Assert.True(_inventory.GetItemsCount(_woodItem.Prototype.Name) == 0);
        Assert.True(_inventory.GetItemsCount(_stoneItem.Prototype.Name) == 0);
        Assert.True(_inventory.GetItemsCount(_axeItem.Prototype.Name) == 1);
    }

    [Test]
    public void SwordCrafting()
    {
        // Sword: 1 wood, 1 steel.

        // Arrange: (установка)
        _inventory.TryAddItem(_woodItem.Prototype.Clone());
        _inventory.TryAddItem(_steelItem.Prototype.Clone());

        // Act: (действие)
        _crafter.Craft(_swordRecipe);

        // Assert: (проверка на результат)
        Assert.True(_inventory.GetItemsCount(_woodItem.Prototype.Name) == 0);
        Assert.True(_inventory.GetItemsCount(_steelItem.Prototype.Name) == 0);
        Assert.True(_inventory.GetItemsCount(_swordItem.Prototype.Name) == 1);
    }

    [Test]
    public void SwordCraftingNotEnougth()
    {
        // Sword: 1 wood, 1 steel.

        // Arrange: (установка)
        _inventory.TryAddItem(_woodItem.Prototype.Clone());

        // Act: (действие)
        Assert.Catch(() => _crafter.Craft(_swordRecipe));

        // Assert: (проверка на результат)
        Assert.True(_inventory.GetItemsCount("Wood") == 1);
        Assert.True(_inventory.GetItemsCount("Sword") == 0);
    }
}