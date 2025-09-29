using UnityEngine;

public class UIMapChoosing : MonoBehaviour
{
    [SerializeField] MapEntry mapEntry;
    public MapMetaData[] MapMetaData;

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

                entry.SetData(m.Art, m.Name, m.scene.name);

            }
        }
    }
}
