using UnityEngine;
using UnityEngine.Events;


public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public DialogueNode startNode;
    public UnityEvent OnEndDialogue;
    public UnityEvent OnCondition;
    public string GetInteractionPrompt()
    {
        return startNode.speakerName;
    }

    public void Interact()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        Debug.Log("trigger" + OnCondition);
        DialogueManager.Instance.StartDialogue(startNode, this,() => OnCondition?.Invoke());
    }
}
