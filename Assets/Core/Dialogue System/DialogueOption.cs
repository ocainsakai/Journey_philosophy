using UnityEngine;

[CreateAssetMenu (menuName = "Dialogue/Option", fileName = "New Dialogue Option")]
public class DialogueOption : ScriptableObject
{
    public string optionText;        
    public DialogueNode nextNode;    
}
