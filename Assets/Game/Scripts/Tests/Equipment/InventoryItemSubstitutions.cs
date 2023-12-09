public class InventoryItemSubstitutions
{
    private InventoryItem _effect;
    private InventoryItem _simpleBoots;
    private InventoryItem _heavyBoots;
    private InventoryItem _lightArmour;
    private InventoryItem _woodShield;
    private InventoryItem _leatherGloves;
    private InventoryItem _heroicHelmet;
    private InventoryItem _axe;

    public InventoryItem Effect  => _effect;
    public InventoryItem SimpleBoots  => _simpleBoots;
    public InventoryItem HeavyBoots  => _heavyBoots;
    public InventoryItem LightArmour  => _lightArmour;
    public InventoryItem WoodShield  => _woodShield;
    public InventoryItem LeatherGloves  => _leatherGloves;
    public InventoryItem HeroicHelmet  => _heroicHelmet;
    public InventoryItem Axe  => _axe;

    private InventoryItem[] _allEquippableItems;


    public void Init()
    {
        var bootsComponent = EquipmentTypeComponentCreator.Create(EquipmentType.BOOTS);
        var weaponComponent = EquipmentTypeComponentCreator.Create(EquipmentType.WEAPON);
        var shieldComponent = EquipmentTypeComponentCreator.Create(EquipmentType.SHIELD);
        var armourComponent = EquipmentTypeComponentCreator.Create(EquipmentType.ARMOUR);
        var glovesComponent = EquipmentTypeComponentCreator.Create(EquipmentType.GLOVES);
        var helmComponent = EquipmentTypeComponentCreator.Create(EquipmentType.HELM);

        object[] heavyBootsComponents = new object[]
        {
            bootsComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.SPEED, 4),
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 5),
                new IntCharacterStats(CharacterStatsTypes.HIT_POINTS, 10)
            )
        };

        object[] simpleBootsComponents = new object[]
        {
            bootsComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.SPEED, 5),
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 3)
            )
        };

        object[] lightArmourComponents = new object[]
        {
            armourComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 10),
                new IntCharacterStats(CharacterStatsTypes.HIT_POINTS, 25),
                new IntCharacterStats(CharacterStatsTypes.BLOCKED_DAMAGE, 2)
            )
        };

        object[] woodShieldComponents = new object[]
        {
            shieldComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 5),
                new IntCharacterStats(CharacterStatsTypes.BLOCKED_DAMAGE, 7)
            )
        };

        object[] leatherGloves = new object[]
        {
            glovesComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 3),
                new IntCharacterStats(CharacterStatsTypes.HIT_POINTS, 8)
            )
        };

        object[] heroicHelmet = new object[]
        {
            helmComponent,
            new CharacterStatsComponent
            (
                new IntCharacterStats(CharacterStatsTypes.ARMOR, 20),
                new IntCharacterStats(CharacterStatsTypes.HIT_POINTS, 40)
            )
        };

        object[] axeComponents = new object[]
        {
            weaponComponent,
            new CharacterStatsComponent(new IntCharacterStats(CharacterStatsTypes.DAMAGE, 35))
        };

        _effect = new InventoryItem("", InventoryItemFlags.EFFECTIBLE, null, simpleBootsComponents);

        _simpleBoots = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, simpleBootsComponents);
        _heavyBoots = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, heavyBootsComponents);
        _lightArmour = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, lightArmourComponents);
        _woodShield = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, woodShieldComponents);
        _leatherGloves = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, leatherGloves);
        _heroicHelmet = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, heroicHelmet);
        _axe = new InventoryItem("", InventoryItemFlags.EQUIPPABLE, null, axeComponents);

        _allEquippableItems = new InventoryItem[] 
        {
            _simpleBoots,
            _lightArmour,
            _woodShield,
            _leatherGloves,
            _heroicHelmet,
            _axe,
        };
    }

    public InventoryItem[] GetAllEquipmentItems() => _allEquippableItems;
}