using BulletHellTemplate;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> _items;
    [SerializeField] 
    private InventorySaved saved;
    public List<InventoryItem> Items => _items;
    public Action OnInventoryAdd;
    public Action OnInventoryRemove;

    private void Awake()
    {
        LoadData();
    }
    //public UnityEvent<List<InventoryItem>> OnAddEvent;
    public void Add(InventoryItem item, int quantity =1)
    {
        for (int i = 0; i < quantity; i++)
        {
            _items.Add(item);
        }
        UIManager.Instance.ItemAddEffect(item);
        OnInventoryAdd?.Invoke();
    }

    public void Remove(InventoryItem item)
    {
        _items.Remove(item);
        OnInventoryRemove?.Invoke();
    }

    public bool HasItem(InventoryItem requiredItem)
    {
        return _items.Contains(requiredItem);
    }

    public void SaveData()
    {
        saved.items = _items;
    }
    public void LoadData()
    {
        _items = new List<InventoryItem>();
        _items = saved.items; 
    }
}