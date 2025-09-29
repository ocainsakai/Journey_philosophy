using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] UIDialogue _uiDialogue;

    public static DialogueManager Instance;

    public static bool IsTalking = false;
    private DialogueTrigger _currentTrigger;
    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueNode startNode, DialogueTrigger trigger)
    {
        
        PlayerController.Instance.OffInput();
        _currentTrigger = trigger;
        _uiDialogue.gameObject.SetActive(true);
        _uiDialogue.StartDialogue(startNode, PlayerController.Instance);
    }

    public void EndDialogue()
    {
        PlayerController.Instance.OnInput();

        _uiDialogue.gameObject.SetActive(false);
        _currentTrigger.OnEndDialogue();
        _currentTrigger=null;
    }
}
