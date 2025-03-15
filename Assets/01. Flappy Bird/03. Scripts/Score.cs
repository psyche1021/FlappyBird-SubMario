using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    void Update()
    {
        float score = FlappyBirdGameManager.instance.Score;

        GetComponent<TextMeshProUGUI>().text = ((int)score).ToString();
    }
}
