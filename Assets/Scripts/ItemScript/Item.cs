using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "SubakAdv/Item", order = 0)]
public class Item : ScriptableObject
{
    public bool stackable;
    public bool IsBreakable;
    public string Name;
    public string Description;
    public Sprite Icon;


}