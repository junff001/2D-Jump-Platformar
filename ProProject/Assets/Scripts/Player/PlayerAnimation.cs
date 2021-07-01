using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    PlayerController playerController;

    private readonly int hashXSpeed = Animator.StringToHash("Speed");
    private readonly int hashYSpeed = Animator.StringToHash("ySpeed");
    private readonly int hashIsJumping = Animator.StringToHash("IsJumping");
    

    private bool jumping = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rigid.velocity.x));
        anim.SetFloat(hashYSpeed, rigid.velocity.y);
       
    }

    public void Jump()
    {
        if (!jumping)
        {
            anim.SetBool(hashIsJumping, true);
        }

        anim.SetBool(hashIsJumping, true);
        jumping = true;
    }

    public void JumpEnd()
    {
        anim.SetBool(hashIsJumping, false);
        jumping = false;
    }
}
