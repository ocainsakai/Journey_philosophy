using BulletHellTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] GameObject popup;
    public GameObject puzzlePanel;
    [SerializeField] InventoryItem inventoryItem1;
    [SerializeField] InventoryItem inventoryItem2;

    Collider2D _collider;
    public float moveSpeed = 3f; // Cho đá rơi
    public bool isMoving = false;
    public bool hasAppeer = false;
    private void Start()
    {
        popup.SetActive(false);
        _collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if ( !hasAppeer)
        {
            OnCondition();
            return;
        }
        if (isMoving)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
        if (transform.position.y <= -1.5f && isMoving)
        {
            isMoving = false;
            _collider.isTrigger = false;
            popup.SetActive(true);
        }
    }

    public void OnCondition()
    {
        var items = PlayerController.Instance.Inventory.Items;
        if (items.Contains(inventoryItem1) && items.Contains(inventoryItem2))
        {
            transform.position = new Vector3(PlayerController.Instance.transform.position.x + 3, 10f, 0f);
            isMoving = true;
            hasAppeer = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
        }
        if (col.CompareTag("Ground"))
        {
            isMoving = false;
            _collider.isTrigger = false;
            popup.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController.Instance.Stop();

            puzzlePanel.SetActive(true);
        }
    }

    public void OnPuzzleWin()
    {
        popup.gameObject.SetActive(false);
        _collider.enabled = false;
    }
}
