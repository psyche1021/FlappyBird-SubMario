using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public CompositeCollider2D terrainCollider;
    public Collider2D frontBottomCollider;
    public Collider2D frontCollider;

    int hp = 3;

    Vector2 vx;

    void Start()
    {
        vx = Vector2.right * speed;
    }

    void Update()
    {
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))
        {
            vx = -vx;
            transform.localScale = new Vector2(-transform.localScale.x, 1);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }

    public void Hit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; 
            GetComponent<Rigidbody2D>().angularVelocity = 720; 
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;

            GameManager.instance.AddTime(2f);

            Invoke("DestroyThis", 2f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
