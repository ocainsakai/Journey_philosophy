// Obstacle.cs - CHƯỚNG NGẠI VẬT ẨU
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 3f; // Cho đá rơi
    public bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.LoseLife();
            // Hiện popup câu hỏi (làm ngày 2)
            Destroy(gameObject); // Xóa luôn
        }
    }
}