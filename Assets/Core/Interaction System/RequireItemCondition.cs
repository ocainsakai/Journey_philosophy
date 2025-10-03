using BulletHellTemplate;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Require Item")]
public class RequireItemCondition : BaseCondition
{
    public InventoryItem requiredItem;

    public override bool IsMet(PlayerController player)
    {
        return player.Inventory.HasItem(requiredItem);
    }

    public override string GetFailMessage()
    {
        return "You don't have the required item.";
    }
}

public abstract class BaseCondition : ScriptableObject, IInteractionCondition
{
    public abstract string GetFailMessage();

    public abstract bool IsMet(PlayerController player);
}