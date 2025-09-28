using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private UIDialogueBox dialogueText;
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private Button optionButtonPrefab;

    private DialogueNode currentNode;
    private PlayerController playerController;

    private void Update()
    {
        // Enter skip chữ đang chạy hoặc sang câu kế
        if (Input.GetKeyDown(KeyCode.F))
        {
            dialogueText.EndDialogue();
        }
    }

    public void StartDialogue(DialogueNode startNode, PlayerController player)
    {
        currentNode = startNode;
        playerController = player;
        StartCoroutine(ShowNode(currentNode));
    }

    IEnumerator ShowNode(DialogueNode node)
    {
        currentNode = node;
        speakerNameText.text = node.speakerName;

        dialogueText.NewLine(node.dialogueText);

        // Đăng ký callback: khi text chạy xong thì gọi
        dialogueText.OnLineComplete = () =>
        {
            Debug.Log("aaaa");
            if (node.options != null && node.options.Length > 0)
            {
                ShowOptions(node);
            }
            else
            {
                // Nếu không có option thì kết thúc luôn node
                CreateEndButton();
            }
        };

        yield return dialogueText.StartDialogue();
    }

    private void ShowOptions(DialogueNode node)
    {
        // Xoá các lựa chọn cũ
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        optionsContainer.gameObject.SetActive(true);

        foreach (var option in node.options)
        {
            CreateButton(option);
        }
    }

    private void CreateButton(DialogueOption option)
    {
        var btn = Instantiate(optionButtonPrefab, optionsContainer);

        btn.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;

        btn.onClick.AddListener(() =>
        {
            if (option.rewardItem != null)
            {
                playerController.Inventory.Add(option.rewardItem);
            }
            if (option.nextNode != null)
            {
                optionsContainer.gameObject.SetActive(false);
                StartCoroutine(ShowNode(option.nextNode));
            }
            else
            {
                EndDialogue();
            }
        });
    }

    private void CreateEndButton()
    {
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        optionsContainer.gameObject.SetActive(true);

        var btn = Instantiate(optionButtonPrefab, optionsContainer);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = "End Dialogue";

        btn.onClick.AddListener(() =>
        {
            EndDialogue();
        });
    }

    void EndDialogue()
    {
        Debug.Log("Kết thúc hội thoại.");
        DialogueManager.Instance.EndDialogue();
        optionsContainer.gameObject.SetActive(false);
        // Ẩn UI, unlock điều khiển cho player ở đây nếu cần
    }
}
