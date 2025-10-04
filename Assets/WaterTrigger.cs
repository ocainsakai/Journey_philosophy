using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] Bridge bridge;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player")){
            GameManager.Instance.LoseLife();
            collision.transform.position = new Vector3( bridge.startPoint.position.x - 2, bridge.startPoint.position.y + 2);
            PlayerController.Instance.Stop();
            bridge.RebuildBridge();
            PlayerController.Instance.UnStop();
        } 
    }
}
