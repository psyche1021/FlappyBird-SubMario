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
            //Invoke("DestroySelf", 0.4f); // 0.4�� �� �Լ� ����. 0.4���� ���� = ��������Ʈ��(6) �� ����������(15)
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
