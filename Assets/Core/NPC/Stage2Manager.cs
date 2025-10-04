using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Stage2Manager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject fadePanel;
    public TextMeshProUGUI doubtText;
    public GameObject miniGamePanel;

    [Header("Player")]
    public GameObject player;
    public Bridge bridge;
    [SerializeField] QuestionManager questionManager;
    [Header("Question Data")]
    [TextArea(3, 5)]
    public string question = "Con người có thể nhận thức\nthế giới khách quan không?";
    public string optionA = "A. Không thể biết được";
    public string optionB = "B. Có, qua thực tiễn & khoa học";
    public string correctAnswer = "B";

    [Header("Feedback")]
    public string correctMessage = "✓ Chính xác! Cầu được tái tạo!";
    public string wrongMessage = "❌ Sai rồi! Hãy nhớ bài học ở Trụ Tri thức!";
    private static Stage2Manager instance;

    void Awake()
    {
        instance = this;
    }

    public static void PlayerFellInWater()
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.HandleWaterFall());
        }
    }

    IEnumerator HandleWaterFall()
    {
        // 1. Tắt điều khiển
        player.GetComponent<PlayerMovement>().enabled = false;

        // 2. Âm thanh rơi nước (nếu có)
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // 3. Fade màn hình
        yield return StartCoroutine(FadeOut());

        // 4. Hiện text hoài nghi
        doubtText.gameObject.SetActive(true);
        doubtText.text = "Con người nhỏ bé, làm sao biết được thế giới?";

        yield return new WaitForSeconds(2.5f);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        // 5. Ẩn text
        doubtText.gameObject.SetActive(false);

        // 6. Mở mini game
        questionManager.ShowQuestion(
            question,
            optionA,
            optionB,
            correctAnswer,
            OnCorrectAnswer,  // Callback khi đúng
            OnWrongAnswer     // Callback khi sai
        );
    }
    void OnCorrectAnswer()
    {
        Debug.Log(correctMessage);

        // Tái tạo cầu
        bridge.RebuildBridge();

        // Respawn player về vị trí đầu cầu
        Transform player = PlayerController.Instance.transform;
        player.position = bridge.startPoint.position + Vector3.left + Vector3.up;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }

    void OnWrongAnswer()
    {
        Debug.Log(wrongMessage);

        // Player đã bị trừ mạng trong QuestionManager
        // Có thể thêm hiệu ứng ở đây nếu muốn

        // Respawn về checkpoint (hoặc startPoint)
        Transform player = PlayerController.Instance.transform;
        player.position = bridge.startPoint.position + Vector3.left + Vector3.up;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }
    IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        float duration = 1f;
        float elapsed = 0f;
        fadePanel.SetActive(true);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }
    }

    // Gọi sau khi trả lời đúng mini game
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnSequence());
    }

    IEnumerator RespawnSequence()
    {
        // 1. Đóng mini game
        miniGamePanel.SetActive(false);

        // 2. Tái tạo cầu
        FindFirstObjectByType<Bridge>().RebuildBridge();

        // 3. Fade in
        yield return StartCoroutine(FadeIn());

        // 4. Đặt lại vị trí player
        player.transform.position = FindFirstObjectByType<Bridge>().startPoint.position;

        // 5. Bật lại điều khiển
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator FadeIn()
    {
        fadePanel.SetActive(true);
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            yield return null;
        }
    }
}