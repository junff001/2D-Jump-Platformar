# Unity-Projects 첫 프로젝트

**장르** : 2D 플랫포머  
**개발 참여인원** : 1인


## 기능
- Platform Effetor 2D 와 TileMap 을 활용하여 스테이지를 구성 및 구현하였습니다.
![화면 캡처 2022-03-23 005306](https://user-images.githubusercontent.com/71419212/159523037-e2ec1629-7606-40e4-9553-cb4b841c62e4.png)
- 총 3개의 스테이지는 프리팹으로 만들어 매 도착지점 마다 On/Off를 실행하여 다음 스테이지로 전환하게 끔 만들었습니다.
![화면 캡처 2022-03-23 004812](https://user-images.githubusercontent.com/71419212/159522816-443b16c4-1a37-46ce-b4d7-c9d78f672bfb.png)
- 버추얼 시네마틱 카메라를 사용하여 카메라가 플레이어를 따라가게끔 만들었습니다.
![image](https://user-images.githubusercontent.com/71419212/159523343-884ff72f-7ba3-4a2a-9b87-4eea8b99702b.png)
- 아바타의 이동과 애니메이션 등은 PlayerController 와 PlayerAnimation 이라는 두 개의 스크립트에서 구현되었습니다.
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Platform plat;
    Animator animator;
    Rigidbody2D rigid;
    PlayerInput input;
    SpriteRenderer spriteRenderer;
    PlayerAnimation playerAnimation;

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
   
    [Header("�ٴ� ���� ����")]
    public bool isGround;
    public LayerMask whatIsGround;
    public Transform groundChecker;

    void Start()
    {
        plat = GetComponent<Platform>();
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
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

        isGround = (Physics2D.OverlapArea(area1.position, area2.position, whatIsGround)); // rigid.velocity.y <= 0

        if (isGround)
        {
            currentJumpCount = jumpCount; //���� ������
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
}

```
```
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
```
- 스테이지를 전환하는 로직은 GameManager 스크립트에서 해줍니다.  
![image](https://user-images.githubusercontent.com/71419212/159523840-c3bec999-0603-4787-b900-9331cdf52d01.png)
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int stageIndex;
    public GameObject[] stage;

    private void Awake()
    {
        instance = this;
    }

    public void NextStage()
    {
        if(stageIndex < stage.Length - 1) //2
        {
            stage[stageIndex].SetActive(false); // 1. 2. 3
            stageIndex++;
            stage[stageIndex].SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
```


