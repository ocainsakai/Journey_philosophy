using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Action dialogueCondition;   // ✅ giữ lại callback

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dialogueText.EndDialogue();
        }
    }

    public void StartDialogue(DialogueNode startNode, PlayerController player, Action onCondition = null)
    {
        dialogueCondition = onCondition;   // ✅ lưu lại khi bắt đầu
        Debug.Log($" StartDialogue {dialogueCondition}");

        currentNode = startNode;
        playerController = player;
        StartCoroutine(ShowNode(currentNode));
    }

    IEnumerator ShowNode(DialogueNode node)
    {
        Debug.Log($" ShowNode {dialogueCondition}");

        currentNode = node;
        speakerNameText.text = node.speakerName;

        dialogueText.NewLine(node.dialogueText);

        // Đăng ký callback: khi text chạy xong thì gọi
        dialogueText.OnLineComplete = () =>
        {
            HandleNodeCompletion(node);
        };

        yield return dialogueText.StartDialogue();
    }

    private void HandleNodeCompletion(DialogueNode node)
    {
        Debug.Log($" HandleNodeCompletion {dialogueCondition}");

        List<DialogueOption> filtedNodes = node.options.Where(IsOptionAvailable).ToList();
        if (filtedNodes != null && filtedNodes.Count > 0)
        {
            ShowOptions(filtedNodes);
        }
        else
        {
            CreateEndButton();
        }
    }

    private void ShowOptions(List<DialogueOption> options)
    {
        Debug.Log($" ShowOptions {dialogueCondition}");

        // Xoá các lựa chọn cũ
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        optionsContainer.gameObject.SetActive(true);

        foreach (var option in options)
        {
            CreateButton(option);
        }
    }

    private bool IsOptionAvailable(DialogueOption option)
    {
        return option.requiredItem == null || playerController.Inventory.HasItem(option.requiredItem);
    }

    private void CreateButton(DialogueOption option)
    {
        Debug.Log($" CreateButton {dialogueCondition}");
        var btn = Instantiate(optionButtonPrefab, optionsContainer);

        btn.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;

        btn.onClick.AddListener(() =>
        {
            if (option.HasCondition)
            {
                dialogueCondition?.Invoke();   // ✅ gọi từ field
            }
            if (option.rewardItem != null)
            {
                playerController.Inventory.Add(option.rewardItem);
            }
            if (option.requiredItem != null)
            {
                playerController.Inventory.Remove(option.requiredItem);
            }

            if (option.EndTrigger)
            {
                EndDialogue();
                return;
            }
            if (option.nextNode != null)
            {
                optionsContainer.gameObject.SetActive(false);
                StartCoroutine(ShowNode(option.nextNode));   // ✅ không cần truyền lại onCondition
            }
        });
    }

    private void CreateEndButton()
    {
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        optionsContainer.gameObject.SetActive(true);

        var btn = Instantiate(optionButtonPrefab, optionsContainer);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = "Kết thúc";

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
