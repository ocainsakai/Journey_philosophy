using BulletHellTemplate;
using UnityEngine;

public class ItemGO : MonoBehaviour
{
    [SerializeField] InventoryItem _item;
    [SerializeField] ItemPickupTrigger _pickupTrigger;

    private void OnEnable()
    {
         _pickupTrigger.Item = _item;
    }


}
