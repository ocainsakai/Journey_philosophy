using BulletHellTemplate;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour
{
    private InventoryItem _item;
    public InventoryItem Item
    {
        get => _item;
        set
        {
            _item = value;
        }
    }

    public void TriggerPickup(PlayerController player)
    {
        player.Inventory.Add(_item);
        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerPickup(collision.transform.parent.GetComponent<PlayerController>());
        }
    }
}
