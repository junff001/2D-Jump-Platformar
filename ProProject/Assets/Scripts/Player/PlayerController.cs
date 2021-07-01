using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Platform plat;
    Animator animator;
    Rigidbody2D rigid;
    PlayerInput input;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    PlayerAnimation playerAnimation;
    CircleCollider2D circleCollider2D;
    public Transform area1;
    public Transform area2;
    public Transform[] rePosition;

    public int moveSpeed = 5;
    public int jumpCount = 2;
    public float jumpPower = 30f;
    private float jumpTimer;
    public float jumpTime;
    private int currentJumpCount;
    private bool isJump = false;
    public float maxSpeed = 5f;
    
   

    [Header("¹Ù´Ú °¨Áö °ü·Ã")]
    public bool isGround;
    public LayerMask whatIsGround;
    public Transform groundChecker;

    void Start()
    {
        plat = GetComponent<Platform>();
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        
    }

    void Update()
    {
        if (input.jump)
        {
            isJump = true;
        }
        //if (input.jump)
        //{
        //    jumpTimer = jumpTime;
        //}
        //if (input.jumpLong )
        //{
        //    if (jumpTimer > 0)
        //    {
        //        jumpTimer -= Time.deltaTime;
        //    }
        //}
    }

    void FixedUpdate()
    {
        float move = input.move;
        if (move > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (move < 0)
        {
            spriteRenderer.flipX = true;
            
        }

        isGround = (Physics2D.OverlapArea(area1.position, area2.position, whatIsGround) && rigid.velocity.y <= 0);

        if (isGround)
        {
            currentJumpCount = jumpCount; //¶¥¿¡ ´êÀ¸¸é
        }

        if (isJump && (isGround || currentJumpCount > 0))
        {
            currentJumpCount--;
            rigid.velocity = Vector2.zero;
            playerAnimation.Jump();
            rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            //if (input.jumpLong)
            //{
            //    rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);

            //}
        }

        isJump = false;

        if (isGround && rigid.velocity.y < 0.1f)
        {
            playerAnimation.JumpEnd();
        }

        //if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        //{
        //    rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxSpeed, rigid.velocity.y);
        //}

        rigid.velocity = new Vector2(move * moveSpeed, rigid.velocity.y);
        //rigid.AddForce(Vector2.right * move * moveSpeed);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.NextStage();
            RePosition();
        }
    }

    public void RePosition()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.position = rePosition[i].position;
            
        }
    }

    //void modifyPhysics()
    //{
    //    bool changingDirections = (input.move > 0 && rigid.velocity.x < 0) || (input.move < 0 && rigid.velocity.x > 0);
    //    if(rigid.velocity.y (Mathf.Abs(input.move) < 0.1f || changingDirections))
    //    {
    //        rigid.drag = LinearDrag;
    //    }
    //    else
    //    {
    //        rigid.drag = 0f;
    //    }
    //}

    //public bool IsGround()
    //{
    //    float depth = 0.1f;
    //    RaycastHit2D hit = Physics2D.BoxCast(groundChecker.position, boxCollider2D.bounds.size,
    //                                         0f, Vector2.down, depth, whatIsGround);

    //    Color rayColor;
    //    if (hit.collider != null)
    //    {
    //        rayColor = Color.green;
    //    }
    //    else
    //    {
    //        rayColor = Color.red;
    //    }

    //    //Debug.DrawRay(transform.position, Vector2.down * depth, rayColor);
    //    return hit.collider != null;
    //}







}
