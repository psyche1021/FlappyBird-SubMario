using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialObjectNumber; // �ʱ� ���� ����

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
            if (!go.activeSelf) // Ȱ��ȭ ���� �ƴ϶��
            {
                go.SetActive(true); // Ȱ��ȭ ���ְ� ��ȯ
                return go;
            }
        }

        // Ȥ�ö� Ǯ�� ��ĥ ��� Instantiate�� �߰� ����
        GameObject obj = Instantiate(prefab, transform);
        objs.Add(obj);
        return obj;
    }
}
