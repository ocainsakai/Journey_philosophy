using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] Transform target;
    public float xOffset = 0f;
    public float yOffset = 0f;

    private void Update()
    {
        transform.position = target.position + new Vector3(xOffset, yOffset);
    }
}
