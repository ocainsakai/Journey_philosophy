using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTrigger?.Invoke();
    }
}
