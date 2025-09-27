using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement _movement;
    
    private bool _inputEnable = true;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
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
}
