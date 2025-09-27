using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] Button button;
    public DialogueNode startNode;
    private PlayerController playerController;
    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Interact());
        button.gameObject.SetActive(false);
    }
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
        DialogueManager.Instance.StartDialogue(startNode);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.transform.parent.GetComponent<PlayerController>();
        if (player != null)
        {
            button.gameObject.SetActive(true);
            player.SetInteract(this);
        }
    }
}
