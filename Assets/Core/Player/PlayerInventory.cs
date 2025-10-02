using BulletHellTemplate;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> _items;

    public List<InventoryItem> Items => _items;
    public Action OnInventoryAdd;
    public Action OnInventoryRemove;

    public UnityEvent<List<InventoryItem>> OnAddEvent;
    public void Add(InventoryItem item, int quantity =1)
    {
        for (int i = 0; i < quantity; i++)
        {
            Debug.Log(item.name);
            _items.Add(item);
        }
        OnInventoryAdd?.Invoke();
        OnAddEvent?.Invoke(_items);
    }

    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
        OnInventoryRemove?.Invoke();
    }

    public bool Contain(InventoryItem requiredItem)
    {
        return _items.Contains(requiredItem);
    }
}