using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public Transform playerTransform;
    private Vector3 startPosition;
    private int score = 0;
    private int bestScore = 0;
    public float scoreMultiplier = 1f;
    private bool isGameClear = false;

    void Start()
    {
        startPosition = playerTransform.position;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    void Update()
    {
        if (!isGameClear)
        {
            float distance = Vector3.Distance(startPosition, playerTransform.position);
            score = (int)(distance * scoreMultiplier);
            scoreText.text = "Score: " + score.ToString();

            if (score > bestScore)
            {
                bestScore = score;
                bestScoreText.text = "Best Score: " + bestScore.ToString();
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save();
            }
        }
    }

    public void SetGameClear(bool clear)
    {
        isGameClear = clear;
    }
}
