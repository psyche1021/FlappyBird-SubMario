using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x < -10) 
            Destroy(gameObject);
    }
}
