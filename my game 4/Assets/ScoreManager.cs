using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform을 참조
    private Vector3 startPosition;
    public float scoreMultiplier = 1f; // 점수 증가율을 조절하는 변수

    void Start()
    {
        // 게임 시작 시 플레이어의 시작 위치를 저장
        startPosition = playerTransform.position;
    }

    void Update()
    {
        // 시작 위치 갱신
        startPosition = playerTransform.position;
    }

    public void ResetScore()
    {
        // 필요 시 초기화 관련 기능 추가
        startPosition = playerTransform.position;
    }

    public void ResetCurrentScore()
    {
        // 필요 시 초기화 관련 기능 추가
    }

    public int GetCurrentScore()
    {
        return 0; // 더 이상 점수를 사용하지 않으므로 기본 값 반환
    }

    public void SetScore(int newScore)
    {
        // 더 이상 점수를 사용하지 않으므로 빈 메서드
    }
}
