using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI clearText; // "클리어" 텍스트를 담을 변수
    public TextMeshProUGUI restartText; // "R 버튼을 누르세요" 텍스트를 담을 변수
    public Transform lastObstacle; // 마지막 장애물의 위치
    private bool isGameClear = false;

    void Start()
    {
        clearText.gameObject.SetActive(false); // 처음에는 클리어 텍스트를 비활성화
        restartText.gameObject.SetActive(false); // 처음에는 재시작 텍스트를 비활성화
    }

    void Update()
    {
        // 게임이 클리어된 상태에서 "R" 키를 누르면 씬을 다시 로드
        if (isGameClear && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
        clearText.gameObject.SetActive(true); // 클리어 텍스트를 활성화
        restartText.gameObject.SetActive(true); // 재시작 텍스트를 활성화
        Time.timeScale = 0; // 게임 종료
    }

    void RestartGame()
    {
        Time.timeScale = 1; // 게임 시간 다시 활성화
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }
}
