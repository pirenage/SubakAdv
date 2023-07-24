using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public bool stackable;
    public string Name;
    public Sprite Icon;
    public ToolAction onAction;
    public ToolAction onTilemapAction;
    public ToolAction onItemUsed;
    public crop Crop;
}
