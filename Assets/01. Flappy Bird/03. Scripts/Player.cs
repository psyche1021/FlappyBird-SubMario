using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float gravity, accel, velocity;

    public AudioClip upSound;
    public AudioClip downSound;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(upSound);
        }

        if (Input.GetButtonUp("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(downSound);
        }

        if (Input.GetButton("Jump"))
        {
            velocity -= accel * Time.deltaTime;
        }
        else
        {
            velocity += gravity * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.down * velocity * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            int score = (int)FlappyBirdGameManager.instance.Score;
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("FlappyBirdGameover");
        }
    }
}
