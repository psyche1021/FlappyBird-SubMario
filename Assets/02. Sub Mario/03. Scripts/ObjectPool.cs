using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialObjectNumber; // 초기 개수 지정

    List<GameObject> objs;

    void Start()
    {
        objs = new List<GameObject>();

        for (int i = 0; i < initialObjectNumber; i++)
        {
            GameObject go = Instantiate(prefab, transform);
            go.SetActive(false);
            objs.Add(go);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject go in objs)
        {
            if (!go.activeSelf) // 활성화 중이 아니라면
            {
                go.SetActive(true); // 활성화 해주고 반환
                return go;
            }
        }

        // 혹시라도 풀이 넘칠 경우 Instantiate로 추가 생성
        GameObject obj = Instantiate(prefab, transform);
        objs.Add(obj);
        return obj;
    }
}
