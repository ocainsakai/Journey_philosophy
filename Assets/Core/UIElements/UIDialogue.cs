using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private Button optionButtonPrefab;

    private DialogueNode currentNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartDialogue(DialogueNode startNode)
    {
        currentNode = startNode;
        ShowNode(currentNode);
    }
    void ShowNode(DialogueNode node)
    {
        speakerNameText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        // Xoá các lựa chọn cũ
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        optionsContainer.gameObject.SetActive(true);
        // Tạo các lựa chọn mới
        foreach (var option in node.options)
        {
            CreateButton(option);
        } 
        if (node.options == null || node.options.Length == 0)
        {
            CreateButton();
        }
    }

    private void CreateButton(DialogueOption option = null)
    {

        var btn = Instantiate(optionButtonPrefab, optionsContainer);

        if (option != null)
        {

            btn.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;
            btn.onClick.AddListener(() =>
            {
                if (option.nextNode != null)
                {
                    ShowNode(option.nextNode);
                }
            });
        }
        else
        {
            btn.GetComponentInChildren<TextMeshProUGUI>().text = "End Dialogue";
            btn.onClick.AddListener(() =>
            {
                EndDialogue();
            });
        }
    }

    void EndDialogue()
    {
        Debug.Log("Kết thúc hội thoại.");
        DialogueManager.Instance.EndDialogue();
        // Ẩn UI, unlock điều khiển cho player
    }
}
