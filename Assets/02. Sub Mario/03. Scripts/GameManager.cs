using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LifeDisplayer lifeDisplayerInstance;
    public PlayerController player;
    public GameObject cinemachine;
    public GameObject popupCanvas;
    public ObjectPool bulletPool;
    public TMP_Text timeLimitLabel;
    public float timeLimit = 30f;

    private bool isCleared;
    public bool IsCleared { get { return isCleared; } }
    
    [SerializeField]
    private int life;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        life = 3;
        Instantiate(LevelManager.Instance.SelectedPrefab);
        lifeDisplayerInstance.SetLives(life);
    }

    void Update()
    {
        timeLimit -= Time.deltaTime;
        timeLimitLabel.text = "" + ((int)timeLimit);

        if (timeLimit <= 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Die();
        }
    }

    public void AddTime(float addTime)
    {
        timeLimit += addTime;
    }

    public void Die()
    {
        cinemachine.SetActive(false);
        life--;
        lifeDisplayerInstance.SetLives(life);

        Invoke("Restart", 2f);
    }

    void Restart()
    {
        if (life > 0)
        {
            cinemachine.SetActive(true);
            player.Restart();
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isCleared = false;
        popupCanvas.SetActive(true);
    }

    public void GameClear()
    {
        isCleared = true;
        popupCanvas.SetActive(true);
    }
}
