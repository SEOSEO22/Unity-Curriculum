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
        if (gameObject.layer == LayerMask.NameToLayer("Item")) return;

        if (score.GetScore() < 1500)
        {
            minTime = 3f; maxTime = 5f;
        }
        else if (score.GetScore() < 3000)
        {
            minTime = 3f; maxTime = 4f;
        }
        else if (score.GetScore() < 9000)
        {
            minTime = 2.5f; maxTime = 3f;
        }
        else if (score.GetScore() < 18000)
        {
            minTime = 1f; maxTime = 2.5f;
        }
        else
        {
            minTime = .5f; maxTime = 1.5f;
        }
    }

    IEnumerator SpawnObstacle()
    {
        if (!player.isStartRunning)
        {
            yield return new WaitForSeconds(6f);
            StartCoroutine("SpawnObstacle");
        }

        while (player.isStartRunning)
        {
            spawnManager.Spawn((int)Random.Range(0f, spawnManager.prefabs.Length));
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
