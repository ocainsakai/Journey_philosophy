using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Node", fileName = "New Dialogue Node")]

public class DialogueNode : ScriptableObject
{
    public string speakerName;       
    [TextArea] public string dialogueText; 
    public DialogueOption[] options; 
}
