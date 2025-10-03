using BulletHellTemplate;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public InventoryItem requiredItem; // Tên item cần ghép
    public Image itemImage;
    public bool isFilled = false;

    [Header("Visual Feedback")]
    public Color normalColor = new Color(1, 1, 1, 0.3f);
    public Color highlightColor = new Color(0, 1, 0, 0.5f);
    public Color wrongColor = new Color(1, 0, 0, 0.5f);

    void Start()
    {
        itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.color = normalColor;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isFilled) return;

        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject == null) return;

        // Kiểm tra nếu kéo từ hotbar
        DraggableHotbarItem hotbarItem = droppedObject.GetComponent<DraggableHotbarItem>();
        if (hotbarItem != null)
        {
            // Kiểm tra đúng item không
            if (requiredItem == hotbarItem.currentItem)
            {
                // ĐÚNG - Ghép thành công!
                itemImage.sprite = hotbarItem.GetItemSprite();
                itemImage.color = Color.white;
                isFilled = true;

                // Hiệu ứng thành công
                StartCoroutine(FlashColor(highlightColor));

                // Thông báo puzzle manager
                FindFirstObjectByType<PuzzleManager>().CheckComplete();
            }
            else
            {
                // SAI - Hiệu ứng sai
                StartCoroutine(FlashColor(wrongColor));
            }
        }
    }

    System.Collections.IEnumerator FlashColor(Color color)
    {
        itemImage.color = color;
        yield return new WaitForSecondsRealtime(0.3f);
        itemImage.color = isFilled ? Color.white : normalColor;
    }
}