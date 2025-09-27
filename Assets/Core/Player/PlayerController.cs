using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerInventory _inventory;
    [SerializeField] PlayerInteraction _interactionManager;
    [SerializeField] UIInventory _uiInventory;
    public PlayerMovement Move => _movement;
    public PlayerInventory Inventory => _inventory;
    public PlayerInteraction Interaction => _interactionManager;

    private bool _inputEnable = true;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Inventory.OnInventoryAdd += () => _uiInventory.UpdateItem(Inventory.Items);
        Inventory.OnInventoryRemove += () => _uiInventory.UpdateItem(Inventory.Items);
    }

    public void OffInput()
    {
        _movement.Stop();
        _inputEnable = false;
    }
    public void OnInput()
    {
        _inputEnable = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_inputEnable) return;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _movement.MoveRight();
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            _movement.MoveLeft();
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _movement.Stop();
        }
    }

    public void SetInteract(IInteractable target)
    {
        Interaction.SetAction(target.GetInteractionPrompt(), () => target.Interact());
    }
}
