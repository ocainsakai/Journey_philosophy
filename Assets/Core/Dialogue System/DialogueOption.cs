using BulletHellTemplate;
using UnityEngine;

[CreateAssetMenu (menuName = "Dialogue/Option", fileName = "New Dialogue Option")]
public class DialogueOption : ScriptableObject
{
    [TextArea]
    public string optionText;        
    public DialogueNode nextNode;

    [Header("Condition")]
    public InventoryItem requiredItem;
    public bool removeItemOnUse;

    [Header("Rewards")]
    public InventoryItem rewardItem;
    public int rewardQuantity = 1;

    public bool HasCondition;
    public bool EndTrigger;
}
