using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] UIDialogue _uiDialogue;

    public static DialogueManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueNode startNode)
    {
        
        PlayerController.Instance.OffInput();

        _uiDialogue.gameObject.SetActive(true);
        _uiDialogue.StartDialogue(startNode);
    }

    public void EndDialogue()
    {
        PlayerController.Instance.OnInput();

        _uiDialogue.gameObject.SetActive(false);
    }
}
