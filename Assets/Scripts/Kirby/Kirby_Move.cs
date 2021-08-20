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


    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(speed*direction, rigid.velocity.y);
        if(rigid.velocity.x > 0){
            animator.SetBool("isWalk", true);  
            spriteRenderer.flipX=false;
        }else if(rigid.velocity.x <0 ){
            animator.SetBool("isWalk", true);
            spriteRenderer.flipX=true;
        }else{
            animator.SetBool("isWalk", false);
        }

        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(0,rigid.velocity.y);
        }
    }

}
