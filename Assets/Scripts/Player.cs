using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed&Time Setting")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float doubleJumpForce = 5f;
    [SerializeField] float invincibleTime = 2f;

    [Header("Move Boundary")]
    [SerializeField] Camera mainCamera;
    [SerializeField] float paddingL = 5f;
    [SerializeField] float paddingR = 15f;
    private Vector2 minBound;
    private Vector2 maxBound;
    private Vector2 limitedTrans;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer renderer;

    public bool isStartRunning;
    public bool isDead;
    private int jumpCount;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        minBound = mainCamera.ViewportToWorldPoint(Vector3.zero);
        maxBound = mainCamera.ViewportToWorldPoint(Vector3.one * .5f);

        GameManager.Instance.isPlayerDead = false;
        ReturnSetting();
    }

    void Update()
    {
        if (isDead) return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cat_Run"))
            isStartRunning = true;

        if (isStartRunning)
        {
            OnRun();
            OnJump();
        }
    }

    private void OnRun()
    {
        #region 달리기
        float xPos = Input.GetAxisRaw("Horizontal");
        transform.localScale = new Vector3(Mathf.Sign(xPos), 1, 1);
        transform.Translate(xPos * runSpeed * Time.deltaTime, 0, 0);

        // 플레이어 이동 구역 제한
        limitedTrans.x = Mathf.Clamp(transform.position.x, minBound.x + paddingL, maxBound.x - paddingR);
        limitedTrans.y = transform.position.y;
        transform.position= limitedTrans;
        #endregion
    }

    private void OnJump()
    {
        #region 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            float force = jumpForce;
            string animTrigger = "IsJumping";

            if (jumpCount == 1)
            {
                force = doubleJumpForce;
                animTrigger = "IsDoubleJumping";
            }

            rigid.velocity = new Vector2(0, force);
            SoundManager.Instance.PlaySound("Jump");
            anim.SetTrigger(animTrigger);
            jumpCount++;
        }
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 땅과 충돌 시 점프 카운트 초기화
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if (!isDead)
            {
                anim.ResetTrigger("IsDoubleJumping");
                anim.SetBool("IsRunning", true);
                jumpCount = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Damaged();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (isDead) return;

            SoundManager.Instance.PlaySound("Item");

            if (collision.gameObject.CompareTag("Health Potion"))
            {
                GameManager.Instance.GetLifePotion();
                collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.CompareTag("Invincible Potion"))
            {
                InvinciblePotion potion = GameObject.FindGameObjectWithTag("Invincible Potion UI").GetComponent<InvinciblePotion>();
                potion.GetPotion();

                collision.gameObject.SetActive(false);
            }
        }
    }

    private void Damaged()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        if (GameManager.Instance.GetLifeNum() <= 1)
        {
            Die();
        }
        else
        {
            GameManager.Instance.Damaged();
            anim.SetTrigger("IsHurt");
            renderer.color = new Color32(255, 255, 255, 200);

            Invoke("ReturnSetting", invincibleTime);
        }
    }

    private void Die()
    {
        GameManager.Instance.Damaged();

        isDead = true;
        GameManager.Instance.isPlayerDead = true;
        anim.SetBool("IsRunning", false);
        anim.SetTrigger("IsDead");

        isStartRunning = false;

        GameObject.Find("Game End Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ReturnSetting()
    {
        renderer.color = Color.white;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }
}
