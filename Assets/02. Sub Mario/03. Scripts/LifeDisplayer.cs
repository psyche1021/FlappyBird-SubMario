using UnityEngine;
using System.Collections.Generic;

public class LifeDisplayer : MonoBehaviour
{
    public List<GameObject> lifeImages;

    public void SetLives(int life)
    {
        for (int i = 0; i < lifeImages.Count; i++)
        {
            if (i < life)
            {
                lifeImages[i].SetActive(true);
            }
            else
            {
                lifeImages[i].SetActive(false);
            }
        }
    }
}
