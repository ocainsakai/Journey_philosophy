using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueNode startNode;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(startNode);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
