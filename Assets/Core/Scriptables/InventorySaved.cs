using BulletHellTemplate;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySaved", menuName = "Scriptable Objects/InventorySaved")]
public class InventorySaved : ScriptableObject
{
    public List<InventoryItem> items;
}
