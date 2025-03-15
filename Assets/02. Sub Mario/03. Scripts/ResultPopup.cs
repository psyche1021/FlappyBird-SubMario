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
    /// high score 확인하고 출력하기
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
        /// score 10개 저장하기
        string currentScoreString = score.ToString("#.##");
        string savedScoreString = PlayerPrefs.GetString("HighScores", ""); // 매개변수에 ""는 디폴트값

        if (savedScoreString == "") // 점수가 없으면 (디폴트값) 최초 하이스코어로 등록
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(','); // 저장된 스코어를 배열로 분리
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++) // 적절한 위치에 새 스코어 넣기
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score) // 앗 나보다 나쁜 점수가?
                {
                    scoreList.Insert(i, currentScoreString); // 뒤로 밀어버린다
                    break;
                }
            }
            if (scoreArray.Length == scoreList.Count) // 적절한 위치를 못맞찾았다면 너는 꼴찌
            {
                scoreList.Add(currentScoreString);
            }

            if (scoreList.Count > 9) // 9개 넘게 저장했다면 리스트의 맨 끝값은 제거
            {
                scoreList.RemoveAt(9); 
            }

            string result = string.Join(",", scoreList); // , 으로 리스트를 하나의 string으로 병합
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
