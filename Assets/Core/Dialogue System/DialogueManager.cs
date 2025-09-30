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

    public void StartDialogue(DialogueNode startNode, DialogueTrigger trigger, Action onCondition = null)
    {
        
        PlayerController.Instance.OffInput();
        _currentTrigger = trigger;
        _uiDialogue.gameObject.SetActive(true);
        _uiDialogue.StartDialogue(startNode, PlayerController.Instance, onCondition);
    }

    public void EndDialogue()
    {
        PlayerController.Instance.OnInput();

        _uiDialogue.gameObject.SetActive(false);

        if (_currentTrigger != null)
        {
            _currentTrigger.OnEndDialogue?.Invoke();
            _currentTrigger=null;
        }
    }
}
