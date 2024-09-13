using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float doubleJumpForce = 5f;

    private Rigidbody2D rigid;
    private Animator anim;

    [HideInInspector] public bool isStartRunning;
    private int jumpCount;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cat_Run"))
            isStartRunning = true;

        OnJump();
    }

    private void OnRun()
    {
        #region 달리기
        float xPos = Input.GetAxisRaw("Horizontal");
        transform.Translate(xPos * runSpeed * Time.deltaTime, 0, 0);
        #endregion
    }

    private void OnJump()
    {
        #region 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && isStartRunning)
        {
            float force = jumpForce;
            string animTrigger = "IsJumping";

            if (jumpCount == 1)
            {
                force = doubleJumpForce;
                animTrigger = "IsDoubleJumping";
            }

            rigid.velocity = new Vector2(0, force);
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
            anim.ResetTrigger("IsDoubleJump");
            anim.SetTrigger("IsRunning");
            jumpCount = 0;
        }
    }
}
