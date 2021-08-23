using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    [Header("Animator")]
    public Animator animator;
    
    [Header("Move")]
    public float speed = 3f;
    float dashSpeed = 1f;
    SpriteRenderer spriteRenderer;
    

    [Header("Run")]

    bool canRun =false; // 달릴수 있는지 딜레이
    bool isRight=false, isLeft = false;
    float RunDelay=0.3f; // 더블클릭 시간 

    [Header("Jump")]
    bool canJump = false; //점프 가능해?
    bool alreayJump = false; // 점프키 한번 누를때마다 한번만 점프하게 하는 부울
    [SerializeField] float jumpPower = 4f;
    bool isJumping=false;
    


    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // StartCoroutine("DashCheck");
    }



    // Update is called once per frame

    IEnumerator ActiveRunTimer(){
        RunDelay=0.3f;
        while(RunDelay >=0){
            RunDelay -= Time.deltaTime;
            yield return null;
        }
        RunDelay=0.3f;
    }

    void Jump(){
        if(canJump && !alreayJump){
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
        }
    }

    void FixedUpdate(){
        
        Debug.DrawRay(transform.position, transform.up*-1, Color.blue, 0.3f);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        if(rayHit.collider!=null) {
            canJump = true;
            isJumping=false;
            Debug.Log(isJumping);
            animator.SetBool("isJumpDown",false);
        }
        else{ 
            canJump = false;
            if(alreayJump) isJumping =true;

        }

        



    }

    float a=0f;
    void Update()
    {   
    

        if(Input.GetKeyUp(KeyCode.Z)) alreayJump = true;
        if(Input.GetKeyUp(KeyCode.RightArrow)){
            StopCoroutine("ActiveRunTimer");
            StartCoroutine("ActiveRunTimer");
            isRight=true; //
            isLeft=false; // 좌우 도리도리 했을때 바로 대쉬 되는 것 방지
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow)){
            StopCoroutine("ActiveRunTimer");
            StartCoroutine("ActiveRunTimer");
            isRight=false;
            isLeft=true;
        }

        //왼쪽 이동시 로직
        if( Input.GetKey(KeyCode.LeftArrow)){
            if(RunDelay>0 && RunDelay<0.3f && rigid.velocity.x <-0.5f && isLeft) canRun = true;
            if(canRun){
                animator.SetBool("isRun",true);
                rigid.AddForce( new Vector2(dashSpeed*-1, 0),ForceMode2D.Impulse);
                if(rigid.velocity.x < -8f) 
                    rigid.velocity = new Vector2(-8f,rigid.velocity.y);            
            }else{
                animator.SetBool("isWalk", true);
                rigid.AddForce( new Vector2(speed*-1, 0),ForceMode2D.Impulse); 
                if(rigid.velocity.x < -5f) 
                    rigid.velocity = new Vector2(-5f,rigid.velocity.y);

            }

            spriteRenderer.flipX=true;
        }
        //오른쪽 이동 시 로직
        else if( Input.GetKey(KeyCode.RightArrow)){
            if(RunDelay>0 && RunDelay<0.3f && rigid.velocity.x >0.5f && isRight) canRun = true;
            if(canRun){
                animator.SetBool("isRun",true);
                rigid.AddForce( new Vector2(dashSpeed*1, 0),ForceMode2D.Impulse);
                if(rigid.velocity.x > 8f) 
                    rigid.velocity = new Vector2(8f,rigid.velocity.y);            
            }else{
                animator.SetBool("isWalk", true);
                rigid.AddForce( new Vector2(speed*1, 0),ForceMode2D.Impulse); 
                if(rigid.velocity.x > 5f) 
                    rigid.velocity = new Vector2(5f,rigid.velocity.y);

            }

            spriteRenderer.flipX=false;

        }

        if(Input.GetKey(KeyCode.Z)){
             Jump(); // 점프
             alreayJump=true;
        }
        if(Input.GetKeyUp(KeyCode.Z) && rigid.velocity.y>=0){
            alreayJump=false;
            rigid.velocity = new Vector2(rigid.velocity.x,0); // 점프
        }

        if(rigid.velocity.x == 0){
            //속력 0시 애니메이션 다끄기 ++++++
            animator.SetBool("isWalk",false);
            animator.SetBool("isRun",false);
            canRun=false;
        }
        
        if(isJumping && rigid.velocity.y>0) {
            animator.SetBool("isJumpUp", true);
            animator.SetBool("isJumpDown", false);
        }
        else if(isJumping && rigid.velocity.y<=0){
            animator.SetBool("isJumpDown", true);
            animator.SetBool("isJumpUp", false);
        }

    }



}
