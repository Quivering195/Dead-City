using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    public GameObject objectToSpawn; // Object cần spawn
    public float spawnInterval = 2f; // Thời gian giữa mỗi lần spawn

    private bool hasEnteredTrigger = false; // Biến kiểm tra đã đi qua trigger hay chưa
    private float lastSpawnTime; // Thời điểm spawn lần cuối

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem có phải là collider cần xử lý không và chưa đi qua trigger
        if (other.CompareTag("Player") && !hasEnteredTrigger)
        {
            hasEnteredTrigger = true; // Đã đi qua trigger

            // Bắt đầu spawn object tự động sau mỗi spawnInterval giây
            InvokeRepeating("SpawnObject", 0f, spawnInterval);
        }
    }

    private void SpawnObject()
    {
        // Tạo một instance mới của objectToSpawn tại vị trí của SpawnOnTrigger
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        // Lưu thời điểm spawn lần cuối
        lastSpawnTime = Time.time;
    }
}
