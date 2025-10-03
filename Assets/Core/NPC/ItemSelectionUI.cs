using BulletHellTemplate;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionUI : MonoBehaviour
{
    public GameObject itemButtonPrefab; // Prefab button cho mỗi item
    public Transform itemContainer; // Container chứa các buttons
    public TextMeshProUGUI instructionText;

    private System.Action<InventoryItem> onItemSelected;

    public void ShowItems(List<InventoryItem> items, System.Action<InventoryItem> callback)
    {
        onItemSelected = callback;

        // Xóa buttons cũ
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // Tạo button cho mỗi item
        foreach (var item in items)
        {
            GameObject btnObj = Instantiate(itemButtonPrefab, itemContainer);
            Button btn = btnObj.GetComponent<Button>();
            Image img = btnObj.GetComponent<Image>();

            // Set sprite
            if (img != null && item.itemIcon != null)
            {
                img.sprite = item.itemIcon;
            }

            // Set text (optional)
            Text btnText = btnObj.GetComponentInChildren<Text>();
            if (btnText != null)
            {
                btnText.text = item.title;
            }

            // Add click listener
            InventoryItem currentItem = item; // Capture variable
            btn.onClick.AddListener(() => OnItemButtonClicked(currentItem));
        }

        instructionText.text = "Chọn item để sử dụng:";
    }

    void OnItemButtonClicked(InventoryItem item)
    {
        onItemSelected?.Invoke(item);
    }
}
