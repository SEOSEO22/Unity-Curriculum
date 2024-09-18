using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    int score;
    char rank;

    [SerializeField] GameObject scoreObject;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] GameObject buttonPanel;

    private void Start()
    {
        score = scoreObject.GetComponent<Score>().GetScore();
        scoreText.text = "최종 점수 : " + score.ToString("D6");

        if (score >= 4000) rank = 'S';
        else if (score >= 25000) rank = 'A';
        else if (score >= 13000) rank = 'B';
        else if (score >= 7000) rank = 'C';
        else if (score >= 3000) rank = 'D';
        else rank = 'F';

        rankText.text = "RANK : " + rank;

        Invoke("ActiveScoreText", 1f);
    }

    private void ActiveScoreText()
    {
        scoreText.gameObject.SetActive(true);
        Invoke("ActiveRankText", 1f);
    }

    private void ActiveRankText()
    {
        rankText.gameObject.SetActive(true);
        Invoke("ActiveButtonPanel", 1f);
    }

    private void ActiveButtonPanel()
    {
        buttonPanel.gameObject.SetActive(true);
    }
}
