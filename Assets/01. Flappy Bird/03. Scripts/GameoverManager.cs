using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = score.ToString();
    }

    public void OnPlayAgainPressed() => SceneManager.LoadScene("FlappyBirdIngame");

    public void OnQuitPressed() => Application.Quit();
}