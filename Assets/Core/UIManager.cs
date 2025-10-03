using BulletHellTemplate;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PickupPanel;
    [SerializeField] Image PickupIcon;


    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ItemAddEffect(InventoryItem item)
    {
        if (PickupPanel != null)
        {
            PickupIcon.sprite = item.itemIcon;
            PickupPanel.SetActive(true);
        }
    }
}
