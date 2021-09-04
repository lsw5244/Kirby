using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Slime : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rigi;

    float moveDir = -1;
    public float speed = 3;
    bool isAttack = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckFloor();
        PlayerCheck();
        rigi.velocity = new Vector2(moveDir * speed, rigi.velocity.y);
        if (Input.GetMouseButtonDown(0))
        {
            Die();
        }

    }
    void CheckFloor()
    {
        // 바닥 체크
        Vector2 frontVector = new Vector2(rigi.position.x + moveDir * 0.8f, rigi.position.y);
        RaycastHit2D hit = Physics2D.Raycast(frontVector, Vector3.down, 1);
        if (hit.collider == null)
        {
            moveDir *= -1;
            transform.Rotate(0, 180, 0);
        }
        ani.SetBool("Walk", true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PLAYER")
        {
            speed *= 2;
            if ((coll.transform.position.x > transform.position.x) && (moveDir == -1)
                || (coll.transform.position.x < transform.position.x) && (moveDir == 1))
            {
                moveDir *= -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PLAYER")
        {
            speed *= 0.5f;
        }
    }

    void Die()
    {
        Destroy(this.transform.parent.gameObject);
    }

    void PlayerCheck()
    {
        float radius = 0.5f;
        float Range = 0.5f;  // 원 넓이(공격 범위)

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, new Vector2(1, 0) * moveDir, Range, LayerMask.GetMask("Player"));
        if (hits.Length > 0 && !isAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        //StopAllCoroutines();
        float temp = moveDir;
        moveDir = 0;
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        isAttack = false;
        moveDir = temp;
    }
}