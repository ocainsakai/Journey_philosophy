using UnityEngine;
using UnityEngine.Rendering;

public class Bridge : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject prefab; // Đổi từ Transform sang GameObject
    public float prefabWidth = 1f;
    public float speed = 1f;
    private GameObject[] bridgePieces; // Lưu các mảnh cầu để có thể gãy sau

    void Start()
    {
        BuildBridge();
    }

    void BuildBridge()
    {
        // Tính khoảng cách giữa start và end
        float distance = Vector3.Distance(startPoint.position, endPoint.position);

        // Tính số lượng prefab cần thiết
        int pieceCount = Mathf.CeilToInt(distance / prefabWidth);

        // Khởi tạo mảng lưu các mảnh cầu
        bridgePieces = new GameObject[pieceCount];

        // Tính vector hướng từ start đến end
        Vector3 direction = (endPoint.position - startPoint.position).normalized;

        // Tạo từng mảnh cầu
        for (int i = 0; i < pieceCount; i++)
        {
            // Tính vị trí cho mảnh cầu thứ i
            Vector3 position = startPoint.position + direction * (i * prefabWidth);

            // Tạo prefab tại vị trí đó
            GameObject piece = Instantiate(prefab, position, Quaternion.identity);

            // Xoay prefab theo hướng của cầu (nếu cần)
            piece.transform.right = direction; // Hoặc forward tùy prefab

            // Đặt làm con của Bridge
            piece.transform.parent = this.transform;
            piece.gameObject.SetActive(true);
            // Lưu vào mảng
            bridgePieces[i] = piece;
        }
    }

    // Hàm làm cầu gãy từ từ (gọi từ script khác)
    public void StartBreaking()
    {
        StartCoroutine(BreakBridgeCoroutine());
    }

    private System.Collections.IEnumerator BreakBridgeCoroutine()
    {
        // Gãy từ startPoint về endPoint
        for (int i = 0; i < bridgePieces.Length; i++)
        {
            if (bridgePieces[i] != null)
            {
                // Thêm Rigidbody để rơi xuống
                Destroy(bridgePieces[i]);

                yield return new WaitForSeconds(speed);
            }
        }
    }

    // Hàm tái tạo cầu (cho mini game)
    public void RebuildBridge()
    {
        // Xóa cầu cũ (nếu có)
        foreach (var child in bridgePieces)
        {
            Destroy(child);
        }
        GetComponent<Collider2D>().enabled = true;
        // Xây lại
        BuildBridge();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartBreaking();
        GetComponent<Collider2D>().enabled = false;
    }
}