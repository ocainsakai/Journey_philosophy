using BulletHellTemplate;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> _items;

    public List<InventoryItem> Items => _items;
    public Action OnInventoryAdd;
    public Action OnInventoryRemove;

    public void Add(InventoryItem item, int quantity =1)
    {
        for (int i = 0; i < quantity; i++)
        {
            Debug.Log(item.name);
            _items.Add(item);
        }
        OnInventoryAdd?.Invoke();
    }

    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
        OnInventoryRemove?.Invoke();
    }
}