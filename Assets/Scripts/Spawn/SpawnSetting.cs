using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSetting : MonoBehaviour
{
    [SerializeField] Vector2 spawnPoint;
    [SerializeField] float moveSpeed;

    private void OnEnable()
    {
        transform.position = spawnPoint;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }
}
