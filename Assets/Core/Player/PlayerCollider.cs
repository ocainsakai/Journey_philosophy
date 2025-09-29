using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public List<Collider2D> colliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);
    }
}
