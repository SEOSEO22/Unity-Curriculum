using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    GameObject player;

    private bool canMove;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position.x < -19f)
        {
            transform.position = Vector2.zero;
        }

        StartMoving();
    }

    private void StartMoving()
    {
        if (player.GetComponent<Player>().isStartRunning)
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }
}
