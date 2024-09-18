using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource bgm_player;
    [SerializeField] AudioSource sfx_player;

    private Slider bgm_slider;
    private Slider sfx_slider;

    public AudioClip[] audio_clips;

    private void Awake()
    {
        #region 싱글톤
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
    }

    private void OnEnable()
    {
        // 씬이 로드될 때마다 슬라이더를 다시 찾기
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때 슬라이더 연결
        FindSliders();
    }

    public void ParentObjectActivated() // 부모 오브젝트가 활성화될 때 호출
    {
        FindSliders();
    }

    private void FindSliders()
    {
        GameObject settingCanvas = GameObject.Find("Setting Canvas");

        if (settingCanvas != null)
        {
            Transform settingPanel = settingCanvas.transform.Find("Setting Panel");

            if (settingPanel != null)
            {
                Transform settingImage = settingPanel.Find("Setting Image");

                if (settingImage != null)
                {
                    bgm_slider = settingImage.Find("BGM Slider")?.GetComponent<Slider>();
                    sfx_slider = settingImage.Find("SFX Slider")?.GetComponent<Slider>();
                }
            }
        }

        if (bgm_slider != null)
        {
            bgm_slider.value = bgm_player.volume; // 슬라이더 초기값 설정
            bgm_slider.onValueChanged.AddListener(ChangeBgmSound); // 리스너 추가
        }

        if (sfx_slider != null)
        {
            sfx_slider.value = sfx_player.volume; // 슬라이더 초기값 설정
            sfx_slider.onValueChanged.AddListener(ChangeSfxSound); // 리스너 추가
        }
    }


    public void PlaySound(string type)
    {
        int index = 0;

        switch (type)
        {
            case "Jump": index = 0; break;
            case "Item": index = 1; break;
        }

        sfx_player.clip = audio_clips[index];
        sfx_player.PlayOneShot(sfx_player.clip);
    }

    void ChangeBgmSound(float value)
    {
        bgm_player.volume = value;
    }

    void ChangeSfxSound(float value)
    {
        PlaySound("Jump");
        sfx_player.volume = value;
    }
}
