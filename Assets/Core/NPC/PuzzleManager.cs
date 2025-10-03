using BulletHellTemplate;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Setup")]
    public ItemSlot[] itemSlots;
    public GameObject puzzlePanel;
    public GameObject resultImage;
    public InventoryItem resultItem;
    [Header("UI")]
    public Button closeButton;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI hintText; // THÊM: Gợi ý cần item gì

    void Start()
    {
        resultImage.SetActive(false);

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePuzzle);
            closeButton.gameObject.SetActive(false); // Ẩn ban đầu
        }

        instructionText.text = "Kéo item từ Hotbar vào các ô phù hợp!";

        // Hiện gợi ý
        UpdateHint();
    }

    void UpdateHint()
    {
        if (hintText == null) return;

        string hint = "Cần: ";
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.isFilled)
            {
                hint += slot.requiredItem.title + " ";
            }
        }
        hintText.text = hint;
    }

    public void CheckComplete()
    {
        Debug.Log("check");
        UpdateHint();

        bool allFilled = true;
        foreach (ItemSlot slot in itemSlots)
        {
            if (!slot.isFilled)
            {
                allFilled = false;
                break;
            }
        }
        Debug.Log("filled");

        if (allFilled)
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        // Ẩn các ô ghép
        foreach (ItemSlot slot in itemSlots)
        {
            slot.gameObject.SetActive(false);
            //PlayerController.Instance.Inventory.Remove(slot.requiredItem);
        }
        PlayerController.Instance.Inventory.Add(resultItem);

        resultImage.SetActive(true);
        resultImage.GetComponent<Image>().sprite = resultItem.itemIcon;

       

        instructionText.text = "Thành công! Con người đã cải tạo tự nhiên!";
        hintText.text = "";

        // Hiện nút close hoặc tự đóng
        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            Invoke("ClosePuzzle", 2f);
        }
    }

    void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        //Time.timeScale = 1;
        PlayerController.Instance.UnStop();
        // Thông báo game
        GameManager manager = GameManager.instance;
        if (manager != null)
        {
            manager.OnPuzzleComplete();
        }
    }
}