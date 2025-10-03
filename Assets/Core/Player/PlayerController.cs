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
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
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

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F) && _inputEnable )
    //    {
    //        CheckInteract();
    //    }
    //}

    //public void CheckInteract()
    //{
        
    //    if (_collider.CanInteract())
    //    {
    //        _collider.GetInteractable().Interact();
    //    }
    //    else
    //    {
    //        Debug.Log("No interactable object found");
    //    }
    //}

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
