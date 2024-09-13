using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    Player player;
    int score;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isStartRunning)
        {
            SetScore();
        }
    }

    private void SetScore()
    {
        score += (int)(100 * Time.deltaTime);
        scoreText.text = score.ToString("D5");
    }
}
