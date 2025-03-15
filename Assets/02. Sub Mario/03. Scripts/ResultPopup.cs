using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ResultPopup : MonoBehaviour
{
    public TMP_Text resultTitle;
    public TMP_Text scoreLabel;
    public GameObject highScoreObject;
    public GameObject highScorePopup;

    void OnEnable()
    {
        Time.timeScale = 0;

        if (GameManager.instance.IsCleared)
        {
            resultTitle.text = "Cleared!!";
            scoreLabel.text = GameManager.instance.timeLimit.ToString("#.##");
            SaveHighScore();
        }
        else
        {
            resultTitle.text = "Game Over...";
            scoreLabel.text = "";
        }
    }

    /// TODO
    /// high score Ȯ���ϰ� ����ϱ�
    void SaveHighScore()
    {
        float score = GameManager.instance.timeLimit;
        float highScore = PlayerPrefs.GetFloat("highscore", 0);

        if (GameManager.instance.timeLimit > highScore)
        {
            highScoreObject.SetActive(true);
            PlayerPrefs.SetFloat("highscore", GameManager.instance.timeLimit);
        }
        else
        {
            highScoreObject.SetActive(false);
        }

        /// TODO
        /// score 10�� �����ϱ�
        string currentScoreString = score.ToString("#.##");
        string savedScoreString = PlayerPrefs.GetString("HighScores", ""); // �Ű������� ""�� ����Ʈ��

        if (savedScoreString == "") // ������ ������ (����Ʈ��) ���� ���̽��ھ�� ���
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(','); // ����� ���ھ �迭�� �и�
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++) // ������ ��ġ�� �� ���ھ� �ֱ�
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score) // �� ������ ���� ������?
                {
                    scoreList.Insert(i, currentScoreString); // �ڷ� �о������
                    break;
                }
            }
            if (scoreArray.Length == scoreList.Count) // ������ ��ġ�� ����ã�Ҵٸ� �ʴ� ����
            {
                scoreList.Add(currentScoreString);
            }

            if (scoreList.Count > 9) // 9�� �Ѱ� �����ߴٸ� ����Ʈ�� �� ������ ����
            {
                scoreList.RemoveAt(9); 
            }

            string result = string.Join(",", scoreList); // , ���� ����Ʈ�� �ϳ��� string���� ����
            Debug.Log(result);
            PlayerPrefs.SetString("HighScores", result);
        }
        PlayerPrefs.Save();
    }

    public void TryAgainPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SubMario_GameScene");
    }

    public void QuitPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SubMario_LevelSelect");
    }

    public void ShowHighScorePressed()
    {
        highScorePopup.SetActive(true);
    }
}
