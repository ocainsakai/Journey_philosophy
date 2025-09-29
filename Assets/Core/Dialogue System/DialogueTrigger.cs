using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Button button;
    public DialogueNode startNode;
    public Action OnEndDialogue;
    private PlayerController playerController;
    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            Interact();
        });
    }
    public string GetInteractionPrompt()
    {
        return startNode.speakerName;
    }

    public void Interact()
    {
        TriggerDialogue();
        button.gameObject.SetActive(false);
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(startNode, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.parent.GetComponent<PlayerController>();
        if (player != null)
        {

            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.transform.parent.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log($"{gameObject.name} Ontrigger");
            button.gameObject.SetActive(false);
            //button.GetComponentInChildren<TextMeshProUGUI>().text = "Enter";

        }
    }
}
