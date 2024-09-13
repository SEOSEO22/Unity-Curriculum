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

    private void Awake()
    {
        #region ΩÃ±€≈Ê
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
        settingPanel = GameObject.Find("UICanvas").transform.GetChild(0).gameObject; ;

        if (Input.GetKeyDown(KeyCode.Escape))
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
        hearts.Add(Instantiate(heart, this.transform).GetComponent<Image>());
        GetLifeNum();
    }

    public int GetLifeNum()
    {
        hearts = GameObject.Find("Hearts").GetComponentsInChildren<Image>().ToList<Image>();
        return hearts.Count;
    }
}
