using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        // Nếu quên gán trong Inspector thì sẽ tự động lấy
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    public void MoveLeft()
    {
        if (_rb == null) return;
        _rb.linearVelocity = new Vector2(-moveSpeed, _rb.linearVelocity.y);
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

    }

    public void MoveRight()
    {
        if (_rb == null) return;
        _rb.linearVelocity = new Vector2(moveSpeed, _rb.linearVelocity.y);
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

    }

    public void Stop()
    {
        if (_rb == null) return;
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
    }
}
