using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack : MonoBehaviour
{
    [SerializeField] GameObject egg;
    GameObject player;

    private bool isAttacked;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        isAttacked = false;
    }

    void Update()
    {
        if (transform.position.x <= player.transform.position.x && !isAttacked)
        {
            isAttacked = true;
            Instantiate(egg, transform.transform.position, Quaternion.identity);
        }
    }
}
