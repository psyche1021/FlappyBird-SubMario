using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State { Playing, Dead }

    public float playerSpeed;
    public float playerJumpPower;

    public Collider2D bottonCollider;
    public CompositeCollider2D terrainCollider;
    Rigidbody2D rigid;

    Vector2 originPos;
    State state;

    float vx = 0;
    bool canJump = false; // 점프 할 수 있는지
    float coyoteTime = 0.15f; // 코요테 타임
    float coyoteTimer;
    float jumpBufferTime = 0.15f; // 점프 버퍼링
    float jumpBufferTimer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originPos = transform.position;
        state = State.Playing;
    }

    void Update()
    {
        if (state != State.Playing) return; // 플레이 상태가 아니라면 Update 함수 탈출

        // 좌우 이동
        vx = Input.GetAxisRaw("Horizontal") * playerSpeed;

        // 점프체크 (점프 버퍼링 시작)
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime; // 점프신호 받으면 점프버퍼링 타이머 리셋
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime; // 점프신호 받지않으면 점프버퍼링 타이머 감소
        }
    }

    void FixedUpdate()
    {
        float vy = rigid.velocity.y;

        // 플레이어 플립
        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // 바닥 접촉 확인
        if (bottonCollider.IsTouching(terrainCollider))
        {
            if (vx == 0) GetComponent<Animator>().SetTrigger("Idle");
            else GetComponent<Animator>().SetTrigger("Run");
            canJump = true;
            coyoteTimer = coyoteTime; // 바닥에 닿으면 코요테 타이머 리셋
        }
        else
        {
            if (vy < 0) GetComponent<Animator>().SetTrigger("Fall");
            else if (vy > 0) GetComponent<Animator>().SetTrigger("Jump");
            coyoteTimer -= Time.fixedDeltaTime; // 바닥에서 떨어지면 타이머 감소
        }

        // 점프 처리
        if (jumpBufferTimer > 0f && coyoteTimer > 0f && canJump)
        {
            vy = playerJumpPower;
            canJump = false;
            coyoteTimer = 0f; // 점프 후 코요테 타이머 리셋
            jumpBufferTimer = 0f; // 점프 후 버퍼 타이머 리셋
        }

        // 리지드바디 속도 설정
        rigid.velocity = new Vector2(vx, vy);

        // 공격
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 bulletV = new Vector2(10, 0);

            if (GetComponent<SpriteRenderer>().flipX) // 바라보는 방향에 따라 총알도 뒤집어줌
            {
                bulletV.x = -bulletV.x;
            }

            GameObject bullet = GameManager.instance.bulletPool.GetObject();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletV;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Restart()
    {
        state = State.Playing;

        rigid.constraints = RigidbodyConstraints2D.FreezeRotation; // 모든 축의 회전 제한 (https://meongjeong.tistory.com/entry/UNITY-RigidbodyConstraints-Rigidbody-%EC%9B%80%EC%A7%81%EC%9E%84-%EC%A0%9C%ED%95%9C%ED%95%98%EA%B8%B0)
        rigid.angularVelocity = 0; // 회전값 정상화
        transform.eulerAngles = Vector3.zero; // 트랜스폼 회전값 초기화
        transform.position = originPos; // 위치 초기화
        rigid.velocity = Vector2.zero; // 가속을 초기화 하기 위해 velocity를 원점으로
        GetComponent<BoxCollider2D>().enabled = true; // 콜라이더 활성화
    }

    void Die()
    {
        state = State.Dead;

        rigid.constraints = RigidbodyConstraints2D.None; // 모든 축의 제한 해제
        rigid.angularVelocity = 720; // 720도 만큼 회전
        rigid.AddForce(new Vector2(0, 10), ForceMode2D.Impulse); // 강체에 충격
        GetComponent<BoxCollider2D>().enabled = false; // 콜라이더 비활성화
        GameManager.instance.Die(); // 게임매니저의 Die 함수 호출
    }
}