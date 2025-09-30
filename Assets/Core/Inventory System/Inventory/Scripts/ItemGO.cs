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
        _pickupTrigger.enabled = false;
        image.sprite = _item.itemIcon;
    }
    private void OnEnable()
    {
        StartCoroutine(AwakeAnim());
    }

    IEnumerator AwakeAnim()
    {
        yield return transform.DOJump(transform.position + Vector3.right, 1f, 2 ,1f);
        _pickupTrigger.enabled=true;
    }
}
