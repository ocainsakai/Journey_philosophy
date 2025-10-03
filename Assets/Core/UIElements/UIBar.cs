using BulletHellTemplate;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UIBar : MonoBehaviour
{
    public Image[] images;
    public PlayerInventory playerController;

    // THÊM: Array chứa DraggableHotbarItem cho mỗi slot
    private DraggableHotbarItem[] draggableItems;

    private void Awake()
    {
        // Tự động lấy hoặc thêm DraggableHotbarItem cho mỗi image
        draggableItems = new DraggableHotbarItem[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            draggableItems[i] = images[i].gameObject.GetComponent<DraggableHotbarItem>();
            if (draggableItems[i] == null)
            {
                draggableItems[i] = images[i].gameObject.AddComponent<DraggableHotbarItem>();
            }
            draggableItems[i].slotIndex = i;
            draggableItems[i].uiBar = this;
        }
    }

    private void OnEnable()
    {
        playerController.OnInventoryAdd += () => UpdateUI();
        playerController.OnInventoryRemove += () => UpdateUI();
        UpdateUI();
    }
    private void Start()
    {
        UpdateUI();
    }

    private void OnDisable()
    {
        playerController.OnInventoryAdd -= () => UpdateUI();
        playerController.OnInventoryRemove -= () => UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateUI(playerController.Items.Select(x => x.itemIcon).ToArray());
    }

    public void UpdateUI(Sprite[] images)
    {
        ClearImage();
        int length = Mathf.Min(images.Length, this.images.Length);

        for (int i = 0; i < length; i++)
        {
            this.images[i].sprite = images[i];

            // THÊM: Cập nhật thông tin item cho draggable
            if (draggableItems[i] != null && i < playerController.Items.Count)
            {
                draggableItems[i].currentItem = playerController.Items[i];
                draggableItems[i].isEmpty = false;
            }
        }

        // Đánh dấu các slot trống
        for (int i = length; i < this.images.Length; i++)
        {
            if (draggableItems[i] != null)
            {
                draggableItems[i].isEmpty = true;
            }
        }
    }

    public void ClearImage()
    {
        for (int i = 0; i < this.images.Length; i++)
        {
            images[i].sprite = null;
        }
    }

    // THÊM: Method để lấy item từ slot
    public InventoryItem GetItemAtSlot(int index)
    {
        if (index >= 0 && index < playerController.Items.Count)
        {
            return playerController.Items[index];
        }
        return null;
    }
}