using BulletHellTemplate;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniBossSimple : MonoBehaviour
{
    [Header("Boss Setup")]
    public string bossName = "Ma Duy Tâm";
    public string bossDialogue = "Ý thức sinh ra vạn vật! Vật chất chỉ là ảo ảnh trong đầu ta.";
    public string correctAnswer = "B";
    public string requiredItemName = "LuoiCay";

    [Header("Reward")]
    public InventoryItem rewardItem;
    public Sprite rewardItemSprite;

    [Header("UI")]
    public GameObject bossPanel;
    public TextMeshProUGUI dialogueText;
    public Button answerA_Button;
    public Button answerB_Button;
    public TextMeshProUGUI feedbackText;

    [Header("Visual")]
    public SpriteRenderer bossSpriteRenderer;

    void Start()
    {
        bossPanel.SetActive(false);
        answerA_Button.onClick.AddListener(() => CheckAnswer("A"));
        answerB_Button.onClick.AddListener(() => CheckAnswer("B"));
        rewardItemSprite = rewardItem.itemIcon;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartBossFight();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartBossFight();
        }
    }
    void StartBossFight()
    {
        PlayerController.Instance.Stop();
        bossPanel.SetActive(true);
        dialogueText.text = bossName + ": " + bossDialogue;
        feedbackText.text = "";
    }

    void CheckAnswer(string answer)
    {
        if (answer != correctAnswer)
        {
            // SAI
            feedbackText.text = "Sai rồi!";
            feedbackText.color = Color.red;
            GameManager.instance.LoseLife();
            CloseBossFight();
            return;
        }

        // ĐÚNG - Kiểm tra có item không
        PlayerInventory inventory = PlayerController.Instance.Inventory;
        bool hasItem = false;

        if (inventory != null)
        {
            foreach (InventoryItem item in inventory.Items)
            {
                if (item.title == requiredItemName)
                {
                    hasItem = true;
                    break;
                }
            }
        }

        if (hasItem)
        {
            // THÀNH CÔNG!
            DefeatBoss();
        }
        else
        {
            // Không có item
            feedbackText.text = "Cần item " + requiredItemName + " để đánh bại boss!";
            feedbackText.color = Color.yellow;
            Invoke("CloseBossFight", 0.5f);
        }
    }

    void DefeatBoss()
    {
        feedbackText.text = "Thành công! Boss đã bị tiêu diệt!";
        feedbackText.color = Color.green;

        // Fade out boss
        StartCoroutine(FadeOut());

        // Thưởng item
        PlayerInventory inventory = PlayerController.Instance.Inventory;
        if (inventory != null)
        {
            inventory.Add(rewardItem);
        }

        Invoke("CloseBossFight", 2f);
    }

    System.Collections.IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= Time.unscaledDeltaTime * 0.5f;
            Color c = bossSpriteRenderer.color;
            c.a = alpha;
            bossSpriteRenderer.color = c;
            yield return null;
        }
        Destroy(gameObject);
    }

    void CloseBossFight()
    {
        bossPanel.SetActive(false);
        PlayerController.Instance.UnStop();
    }
}