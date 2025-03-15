using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public GameObject LevelPanelPrefab;
    public GameObject ScrollViewContent;

    void Start()
    {
        for (int i = 0; i < LevelManager.Instance.levels.Count; i++)
        {
            LevelInfo info = LevelManager.Instance.levels[i];
            GameObject go = Instantiate(LevelPanelPrefab, ScrollViewContent.transform);
            go.GetComponent<LevelPanel>().SetLevelInfomation(i, info.LevelThumb, info.LevelName);
        }
    }
}
