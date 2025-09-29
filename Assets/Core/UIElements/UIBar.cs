using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Image[] images;
    
    public PlayerInventory playerController;

    private void OnEnable()
    {
        playerController.OnInventoryAdd += () => UpdateUI();
        playerController.OnInventoryRemove += () => UpdateUI();
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
        int lenght = Mathf.Min( images.Length, this.images.Length);
        for (int i = 0; i < lenght; i++)
        {
            this.images[i].sprite = images[i];
        }
    }
    public void ClearImage()
    {
        for (int i = 0;i < this.images.Length;i++)
        {
            images[i].sprite = null;
        }
    }
}
