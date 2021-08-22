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
    float RunDelay=0.4f; // 더블클릭 시간 
    


    
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
        RunDelay=0.4f;
        while(RunDelay >=0){
            RunDelay -= Time.deltaTime;
            yield return null;
        }
        RunDelay=0.4f;
    }
    void Update()
    {   
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
        Debug.Log(RunDelay);

        //왼쪽 이동시 로직
        if( Input.GetKey(KeyCode.LeftArrow)){
            if(RunDelay>0 && RunDelay<0.4f && rigid.velocity.x <-0.5f && isLeft) canRun = true;
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
            if(RunDelay>0 && RunDelay<0.4f && rigid.velocity.x >0.5f && isRight) canRun = true;
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

        if(rigid.velocity.x == 0){
            //속력 0시 애니메이션 다끄기 ++++++
            animator.SetBool("isWalk",false);
            animator.SetBool("isRun",false);
            canRun=false;
        }

        // Debug.Log(rightMove + " " + isRunTime);
        // if((rightMove==1 || leftMove==1) && isRunTime<=0.6f){
        //     isRunTime-=Time.deltaTime;
        // }
        // if((rightMove==1 ||leftMove==1) && isRunTime >0 && Input.GetKeyDown(KeyCode.RightArrow))canRun=true;
        // if((rightMove>=2 || leftMove>=2) && isRunTime<0f){
        //     isRunTime=0.6f;
        //     canRun=false;
        //     rightMove=0;
        //     leftMove=0;
        // }else if((rightMove>=2 || leftMove>=2)) isRunTime-=Time.deltaTime;
        


        // if(Input.GetKeyUp(KeyCode.RightArrow)) {checkRun = true; isRunTime=0; animator.SetBool("isRun",false);}
        // if(checkRun) isRunTime += Time.deltaTime;


        // if(isRunTime <=0.7f && Input.GetKeyDown(KeyCode.RightArrow)){
        //     speed=6f;
        //     animator.SetBool("isRun", true);
        //     Move();
        // }else if(checkRun){
        //     speed=3f;
        //     Move();
        // }

        // if(Input.GetButtonUp("Horizontal")){
        //     rigid.velocity = new Vector2(0,rigid.velocity.y);
        // }
    }



}
