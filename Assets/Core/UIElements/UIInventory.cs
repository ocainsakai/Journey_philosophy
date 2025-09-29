using BulletHellTemplate;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] Transform inventoryContainer;
    [SerializeField] InventoryEntry _slotPrefab;

    private List<InventoryEntry> _inventoryEntries;

    //private List<string> _inventoryItemIDs => _inventoryEntries.Select(x => x.GetInstanceID()).ToList();
    public void UpdateItem(IEnumerable<InventoryItem> items)
    {
        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }
        foreach(InventoryItem item in items)
        {
            CreateSlot(item);
        }
    }
    private void OnEnable()
    {
        //if (_inventoryEntries == null)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        CreateSlot();
        //    }
        //}
    }
    public InventoryEntry CreateSlot(InventoryItem item = null)
    {
        var slot = Instantiate(_slotPrefab);
        slot.transform.SetParent(inventoryContainer);
        slot.transform.localScale = Vector3.one;
        if (item != null)
        {
            slot.icon.sprite = item.itemIcon;
            slot.title.text = item.title;
        }
        return slot;
    }
}
