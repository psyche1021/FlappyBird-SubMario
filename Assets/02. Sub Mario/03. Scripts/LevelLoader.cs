using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public PlayerController player;
    public GameObject cinemachine;

    void Awake()
    {
        GameManager.instance.player = player;
        GameManager.instance.cinemachine = cinemachine;
    }
}
