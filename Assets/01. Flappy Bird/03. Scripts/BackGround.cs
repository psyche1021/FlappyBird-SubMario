using UnityEngine;

public class BackGround : MonoBehaviour
{
    float speed, width;

    void Start()
    {
        speed = 10f;
        width = 19.2f;
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x < -width)
            transform.Translate(new Vector3(width, 0));
    } 
}