using TMPro;
using UnityEngine;

public class ViewPlayerStats : MonoBehaviour
{
    [SerializeField] private TMP_Text _hitPoints;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _speed;
    [SerializeField] private TMP_Text _armor;
    [SerializeField] private TMP_Text _blockedDamage;


    public void UpdateBlockedDamage(string text) => _blockedDamage.text = text;
    public void UpdateArmor(string text) => _armor.text = text;
    public void UpdateSpeed(string text) => _speed.text = text;
    public void UpdateDamage(string text) => _damage.text = text;
    public void UpdateHitPoints(string text) => _hitPoints.text = text;
}