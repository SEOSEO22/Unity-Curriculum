using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    // 오브젝트 풀 초기화
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // 풀에서 비활성화된 게임 오브젝트 찾기
    public GameObject Spawn(int index)
    {
        GameObject select = null;

        // 비활성 게임 오브젝트가 있을 경우 select에 할당
        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        // 비활성 오브젝트가 없을 경우 새로 생성하여 select에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
