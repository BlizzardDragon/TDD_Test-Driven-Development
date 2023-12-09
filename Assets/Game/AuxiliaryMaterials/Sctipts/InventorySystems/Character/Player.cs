using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ShowInInspector, ReadOnly] private int _hitPoints = DEFAULT_HIT_POINTS;
    [ShowInInspector, ReadOnly] private int _damage = DEFAULT_DAMAGE;
    [ShowInInspector, ReadOnly] private int _speed = DEFAULT_SPEED;
    [ShowInInspector, ReadOnly] private int _armor = DEFAULT_ARMOR;
    [ShowInInspector, ReadOnly] private int _blockedDamage = DEFAULT_BLOCKED_DAMAGE;

    public int HitPoints => _hitPoints;
    public int Damage => _damage;
    public int Speed => _speed;
    public int Armor => _armor;
    public int BlockedDamage => _blockedDamage;

    public event Action<int> OnHitPointsChanged;
    public event Action<int> OnDamageChanged;
    public event Action<int> OnSpeedChanged;
    public event Action<int> OnArmorChanged;
    public event Action<int> OnBlockedDamageChanged;

    private const int DEFAULT_HIT_POINTS = 50;
    private const int DEFAULT_DAMAGE = 0;
    private const int DEFAULT_SPEED = 5;
    private const int DEFAULT_ARMOR = 0;
    private const int DEFAULT_BLOCKED_DAMAGE = 0;


    public void AddHitPoints(int value)
    {
        _hitPoints += value;
        OnHitPointsChanged?.Invoke(_hitPoints);
    }

    public void RemoveHitPoints(int value)
    {
        _hitPoints -= value;
        OnHitPointsChanged?.Invoke(_hitPoints);
    }

    public void AddDamage(int value)
    {
        _damage += value;
        OnDamageChanged?.Invoke(_damage);
    }

    public void RemoveDamage(int value)
    {
        _damage -= value;
        OnDamageChanged?.Invoke(_damage);
    }

    public void AddSpeed(int value)
    {
        _speed += value;
        OnSpeedChanged?.Invoke(_speed);
    }

    public void RemoveSpeed(int value)
    {
        _speed -= value;
        OnSpeedChanged?.Invoke(_speed);
    }

    public void AddArmor(int value)
    {
        _armor += value;
        OnArmorChanged?.Invoke(_armor);
    }

    public void RemoveArmor(int value)
    {
        _armor -= value;
        OnArmorChanged?.Invoke(_armor);
    }

    public void AddBlockedDamage(int value)
    {
        _blockedDamage += value;
        OnBlockedDamageChanged?.Invoke(_blockedDamage);
    }

    public void RemoveBlockedDamage(int value)
    {
        _blockedDamage -= value;
        OnBlockedDamageChanged?.Invoke(_blockedDamage);
    }
}
