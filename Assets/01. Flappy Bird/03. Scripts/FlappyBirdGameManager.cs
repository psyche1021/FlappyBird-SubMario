using UnityEngine;

public class FlappyBirdGameManager : MonoBehaviour
{
    public static FlappyBirdGameManager instance;

    public GameObject walls;
    public float spawnTerm = 4f;
    float spawnTimer;

    public float Score { get { return score; } }
    float score;

    void Awake() => instance = this;
    
    void Start()
    {
        spawnTimer = 0;
        score = 0;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        score += Time.deltaTime;

        if (spawnTimer > spawnTerm)
        {
            spawnTimer -= spawnTerm;

            GameObject obj = Instantiate(walls);
            obj.transform.position = new Vector2(10, Random.Range(-2f, 2f));
        }
    }
}
