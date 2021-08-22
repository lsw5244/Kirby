using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    [Header("Animator")]
    public Animator animator;
    
    [Header("Move")]
    public float speed = 4f;
    SpriteRenderer spriteRenderer;
    

    [Header("Run")]
    int canRun=0;    
    float isRunTime=0.5f;
    bool isDash = false;



    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // StartCoroutine("DashCheck");
    }

    IEnumerator DashCheck(){
        while(true){
            if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
                canRun++;
                Debug.Log("True됐다");
                Debug.Log(isRunTime);
                animator.SetBool("isRun",false);            
            }

            if(canRun>=1 && !isDash){
                isRunTime -=Time.deltaTime;// 제한시간 줄이기
                if(isRunTime <= 0){ // 시간 다됐을 때 
                    canRun = 0; // 달리는 판정 false
                    isRunTime = 0.5f; //시간초 돌려주기
                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {      
        // MoveCheck();

        if( Input.GetKey(KeyCode.LeftArrow)){
            rigid.AddForce( new Vector2(speed*-1, 0),ForceMode2D.Impulse);
            if(rigid.velocity.x < -5f) 
                rigid.velocity = new Vector2(-4f,rigid.velocity.y);
            spriteRenderer.flipX=true;
            animator.SetBool("isWalk", true);
        }else if( Input.GetKey(KeyCode.RightArrow)){
            rigid.AddForce( new Vector2(speed*1, 0),ForceMode2D.Impulse);
            if(rigid.velocity.x >5f) 
                rigid.velocity = new Vector2(4f,rigid.velocity.y);
            spriteRenderer.flipX=false;
            animator.SetBool("isWalk", true);
        }else if(rigid.velocity.x == 0){
            animator.SetBool("isWalk",false);
        }
        
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

    // void MoveCheck(){
        
    //     if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && canRun>=2 ){
    //         animator.SetBool("isRun",true);
    //         Debug.Log("달린다");
    //         canRun=2;
    //         Move(10f);
    //         isDash = true;
    //     }else if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))){
    //         animator.SetBool("isWalk", true);  
    //         Move(4f);
    //         isDash = false;
    //     }else{
    //         Move(0f);
    //         isDash = false;
    //     }

    // }

    // void Move(float speed){
    //     float direction = Input.GetAxisRaw("Horizontal");

    //     rigid.velocity = new Vector2(speed*direction, rigid.velocity.y);
    //     if(rigid.velocity.x > 0){
    //         spriteRenderer.flipX=false;
    //     }else if(rigid.velocity.x <0 ){
    //         spriteRenderer.flipX=true;
    //     }else{
    //         animator.SetBool("isWalk", false);
    //         animator.SetBool("isRun", false);
    //     }
    // }

}
