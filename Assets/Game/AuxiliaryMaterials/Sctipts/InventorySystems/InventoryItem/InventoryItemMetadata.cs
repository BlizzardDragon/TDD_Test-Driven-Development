using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public sealed class InventoryItemMetadata
{
    [PreviewField]
    public Sprite Icon;

    public string Title;
    
    [TextArea] 
    public string Description;
}
