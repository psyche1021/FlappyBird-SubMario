using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float addTime = 5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Eaten");
            GetComponent<Collider2D>().enabled = false;
            GameManager.instance.AddTime(addTime);
            //Invoke("DestroySelf", 0.4f); // 0.4초 뒤 함수 실행. 0.4초인 이유 = 스프라이트수(6) ÷ 샘플프레임(15)
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
