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
    bool canJump = false; // ���� �� �� �ִ���
    float coyoteTime = 0.15f; // �ڿ��� Ÿ��
    float coyoteTimer;
    float jumpBufferTime = 0.15f; // ���� ���۸�
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
        if (state != State.Playing) return; // �÷��� ���°� �ƴ϶�� Update �Լ� Ż��

        // �¿� �̵�
        vx = Input.GetAxisRaw("Horizontal") * playerSpeed;

        // ����üũ (���� ���۸� ����)
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime; // ������ȣ ������ �������۸� Ÿ�̸� ����
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime; // ������ȣ ���������� �������۸� Ÿ�̸� ����
        }
    }

    void FixedUpdate()
    {
        float vy = rigid.velocity.y;

        // �÷��̾� �ø�
        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // �ٴ� ���� Ȯ��
        if (bottonCollider.IsTouching(terrainCollider))
        {
            if (vx == 0) GetComponent<Animator>().SetTrigger("Idle");
            else GetComponent<Animator>().SetTrigger("Run");
            canJump = true;
            coyoteTimer = coyoteTime; // �ٴڿ� ������ �ڿ��� Ÿ�̸� ����
        }
        else
        {
            if (vy < 0) GetComponent<Animator>().SetTrigger("Fall");
            else if (vy > 0) GetComponent<Animator>().SetTrigger("Jump");
            coyoteTimer -= Time.fixedDeltaTime; // �ٴڿ��� �������� Ÿ�̸� ����
        }

        // ���� ó��
        if (jumpBufferTimer > 0f && coyoteTimer > 0f && canJump)
        {
            vy = playerJumpPower;
            canJump = false;
            coyoteTimer = 0f; // ���� �� �ڿ��� Ÿ�̸� ����
            jumpBufferTimer = 0f; // ���� �� ���� Ÿ�̸� ����
        }

        // ������ٵ� �ӵ� ����
        rigid.velocity = new Vector2(vx, vy);

        // ����
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 bulletV = new Vector2(10, 0);

            if (GetComponent<SpriteRenderer>().flipX) // �ٶ󺸴� ���⿡ ���� �Ѿ˵� ��������
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

        rigid.constraints = RigidbodyConstraints2D.FreezeRotation; // ��� ���� ȸ�� ���� (https://meongjeong.tistory.com/entry/UNITY-RigidbodyConstraints-Rigidbody-%EC%9B%80%EC%A7%81%EC%9E%84-%EC%A0%9C%ED%95%9C%ED%95%98%EA%B8%B0)
        rigid.angularVelocity = 0; // ȸ���� ����ȭ
        transform.eulerAngles = Vector3.zero; // Ʈ������ ȸ���� �ʱ�ȭ
        transform.position = originPos; // ��ġ �ʱ�ȭ
        rigid.velocity = Vector2.zero; // ������ �ʱ�ȭ �ϱ� ���� velocity�� ��������
        GetComponent<BoxCollider2D>().enabled = true; // �ݶ��̴� Ȱ��ȭ
    }

    void Die()
    {
        state = State.Dead;

        rigid.constraints = RigidbodyConstraints2D.None; // ��� ���� ���� ����
        rigid.angularVelocity = 720; // 720�� ��ŭ ȸ��
        rigid.AddForce(new Vector2(0, 10), ForceMode2D.Impulse); // ��ü�� ���
        GetComponent<BoxCollider2D>().enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        GameManager.instance.Die(); // ���ӸŴ����� Die �Լ� ȣ��
    }
}