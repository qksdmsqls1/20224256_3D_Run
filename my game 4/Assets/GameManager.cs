using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI nextStageText; // "다음 스테이지" 텍스트
    public TextMeshProUGUI stageText; // 스테이지 번호 텍스트
    public Transform lastObstacle;
    private bool isGameClear = false;
    private int currentStage = 1; // 현재 스테이지 번호
    private int maxStage = 5; // 최대 스테이지 번호

    public Transform stage1Start; // 스테이지 1의 시작 위치
    public Transform stage2Start; // 스테이지 2의 시작 위치
    public Transform stage3Start; // 스테이지 3의 시작 위치
    public Transform stage4Start; // 스테이지 4의 시작 위치
    public Transform stage5Start; // 스테이지 5의 시작 위치

    private ScoreManager scoreManager;

    void Start()
    {
        clearText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextStageText.gameObject.SetActive(false);
        stageText.text = "Stage " + currentStage; // 스테이지 번호 텍스트 초기화

        scoreManager = FindObjectOfType<ScoreManager>(); // ScoreManager 찾기

        // 현재 스테이지의 시작 위치를 설정
        MovePlayerToStartPosition();
    }

    void Update()
    {
        if (isGameClear)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartStage();
            }
            if (currentStage < maxStage && Input.GetKeyDown(KeyCode.N)) // 최대 스테이지를 넘지 않도록 설정
            {
                NextStage();
            }
        }

        // 마스터 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoToStage(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GoToStage(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GoToStage(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GoToStage(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GoToStage(5);
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
        if (currentStage < maxStage)
        {
            nextStageText.gameObject.SetActive(true); // 마지막 스테이지가 아닐 때만 다음 스테이지 텍스트를 활성화
        }
        Time.timeScale = 0; // 게임 종료
    }

    public void RestartStage()
    {
        Time.timeScale = 1; // 게임 시간 다시 활성화
        scoreManager.ResetCurrentScore(); // 현재 스코어를 리셋
        stageText.text = "Stage " + currentStage; // 스테이지 번호 텍스트 업데이트
        MovePlayerToStartPosition();
        isGameClear = false;
        clearText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextStageText.gameObject.SetActive(false);
    }

    void NextStage()
    {
        Time.timeScale = 1; // 게임 시간 다시 활성화
        currentStage++;
        stageText.text = "Stage " + currentStage; // 스테이지 번호 텍스트 업데이트
        MovePlayerToStartPosition();
        isGameClear = false;
        clearText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        nextStageText.gameObject.SetActive(false);
    }

    void MovePlayerToStartPosition()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        switch (currentStage)
        {
            case 1:
                player.transform.position = stage1Start.position;
                lastObstacle = GameObject.FindWithTag("Stage1LastObstacle").transform;
                player.SetSpeed(11f); // 스테이지 1 속도 설정
                break;
            case 2:
                player.transform.position = stage2Start.position;
                lastObstacle = GameObject.FindWithTag("Stage2LastObstacle").transform;
                player.SetSpeed(17f); // 스테이지 2 속도 설정
                break;
            case 3:
                player.transform.position = stage3Start.position;
                lastObstacle = GameObject.FindWithTag("Stage3LastObstacle").transform;
                player.SetSpeed(17f); // 스테이지 3 속도 설정 (빠르게)
                break;
            case 4:
                player.transform.position = stage4Start.position;
                lastObstacle = GameObject.FindWithTag("Stage4LastObstacle").transform;
                player.SetSpeed(20f); // 스테이지 4 속도 설정 (빠르게)
                break;
            case 5:
                player.transform.position = stage5Start.position;
                lastObstacle = GameObject.FindWithTag("Stage5LastObstacle").transform;
                player.SetSpeed(30f); // 스테이지 5 속도 설정 (빠르게)
                break;
                // 필요한 경우, 추가 스테이지에 대한 설정을 여기 추가합니다.
        }
        player.ResetPlayer(); // 플레이어 초기화
    }

    void GoToStage(int stageNumber)
    {
        if (stageNumber > 0 && stageNumber <= maxStage)
        {
            Time.timeScale = 1; // 게임 시간 다시 활성화
            currentStage = stageNumber;
            stageText.text = "Stage " + currentStage; // 스테이지 번호 텍스트 업데이트
            MovePlayerToStartPosition();
            isGameClear = false;
            clearText.gameObject.SetActive(false);
            restartText.gameObject.SetActive(false);
            nextStageText.gameObject.SetActive(false);
        }
    }

    public void PlayerDied()
    {
        RestartStage();
    }
}
