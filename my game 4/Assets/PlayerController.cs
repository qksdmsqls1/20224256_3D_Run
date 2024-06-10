using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; // 점프 힘 설정
    public float extraGravity = 1f; // 추가 중력 설정
    private Rigidbody rb;
    private bool isGrounded = true;
    private GameManager gameManager; // GameManager 참조

    public float sideSpeed = 5f; // 좌우 이동 속도

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 초기화
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager 찾기
    }

    void FixedUpdate()
    {
        // 전진 이동
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // 추가 중력 적용
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }

        // 마지막 장애물 위치를 기준으로 클리어 상태 확인
        gameManager.CheckGameClear(transform.position);
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }

        // 좌우 이동
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 sideMove = transform.right * moveHorizontal * sideSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + sideMove);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 착지 시 수직 속도 초기화
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 장애물과 충돌 시 게임오버 처리
            gameManager.PlayerDied();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // 스테이지 이동 시 플레이어 초기화 메서드
    public void ResetPlayer()
    {
        rb.velocity = Vector3.zero;
        isGrounded = true; // 초기화 시 isGrounded를 true로 설정
    }

    // 플레이어의 속도를 설정하는 메서드
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
