using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "New Item")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Art;
    public string Description;
}
