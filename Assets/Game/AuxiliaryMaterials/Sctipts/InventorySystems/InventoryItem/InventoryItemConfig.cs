using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem", order = 0)]
public class InventoryItemConfig : ScriptableObject
{
    [SerializeField] private InventoryItem _prototype;

    public InventoryItem Prototype
    {
        get { return _prototype; }
        set { _prototype = value; }
    }


    [ContextMenu(nameof(SaveData))]
    public void SaveData()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}