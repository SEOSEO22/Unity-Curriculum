using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnReturnStartSceneButtonClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
