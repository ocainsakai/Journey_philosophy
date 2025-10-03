using BulletHellTemplate;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ItemGO : MonoBehaviour
{
    [SerializeField] InventoryItem _item;
    [SerializeField] ItemPickupTrigger _pickupTrigger;
    [SerializeField] SpriteRenderer image;
    private void Awake()
    {
        _pickupTrigger.Item = _item;
        image.sprite = _item.itemIcon;
    }
}
