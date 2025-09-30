using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemPickupUI : MonoBehaviour
{
    public UnityEvent OnCompleted;
    public Image itemIcon;
    public void OnUICompleted()
    {
        OnCompleted?.Invoke();
    }
}
