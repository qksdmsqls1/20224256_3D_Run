using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // 플레이어의 Transform
    public Vector3 offset;         // 플레이어와 카메라 간의 거리

    void Start()
    {
        // 초기 오프셋 값을 설정
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 카메라 위치를 플레이어의 위치에 맞추어 업데이트
        transform.position = player.position + offset;
    }
}
