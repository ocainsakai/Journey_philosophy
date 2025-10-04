using UnityEngine;
// Script riêng cho dá roi
public class FallingRock : MonoBehaviour
{
    private SwampZone swampZone;
    private bool hasHitPlayer = false;

    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float lifeTime = 5f; // T? h?y sau 5s n?u không ch?m gì

    public void Initialize(SwampZone zone)
    {
        swampZone = zone;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Roi xu?ng
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHitPlayer)
        {
            Debug.LogWarning("hitted");
            hasHitPlayer = true;
            swampZone?.OnPlayerHitByRock();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            // Ðá ch?m d?t thì bi?n m?t
            Destroy(gameObject);
        }
    }
}