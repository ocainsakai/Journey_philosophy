using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives = 3;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject gameOpenPanel;
    [SerializeField] private CanvasGroup canvasGroup; 

    public static GameManager instance;

    public UnityEvent onComplete;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
    public void ShowOpen()
    {
        gameOpenPanel.SetActive(true);
        canvasGroup.alpha = 0.5f;
        canvasGroup.DOFade(1f, 0.5f) // fade in 0.5s
            .OnComplete(() =>
            {
                // sau khi hiển thị một lúc thì tắt đi
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    canvasGroup.DOFade(0f, 0.5f)
                        .OnComplete(() => gameOpenPanel.SetActive(false));
                });
            });
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void Start()
    {
        ShowOpen();
        UpdateUI();

    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            PlayerController.Instance.Inventory.Items.Clear();
            PlayerController.Instance.Inventory.SaveData();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; // Dừng game
        }
    }

    void UpdateUI()
    {
        livesText.text = "Mạng: " + lives;

        // reset scale trước khi tween
        livesText.transform.localScale = Vector3.one;

        // hiệu ứng scale bật lên rồi về lại
        livesText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 5, 0.8f);

        // (tùy chọn) đổi màu đỏ rồi về lại trắng
        livesText.DOColor(Color.red, 0.15f).OnComplete(() =>
        {
            livesText.DOColor(Color.white, 0.15f);
        });
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnPuzzleComplete()
    {
        Debug.Log("Đã vượt qua chướng ngại!");
        // Không mất mạng, tiếp tục chơi
        onComplete?.Invoke();
    }

}
