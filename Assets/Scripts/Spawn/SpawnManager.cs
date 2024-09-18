using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    // ������Ʈ Ǯ �ʱ�ȭ
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // Ǯ���� ��Ȱ��ȭ�� ���� ������Ʈ ã��
    public GameObject Spawn(int index)
    {
        GameObject select = null;

        // ��Ȱ�� ���� ������Ʈ�� ���� ��� select�� �Ҵ�
        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        // ��Ȱ�� ������Ʈ�� ���� ��� ���� �����Ͽ� select�� �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
