using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvinciblePotion : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI num;
    [SerializeField] Player player;
    [SerializeField] float invinciblePotion = 5f;

    private int potionNum = 0;
    private bool isPotionUsing;

    private void Update()
    {
        if (isPotionUsing || !player.isStartRunning) return;

        if (potionNum > 0 && Input.GetKeyDown(KeyCode.R))
        {
            UsePotion();
        }
    }

    public void GetPotion()
    {
        potionNum++;
        num.text = potionNum.ToString("D2");
    }

    private void UsePotion()
    {
        if (Time.timeScale == 0) return;

        isPotionUsing = true;
        image.fillAmount = 0f;
        potionNum--;
        num.text = potionNum.ToString("D2");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        StartCoroutine(FillImage());
    }

    private IEnumerator FillImage()
    {
        float elapsed = 0f;

        while (image.fillAmount < 1)
        {
            elapsed += Time.deltaTime;
            image.fillAmount = Mathf.Clamp01(elapsed / invinciblePotion);
            yield return null;
        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        isPotionUsing = false;
    }
}
