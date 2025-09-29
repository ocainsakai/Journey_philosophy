using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private DialogueSet dialogueSet;
    public DialogueSet DialogueSet => dialogueSet;

    [SerializeField] DialogueTrigger trigger;

    private int currentIndex = 0;
    public void Awake()
    {
        if (dialogueSet != null && dialogueSet.dialoguesEntries.Any())
        {
            currentIndex = 0;
            trigger.startNode = dialogueSet.dialoguesEntries[currentIndex];
        }
        else
        {
            trigger.enabled = false;
        }
    }
    public void OnDialogueFinished()
    {
        currentIndex++;

        if (currentIndex < dialogueSet.dialoguesEntries.Count)
        {
            trigger.startNode = dialogueSet.dialoguesEntries[currentIndex];
        }
        else
        {
            trigger.enabled = false;
            Debug.Log($"{name} has no more dialogues. Trigger disabled.");
        }
    }
}
