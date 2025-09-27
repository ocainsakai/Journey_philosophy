using System.Linq;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private DialogueSet dialogueSet;
    public DialogueSet DialogueSet => dialogueSet;

    [SerializeField] DialogueTrigger trigger;

    public void Awake()
    {
        trigger.startNode = dialogueSet.dialoguesEntries.FirstOrDefault();
    }
}
