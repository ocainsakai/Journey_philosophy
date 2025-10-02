using BulletHellTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : NPCController
{
    [SerializeField] GameObject popup;

    [SerializeField] InventoryItem inventoryItem1;
    [SerializeField] InventoryItem inventoryItem2;

    [SerializeField] Rigidbody2D rigidbody2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (popup != null && popup.activeSelf) 
        StartCoroutine(ShowPopup());
    }
    private void Start()
    {
        popup.SetActive(false);
        rigidbody2.bodyType = RigidbodyType2D.Static;
    }
    IEnumerator ShowPopup()
    {
        popup.SetActive(true);
        yield return new WaitForSeconds(1);
        popup.SetActive(false);
    }

    public void OnCondition(List<InventoryItem> items)
    {
        if (items.Contains(inventoryItem1) && items.Contains(inventoryItem2))
        {
            rigidbody2.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
