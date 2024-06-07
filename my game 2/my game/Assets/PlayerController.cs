using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필요

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; // 점프 힘 설정
    public float extraGravity = 1f; // 추가 중력 설정
    private Rigidbody rb;
    private bool isGrounded = true;

    public float sideSpeed = 5f; // 좌우 이동 속도

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            GameOver();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void GameOver()
    {
        // 게임오버 처리 (예: 씬 재시작)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
