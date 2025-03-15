using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(10, 0);

    void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            collision.GetComponent<EnemyController>().Hit(1);
        }
    }
}
