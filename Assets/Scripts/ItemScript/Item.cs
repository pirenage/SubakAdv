using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "SubakAdv/Item", order = 0)]
public class Item : ScriptableObject
{
    public bool stackable;
    public bool IsBreakable;
    public string Name;
    public int MaxStack;
    public string Description;
    public Sprite Icon;
}