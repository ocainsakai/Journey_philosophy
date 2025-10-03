using BulletHellTemplate;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableHotbarItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public int slotIndex;
    [HideInInspector] public UIBar uiBar;
    [HideInInspector] public InventoryItem currentItem;
    [HideInInspector] public bool isEmpty = true;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;
    private Image image;

    // Ghost image khi kéo
    private GameObject ghostImage;

    void Awake()
    {
        Debug.Log(gameObject.name);
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();

        // Thêm CanvasGroup nếu chưa có
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        // Không cho kéo nếu slot trống
        if (isEmpty || image.sprite == null)
        {
            eventData.pointerDrag = null;
            return;
        }

        // Tạo ghost image
        CreateGhostImage();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghostImage != null)
        {
            // FIX: Chuyển đổi screen position sang canvas position
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out position
            );
            ghostImage.transform.localPosition = position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Xóa ghost image
        if (ghostImage != null)
        {
            Destroy(ghostImage);
        }

        // Reset vị trí (vì item vẫn ở hotbar)
        rectTransform.anchoredPosition = originalPosition;
    }

    void CreateGhostImage()
    {
        ghostImage = new GameObject("GhostImage");
        ghostImage.transform.SetParent(canvas.transform);
        ghostImage.transform.SetAsLastSibling();

        Image ghostImg = ghostImage.AddComponent<Image>();
        ghostImg.sprite = image.sprite;
        ghostImg.raycastTarget = false;

        RectTransform ghostRect = ghostImage.GetComponent<RectTransform>();
        ghostRect.sizeDelta = rectTransform.sizeDelta;

        CanvasGroup ghostGroup = ghostImage.AddComponent<CanvasGroup>();
        ghostGroup.alpha = 0.6f;
        ghostGroup.blocksRaycasts = false;
    }

    // Method để lấy thông tin item (dùng cho puzzle)
    public string GetItemName()
    {
        return currentItem != null ? currentItem.title : "";
    }

    public Sprite GetItemSprite()
    {
        return currentItem.itemIcon;
    }
}
