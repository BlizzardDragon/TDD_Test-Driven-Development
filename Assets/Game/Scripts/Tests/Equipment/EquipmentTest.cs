using NUnit.Framework;

[TestFixture]
public class EquipmentTest
{
    private Player _player;
    private Inventory _mainInventory;
    private EquipmentInventory _equipmentInventory;
    private EquipmentStatsApplier _equipmentStatsApplier;
    private ItemDistributor _itemDistributor;
    private InventoryItemSubstitutions _itemSubstitutions;


    [SetUp]
    public void SetUp()
    {
        _player = new();
        _mainInventory = new();
        _equipmentInventory = new();
        _equipmentStatsApplier = new();
        _itemSubstitutions = new();

        _itemDistributor = new(_mainInventory, _equipmentInventory);
        _equipmentStatsApplier.Construct(_player, _equipmentInventory);
        _itemSubstitutions.Init();
    }

    #region CommonTests

    [Test]
    public void EquipSimpleBoots_1()
    {
        // Player stats: +5 speed, +3 armor.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 1.
        // EquipmentInventory: item in slot(BOOTS) == _itemSubstitutions.SimpleBoots.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;
        _mainInventory.TryAddItem(boots);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;

        // Act: (действие)
        _itemDistributor.TryEquip(boots);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == 5);
        Assert.True(_player.Armor - startArmor == 3);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 1);

        _equipmentInventory.TryGetEquipmentItem(EquipmentType.BOOTS, out var item);
        Assert.True(item == boots);
    }

    [Test]
    public void EquipSimpleBoots_2()
    {
        // Player stats: +0 speed, +0 armor.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // EquipmentInventory: HasEquipmentItem == false.

        // Arrange: (установка)
        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var boots = _itemSubstitutions.SimpleBoots;

        // Act: (действие)
        _itemDistributor.TryEquip(boots);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == 0);
        Assert.True(_player.Armor - startArmor == 0);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.HasEquipmentItem(boots) == false);
        Assert.True(_equipmentInventory.HasEquipmentItem(EquipmentType.BOOTS) == false);
    }

    [Test]
    public void TryEquipSimpleBoots()
    {
        // Player stats: +5 speed, +3 armor.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 1.
        // EquipmentInventory: item in slot(BOOTS) == _itemSubstitutions.SimpleBoots.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;
        _mainInventory.TryAddItem(boots);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;

        // Act: (действие)
        _itemDistributor.TryEquip(boots);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == 5);
        Assert.True(_player.Armor - startArmor == 3);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 1);

        _equipmentInventory.TryGetEquipmentItem(EquipmentType.BOOTS, out var item);
        Assert.True(item == boots);
    }

    [Test]
    public void UnequipSimpleBoots()
    {
        // Player stats: -5 speed, -3 armor.
        // MainInventory: itemsCount == 1.
        // EquipmentInventory: itemsCount == 0.
        // EquipmentInventory: HasEquipmentItem == false.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;
        _mainInventory.TryAddItem(boots);
        _itemDistributor.TryEquip(boots);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;

        // Act: (действие)
        _equipmentInventory.TryGetEquipmentItem(EquipmentType.BOOTS, out var item);
        _itemDistributor.TryUnequip(item);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == -5);
        Assert.True(_player.Armor - startArmor == -3);
        Assert.True(_mainInventory.GetItemsCount() == 1);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.HasEquipmentItem(boots) == false);
        Assert.True(_equipmentInventory.HasEquipmentItem(EquipmentType.BOOTS) == false);
    }

    [Test]
    public void UnequipSlotBoots_1()
    {
        // Player stats: -5 speed, -3 armor.
        // MainInventory: itemsCount == 1.
        // EquipmentInventory: itemsCount == 0.
        // EquipmentInventory: HasEquipmentItem == false.
        // actResult == true.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;
        _mainInventory.TryAddItem(boots);
        _itemDistributor.TryEquip(boots);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        bool actResult;

        // Act: (действие)
        actResult = _itemDistributor.TryUnequip(EquipmentType.BOOTS);

        // Assert: (проверка на результат)
        Assert.True(actResult == true);
        Assert.True(_player.Speed - startSpeed == -5);
        Assert.True(_player.Armor - startArmor == -3);
        Assert.True(_mainInventory.GetItemsCount() == 1);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.HasEquipmentItem(boots) == false);
        Assert.True(_equipmentInventory.HasEquipmentItem(EquipmentType.BOOTS) == false);
    }

    [Test]
    public void UnequipSlotBoots_2()
    {
        // Player stats: -0 speed, -0 armor.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        bool actResult;

        // Act: (действие)
        actResult = _itemDistributor.TryUnequip(EquipmentType.BOOTS);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_player.Speed - startSpeed == 0);
        Assert.True(_player.Armor - startArmor == 0);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void TryEquipHeavyBootsThenSimpleBoots()
    {
        // Player stats: +4 speed, +5 armor, +10 hit points.
        // MainInventory: itemsCount == 1.
        // EquipmentInventory: itemsCount == 1.
        // EquipmentInventory: item in slot(BOOTS) == _itemSubstitutions.HeavyBoots.

        // Arrange: (установка)
        var simpleBoots = _itemSubstitutions.SimpleBoots;
        var heavyBoots = _itemSubstitutions.HeavyBoots;
        _mainInventory.TrySetup(simpleBoots, heavyBoots);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var startHitPoints = _player.HitPoints;

        // Act: (действие)
        _itemDistributor.TryEquip(heavyBoots);
        _itemDistributor.TryEquip(simpleBoots);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == 4);
        Assert.True(_player.Armor - startArmor == 5);
        Assert.True(_player.HitPoints - startHitPoints == 10);
        Assert.True(_mainInventory.GetItemsCount() == 1);
        Assert.True(_equipmentInventory.GetItemsCount() == 1);

        _equipmentInventory.TryGetEquipmentItem(EquipmentType.BOOTS, out var item);
        Assert.True(item == heavyBoots);
    }

    [Test]
    public void TryEquipEffect()
    {
        // Player stats: +0 speed, +0 armor.
        // MainInventory: itemsCount == 1.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var effect = _itemSubstitutions.Effect;
        _mainInventory.TryAddItem(effect);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;

        // Act: (действие)
        var actResult = _itemDistributor.TryEquip(effect);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_player.Speed - startSpeed == 0);
        Assert.True(_player.Armor - startArmor == 0);
        Assert.True(_mainInventory.GetItemsCount() == 1);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void EquipAllEquipment()
    {
        // Player stats: +5 speed, +41 armor, +73 hit points, +35 danage, +9 blockedDamage.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 6.
        // actResult == true.

        // Arrange: (установка)
        var equipmentItems = _itemSubstitutions.GetAllEquipmentItems();
        _mainInventory.TrySetup(equipmentItems);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var startDamage = _player.Damage;
        var startHitPoints = _player.HitPoints;
        var startBlockedDamage = _player.BlockedDamage;

        // Act: (действие)
        var actResult = _itemDistributor.TryEquip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == 5);
        Assert.True(_player.Armor - startArmor == 41);
        Assert.True(_player.HitPoints - startHitPoints == 73);
        Assert.True(_player.Damage - startDamage == 35);
        Assert.True(_player.BlockedDamage - startBlockedDamage == 9);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 6);
        Assert.True(actResult == true);
    }

    [Test]
    public void UnequipAllEquipment_1()
    {
        // Player stats: -5 speed, -41 armor, -73 hit points, -35 danage, -9 blockedDamage.
        // MainInventory: itemsCount == 6.
        // EquipmentInventory: itemsCount == 0.
        // actResult == true.

        // Arrange: (установка)
        var equipmentItems = _itemSubstitutions.GetAllEquipmentItems();

        _mainInventory.TrySetup(equipmentItems);
        _itemDistributor.TryEquip(equipmentItems);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var startDamage = _player.Damage;
        var startHitPoints = _player.HitPoints;
        var startBlockedDamage = _player.BlockedDamage;

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == -5);
        Assert.True(_player.Armor - startArmor == -41);
        Assert.True(_player.HitPoints - startHitPoints == -73);
        Assert.True(_player.Damage - startDamage == -35);
        Assert.True(_player.BlockedDamage - startBlockedDamage == -9);
        Assert.True(_mainInventory.GetItemsCount() == 6);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == true);
    }

    [Test]
    public void UnequipAllEquipment_2()
    {
        // Player stats: -5 speed, -41 armor, -73 hit points, -35 danage, -9 blockedDamage.
        // MainInventory: itemsCount == 6.
        // EquipmentInventory: itemsCount == 0.
        // actResult == true.

        // Arrange: (установка)
        var equipmentItems = _itemSubstitutions.GetAllEquipmentItems();

        _mainInventory.TrySetup(equipmentItems);
        _itemDistributor.TryEquip(equipmentItems);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var startDamage = _player.Damage;
        var startHitPoints = _player.HitPoints;
        var startBlockedDamage = _player.BlockedDamage;

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(
            EquipmentType.ARMOUR,
            EquipmentType.BOOTS,
            EquipmentType.GLOVES,
            EquipmentType.HELM,
            EquipmentType.SHIELD,
            EquipmentType.WEAPON
        );

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == -5);
        Assert.True(_player.Armor - startArmor == -41);
        Assert.True(_player.HitPoints - startHitPoints == -73);
        Assert.True(_player.Damage - startDamage == -35);
        Assert.True(_player.BlockedDamage - startBlockedDamage == -9);
        Assert.True(_mainInventory.GetItemsCount() == 6);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == true);
    }

    [Test]
    public void UnequipAllEquipment_3()
    {
        // Player stats: -5 speed, -41 armor, -73 hit points, -35 danage, -9 blockedDamage.
        // MainInventory: itemsCount == 6.
        // EquipmentInventory: itemsCount == 0.
        // actResult == true.

        // Arrange: (установка)
        var equipmentItems = _itemSubstitutions.GetAllEquipmentItems();

        _mainInventory.TrySetup(equipmentItems);
        _itemDistributor.TryEquip(equipmentItems);

        var startSpeed = _player.Speed;
        var startArmor = _player.Armor;
        var startDamage = _player.Damage;
        var startHitPoints = _player.HitPoints;
        var startBlockedDamage = _player.BlockedDamage;

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequipAllEquipment();

        // Assert: (проверка на результат)
        Assert.True(_player.Speed - startSpeed == -5);
        Assert.True(_player.Armor - startArmor == -41);
        Assert.True(_player.HitPoints - startHitPoints == -73);
        Assert.True(_player.Damage - startDamage == -35);
        Assert.True(_player.BlockedDamage - startBlockedDamage == -9);
        Assert.True(_mainInventory.GetItemsCount() == 6);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == true);
    }

    [Test]
    public void TryAddBootsToInventory()
    {
        // actResult == true.
        // MainInventory: itemsCount == 1.
        // MainInventory: HasItem == true.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;

        // Act: (действие)
        var actResult = _mainInventory.TryAddItem(boots);

        // Assert: (проверка на результат)
        Assert.True(actResult == true);
        Assert.True(_mainInventory.GetItemsCount() == 1);
        Assert.True(_mainInventory.HasItem(boots) == true);
    }

    [Test]
    public void TryRemoveBootsFromInventory()
    {
        // actResult == true.
        // MainInventory: itemsCount == 0.
        // MainInventory: HasItem == false.

        // Arrange: (установка)
        var boots = _itemSubstitutions.SimpleBoots;
        _mainInventory.TryAddItem(boots);

        // Act: (действие)
        var actResult = _mainInventory.TryRemoveItem(boots.Name);

        // Assert: (проверка на результат)
        Assert.True(actResult == true);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_mainInventory.HasItem(boots) == false);
    }
        
    #endregion

    #region NullAndEmptyTests

    [Test]
    public void TryAddNullToInventory()
    {
        // actResult == false.
        // MainInventory: itemsCount == 1.

        // Arrange: (установка)
        _mainInventory.TrySetup(_itemSubstitutions.SimpleBoots);
        InventoryItem item = null;

        // Act: (действие)
        var actResult = _mainInventory.TryAddItem(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_mainInventory.GetItemsCount() == 1);
    }

    [Test]
    public void TryRemoveNullFromInventory()
    {
        // actResult == false.
        // MainInventory: itemsCount == 1.

        // Arrange: (установка)
        _mainInventory.TrySetup(_itemSubstitutions.SimpleBoots);
        InventoryItem item = null;
        _mainInventory.TryAddItem(item);

        // Act: (действие)
        var actResult = _mainInventory.TryRemoveItem(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_mainInventory.GetItemsCount() == 1);
    }

    [Test]
    public void TryEquipNull_1()
    {
        // actResult == false.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.

        // Arrange: (установка)
        InventoryItem item = null;
        _mainInventory.TryAddItem(item);

        // Act: (действие)
        var actResult = _itemDistributor.TryEquip(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void TryEquipNull_2()
    {
        // actResult == false.
        // EquipmentInventory: itemsCount == 0.

        // Arrange: (установка)
        InventoryItem item = null;

        // Act: (действие)
        var actResult = _equipmentInventory.TryAddEquipment(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void TryUnequipNull_1()
    {
        // actResult == false.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.

        // Arrange: (установка)
        InventoryItem item = null;
        _mainInventory.TryAddItem(item);
        _itemDistributor.TryEquip(item);

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void TryUnequipNull_2()
    {
        // actResult == false.
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.

        // Arrange: (установка)
        InventoryItem item = null;
        _equipmentInventory.TryAddEquipment(item);

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(item);

        // Assert: (проверка на результат)
        Assert.True(actResult == false);
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
    }

    [Test]
    public void TrySetupNullListToInventory()
    {
        // MainInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] items = null;

        // Act: (действие)
        var actResult = _mainInventory.TrySetup(items);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryRemoveNullListFromInventory()
    {
        // MainInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] items = null;
        _mainInventory.TrySetup(items);

        // Act: (действие)
        var actResult = _mainInventory.TryRemoveItems(items);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TrySetupEmptyListToInventory()
    {
        // MainInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var items = new InventoryItem[0];

        // Act: (действие)
        var actResult = _mainInventory.TrySetup(items);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryRemoveEmptyListFromInventory()
    {
        // MainInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var items = new InventoryItem[0];
        _mainInventory.TrySetup(items);

        // Act: (действие)
        var actResult = _mainInventory.TryRemoveItems(items);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryEquipNullList_1()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] equipmentItems = null;
        _mainInventory.TrySetup(equipmentItems);

        // Act: (действие)
        var actResult = _itemDistributor.TryEquip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryEquipNullList_2()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] equipmentItems = null;

        // Act: (действие)
        var actResult = _equipmentInventory.TryAddEquipments(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryEquipEmptyList_1()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var equipmentItems = new InventoryItem[0];
        _mainInventory.TrySetup(equipmentItems);

        // Act: (действие)
        var actResult = _itemDistributor.TryEquip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryEquipEmptyList_2()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var equipmentItems = new InventoryItem[0];

        // Act: (действие)
        var actResult = _equipmentInventory.TryAddEquipments(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryUnequipNullList_1()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] equipmentItems = null;
        _mainInventory.TrySetup(equipmentItems);
        _itemDistributor.TryEquip(equipmentItems);

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryUnequipNullList_2()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        InventoryItem[] equipmentItems = null;
        _equipmentInventory.TryAddEquipments(equipmentItems);

        // Act: (действие)
        var actResult = _equipmentInventory.TryRemoveEquipments(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryUnequipEmptyList_1()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var equipmentItems = new InventoryItem[0];
        _mainInventory.TrySetup(equipmentItems);
        _itemDistributor.TryEquip(equipmentItems);

        // Act: (действие)
        var actResult = _itemDistributor.TryUnequip(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }

    [Test]
    public void TryUnequipEmptyList_2()
    {
        // MainInventory: itemsCount == 0.
        // EquipmentInventory: itemsCount == 0.
        // actResult == false.

        // Arrange: (установка)
        var equipmentItems = new InventoryItem[0];
        _equipmentInventory.TryAddEquipments(equipmentItems);

        // Act: (действие)
        var actResult = _equipmentInventory.TryRemoveEquipments(equipmentItems);

        // Assert: (проверка на результат)
        Assert.True(_mainInventory.GetItemsCount() == 0);
        Assert.True(_equipmentInventory.GetItemsCount() == 0);
        Assert.True(actResult == false);
    }
        
    #endregion
}
