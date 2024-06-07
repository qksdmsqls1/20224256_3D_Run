using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText; // 최고 점수를 표시할 TextMeshProUGUI
    public Transform playerTransform; // 플레이어의 Transform을 참조
    private Vector3 startPosition;
    private int score = 0;
    private int bestScore = 0; // 최고 점수를 저장할 변수
    public float scoreMultiplier = 1f; // 점수 증가율을 조절하는 변수

    void Start()
    {
        // 게임 시작 시 플레이어의 시작 위치를 저장
        startPosition = playerTransform.position;

        // 최고 점수를 로드 (PlayerPrefs를 사용하여 영구 저장 가능)
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    void Update()
    {
        // 플레이어의 이동 거리를 계산하여 점수로 변환
        float distance = Vector3.Distance(startPosition, playerTransform.position);
        score = (int)(distance * scoreMultiplier);
        scoreText.text = "Score: " + score.ToString();

        // 최고 점수를 갱신
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "Best Score: " + bestScore.ToString();

            // 최고 점수를 저장 (PlayerPrefs를 사용하여 영구 저장)
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }
}
