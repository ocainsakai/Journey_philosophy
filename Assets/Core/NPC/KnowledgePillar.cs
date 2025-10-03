using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgePillar : MonoBehaviour
{
    [Header("UI References")]
    public GameObject knowledgePanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public Image philosopherImage;
    public Button continueButton;

    [Header("Knowledge Data")]
    public KnowledgeData knowledgeData; // Kéo ScriptableObject vào đây

    [Header("Visual Effects")]
    public ParticleSystem glowEffect; // Hiệu ứng sáng khi player gần
    public float typingSpeed = 0.05f;

    [SerializeField] QuestionManager questionManager;
    private bool isShowing = false;
    private bool isTyping = false;

    private bool hasBeenRead;

    void Start()
    {
        hasBeenRead = false;
        knowledgePanel.SetActive(false);
        continueButton.onClick.AddListener(CloseKnowledge);

        if (glowEffect != null)
        {
            glowEffect.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player") && isQuestion)
        //{
        //    if (!hasBeenRead)
        //    {
        //        if (glowEffect != null) glowEffect.Play();
        //        ShowKnowledge();
        //    }
        //}
        //else 
        if (collision.CompareTag("Player") && !hasBeenRead)
        {
            questionManager.ShowQuestion(
             "Vật chất hay ý thức có trước?",
             "",
             "Vật chất",
             "B",
             () =>
             {
                 ShowKnowledge();
                 hasBeenRead = true;
             }
         );
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (glowEffect != null) glowEffect.Stop();
        }
    }

    public void ShowKnowledge()
    {
        if (isShowing || knowledgeData == null) return;

        isShowing = true;
        hasBeenRead = true;
        //Time.timeScale = 0;
        PlayerController.Instance.Stop();

        knowledgePanel.SetActive(true);

        // Load data từ ScriptableObject
        titleText.text = knowledgeData.title;

        if (philosopherImage != null && knowledgeData.philosopherImage != null)
        {
            philosopherImage.sprite = knowledgeData.philosopherImage;
            philosopherImage.gameObject.SetActive(true);
        }

        StartCoroutine(TypeText(knowledgeData.content));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        contentText.text = "";

        foreach (char c in text)
        {
            contentText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isTyping = true;
    }

    public void CloseKnowledge()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            contentText.text = knowledgeData.content;
            isTyping=false;
            return;
        }

        knowledgePanel.SetActive(false);
        PlayerController.Instance.UnStop();
        //Time.timeScale = 1;
        isShowing = false;

        // Đổi màu trụ sau khi đọc (đã đọc rồi)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(0.5f, 0.5f, 0.5f); // Màu xám
        }
    }
}