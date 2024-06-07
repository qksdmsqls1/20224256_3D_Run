using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    private float timer = 0f;

    void Update()
    {
        // SpawnObstacle() 메서드 호출을 막아 장애물이 생성되지 않도록 합니다.
        // timer += Time.deltaTime;
        // if (timer >= spawnInterval)
        // {
        //     SpawnObstacle();
        //     timer = 0f;
        // }
    }

    // void SpawnObstacle()
    // {
    //     float spawnPositionX = Random.Range(-4f, 4f);
    //     Vector3 spawnPosition = new Vector3(spawnPositionX, 0.5f, transform.position.z);
    //     Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    // }
}
