using System;
using VContainer;
using VContainer.Unity;

public class ViewPlayerStatsPresenter : IInitializable, IDisposable
{
    private Player _player;
    private ViewPlayerStats _viewPlayerStats;

    private const string HIT_POINTS = "HitPoints";
    private const string DAMAGE = "Damage";
    private const string SPEED = "Speed";
    private const string ARMOR = "Armor";
    private const string BLOCKED_DAMAGE = "BlockedDmg";


    [Inject]
    public void Construct(Player player, ViewPlayerStats viewPlayerStats)
    {
        _player = player;
        _viewPlayerStats = viewPlayerStats;

        UpdateOnInit();
    }

    public void Initialize()
    {
        _player.OnHitPointsChanged += UpdateHitPoints;
        _player.OnDamageChanged += UpdateDamage;
        _player.OnSpeedChanged += UpdateSpeed;
        _player.OnArmorChanged += UpdateArmor;
        _player.OnBlockedDamageChanged += UpdateBlockedDamage;
    }

    public void Dispose()
    {
        _player.OnHitPointsChanged -= UpdateHitPoints;
        _player.OnDamageChanged -= UpdateDamage;
        _player.OnSpeedChanged -= UpdateSpeed;
        _player.OnArmorChanged -= UpdateArmor;
        _player.OnBlockedDamageChanged -= UpdateBlockedDamage;
    }

    private void UpdateOnInit()
    {
        _viewPlayerStats.UpdateHitPoints(GetFormattedText(HIT_POINTS, _player.HitPoints));
        _viewPlayerStats.UpdateDamage(GetFormattedText(DAMAGE, _player.Damage));
        _viewPlayerStats.UpdateSpeed(GetFormattedText(SPEED, _player.Speed));
        _viewPlayerStats.UpdateArmor(GetFormattedText(ARMOR, _player.Armor));
        _viewPlayerStats.UpdateBlockedDamage(GetFormattedText(BLOCKED_DAMAGE, _player.BlockedDamage));
    }

    private string GetFormattedText(string text, int value) => $"{text}: {value}";

    private void UpdateHitPoints(int value)
    {
        _viewPlayerStats.UpdateHitPoints(GetFormattedText(HIT_POINTS, value));
    }

    private void UpdateDamage(int value)
    {
        _viewPlayerStats.UpdateDamage(GetFormattedText(DAMAGE, value));
    }

    private void UpdateSpeed(int value)
    {
        _viewPlayerStats.UpdateSpeed(GetFormattedText(SPEED, value));
    }

    private void UpdateArmor(int value)
    {
        _viewPlayerStats.UpdateArmor(GetFormattedText(ARMOR, value));
    }

    private void UpdateBlockedDamage(int value)
    {
        _viewPlayerStats.UpdateBlockedDamage(GetFormattedText(BLOCKED_DAMAGE, value));
    }
}