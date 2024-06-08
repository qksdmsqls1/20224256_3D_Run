using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI nextStageText; // "다음 스테이지" 텍스트
    public Transform lastObstacle;
    private bool isGameClear = false;
    private int currentStage = 1; // 현재 스테이지 번호

    void Start()
    {
        clearText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextStageText.gameObject.SetActive(false); // 다음 스테이지 텍스트를 비활성화
    }

    void Update()
    {
        if (isGameClear)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                NextStage();
            }
        }
    }

    public void CheckGameClear(Vector3 playerPosition)
    {
        if (!isGameClear && playerPosition.z > lastObstacle.position.z)
        {
            isGameClear = true;
            ClearGame();
        }
    }

    void ClearGame()
    {
        clearText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        nextStageText.gameObject.SetActive(true); // 다음 스테이지 텍스트를 활성화
        Time.timeScale = 0; // 게임 종료
    }

    void RestartGame()
    {
        Time.timeScale = 1; // 게임 시간 다시 활성화
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

    void NextStage()
    {
        Time.timeScale = 1; // 게임 시간 다시 활성화
        currentStage++;
        MovePlayerToNextStage();
        isGameClear = false;
        clearText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextStageText.gameObject.SetActive(false);
    }

    void MovePlayerToNextStage()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (currentStage == 2)
        {
            // 스테이지 2의 시작 위치를 설정합니다.
            player.transform.position = new Vector3(0, 0, 100); // 예시로 z값을 100으로 설정
            lastObstacle = GameObject.Find("Stage2LastObstacle").transform;
        }
        // 필요한 경우, 추가 스테이지에 대한 설정을 여기 추가합니다.
    }
}
