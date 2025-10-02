using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    private Collider2D target;
    public Button button;
    [SerializeField] private float interactRadius = 1.5f;
    [SerializeField] private LayerMask interactLayer;

    private void FixedUpdate()
    {
        target = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);
        if (button != null && CanInteract())
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
    public IInteractable GetInteractable()
    {
        if (CanInteract())
        {
            return target.GetComponent<IInteractable>();
        }
        return null;
    }
    public bool CanInteract()
    {
        if (target == null) return false;
        return target.GetComponent<IInteractable>() != null;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
