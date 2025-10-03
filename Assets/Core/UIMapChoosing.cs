using UnityEngine;

public class UIMapChoosing : MonoBehaviour
{
    public MapMetaData[] MapMetaData;
    [SerializeField] MapEntry mapEntry;
    [SerializeField] Sprite locked;
    private void OnEnable()
    {
        foreach(Transform i in transform)
        {
            Destroy(i.gameObject);
        }
        foreach (MapMetaData m in MapMetaData)
        {
            if (m != null)
            {
                var entry = Instantiate(mapEntry, transform.position, Quaternion.identity);
                entry.transform.SetParent(transform, false);

                entry.SetData(m.IsUnlocked ? m.Art : locked, m.Name, m.scene.name, m.IsUnlocked);

            }
        }
    }
}
