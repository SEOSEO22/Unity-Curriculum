using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Player player;
    [SerializeField] Score score;

    [Header("Set Spawn Time")]
    [SerializeField] float minTime = 3f;
    [SerializeField] float maxTime = 6f;

    private void Start()
    {
        StartCoroutine("SpawnObstacle");
    }

    private void Update()
    {
        if (score.GetScore() < 3000)
        {
            minTime = 3f; maxTime = 6f;
        }
        else if (score.GetScore() < 6000)
        {
            minTime = 2f; maxTime = 4f;
        }
        else if (score.GetScore() < 12000)
        {
            minTime = 1f; maxTime = 4f;
        }
        else if (score.GetScore() < 18000)
        {
            minTime = .5f; maxTime = 3f;
        }
    }

    IEnumerator SpawnObstacle()
    {
        if (!player.isStartRunning)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine("SpawnObstacle");
        }

        while (player.isStartRunning)
        {
            spawnManager.Spawn((int)Random.Range(0f, 3f));
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
