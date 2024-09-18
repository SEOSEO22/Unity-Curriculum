using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject heart;
    private List<Image> hearts;
    private GameObject settingPanel;
    public bool isPlayerDead;

    private void Awake()
    {
        #region ½Ì±ÛÅæ
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GetLifeNum();
        }
    }

    private void Update()
    {
        SetSettingPanel();
    }

    private void SetSettingPanel()
    {
        settingPanel = GameObject.Find("Setting Canvas").transform.GetChild(0).gameObject;

        if (Input.GetKeyDown(KeyCode.Escape) && !isPlayerDead)
        {
            settingPanel.SetActive(!settingPanel.activeSelf);
        }
    }

    public void Damaged()
    {
        GetLifeNum();
        Destroy(hearts[hearts.Count - 1].gameObject);
    }

    public void GetLifePotion()
    {
        if (hearts.Count >= 5) return;

        hearts.Add(Instantiate(heart, GameObject.Find("Hearts").transform).GetComponent<Image>());
        GetLifeNum();
    }

    public int GetLifeNum()
    {
        hearts = GameObject.Find("Hearts").GetComponentsInChildren<Image>().ToList<Image>();
        return hearts.Count;
    }
}
