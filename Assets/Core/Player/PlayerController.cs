using System;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerInventory _inventory;
    [SerializeField] PlayerCollider _collider;
    //[SerializeField] UIInventory _uiInventory;
    public PlayerMovement Move => _movement;
    public PlayerInventory Inventory => _inventory;

    private bool _inputEnable = true;
    public float moveSpeed { 
        get => _movement.speed;
        set
        {
            _movement.speed = value;
        }
    } 


    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        UnStop();
        _movement.enabled = true ;
        //Inventory.OnInventoryAdd += () => _uiInventory.UpdateItem(Inventory.Items);
        //Inventory.OnInventoryRemove += () => _uiInventory.UpdateItem(Inventory.Items);
    }

    public void OffInput()
    {
        _inputEnable = false;
    }
    public void OnInput()
    {
        _inputEnable = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            // Gọi Manager xử lý
            Stage2Manager.PlayerFellInWater();
        }
    }

    public void Stop()
    {
        _movement.Stop();
       _movement.enabled = false;
    }

    public void UnStop()
    {
       _movement.enabled = true;

    }
}
