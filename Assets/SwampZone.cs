using System.Collections;
using UnityEngine;

public class SwampZone : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float slowSpeedMultiplier = 0.5f; // Giảm 50% tốc độ

    [Header("Falling Rock Settings")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private float rockSpawnHeight = 10f; // Độ cao spawn đá
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;
    [SerializeField] private float rockWarningTime = 0.5f; // Thời gian cảnh báo trước khi đá rơi
    [SerializeField] private GameObject warningIndicatorPrefab; // Prefab hình tròn cảnh báo

    [Header("Player Respawn")]
    [SerializeField] private Transform spawnPoint; // Vị trí ban đầu

    private bool playerInZone = false;
    private Transform playerTransform;
    private float originalSpeed;
    private Coroutine spawnRockCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerInZone = true;
            playerTransform = collision.transform;

            // Lưu tốc độ gốc và giảm tốc
            originalSpeed = PlayerController.Instance.moveSpeed;
            PlayerController.Instance.moveSpeed *= slowSpeedMultiplier;

            // Bắt đầu spawn đá
            spawnRockCoroutine = StartCoroutine(SpawnRocksRoutine());

            Debug.Log("Player entered swamp - speed reduced!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerInZone = false;

            // Khôi phục tốc độ gốc
            PlayerController.Instance.moveSpeed = originalSpeed;

            // Dừng spawn đá
            if (spawnRockCoroutine != null)
            {
                StopCoroutine(spawnRockCoroutine);
                spawnRockCoroutine = null;
            }

            Debug.Log("Player exited swamp - speed restored!");
        }
    }

    private IEnumerator SpawnRocksRoutine()
    {
        while (playerInZone)
        {
            // Đợi thời gian ngẫu nhiên
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            if (playerInZone && playerTransform != null)
            {
                SpawnRockAtPlayer();
            }
        }
    }

    private void SpawnRockAtPlayer()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3 spawnPos = new Vector3(playerPos.x + 2f, playerPos.y + rockSpawnHeight, 0);

        // Hiển thị cảnh báo
        if (warningIndicatorPrefab != null)
        {
            GameObject warning = Instantiate(warningIndicatorPrefab, playerPos, Quaternion.identity);
            Destroy(warning, rockWarningTime);
        }

        // Spawn đá sau thời gian cảnh báo
        StartCoroutine(SpawnRockDelayed(spawnPos, rockWarningTime));
    }

    private IEnumerator SpawnRockDelayed(Vector3 spawnPos, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rockPrefab != null)
        {
            GameObject rock = Instantiate(rockPrefab, spawnPos, Quaternion.identity);
            var falling = rock.GetComponent<FallingRock>();
            // Thêm component FallingRock nếu chưa có
            if (falling == null)
            {
                falling = rock.AddComponent<FallingRock>();
            }
           falling.Initialize(this);
        }
    }

    public void OnPlayerHitByRock()
    {
        Debug.Log("das");
        // Mất máu
        GameManager.Instance.LoseLife();

        // Dịch chuyển về vị trí ban đầu
        if (playerTransform != null && spawnPoint != null)
        {
            playerTransform.position = spawnPoint.position;
            Debug.Log("Player hit by rock - respawned at start!");
        }
    }

    private void OnDestroy()
    {
        // Khôi phục tốc độ khi object bị destroy
        if (playerInZone && PlayerController.Instance != null)
        {
            PlayerController.Instance.moveSpeed = originalSpeed;
        }
    }
}
