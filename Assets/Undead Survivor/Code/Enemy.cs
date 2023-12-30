using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animController;

    private bool isLive;

    private Rigidbody2D rigid;
    private Collider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private WaitForFixedUpdate wait;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate(); // 하나의 물리프레임 쉬기
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;

        //anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") : 애니메이션이 실행중인지
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;

        if (!isLive) return;

        // # 목표의 X축 값과 자신의 X축 값을 비교하여 작으면 true
        sprite.flipX = target.position.x < rigid.position.x; 
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;

        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }


    // 애니메이션 이벤트로 호출함 
    private void Dead()
    {
        gameObject.SetActive(false);
    }

    // 비동기 함수 
    private IEnumerator KnockBack()
    {
        //1. 애니메이터의 상태가 변하기까지의 시간이 필요 -> FixedUpdate에서 조건으로 사용하기 때문
        //2. 이미 리지드바디의 MovePosition 함수 실행과의 로직 충돌을 피하기 위해

        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos =  GameManager.instance.player.transform.position;
        Vector3 dirVec = playerPos - transform.position; // 플레이어 기준 반대 방향

        //Debug.Log("dirVec : " + dirVec);
        //Debug.Log("dirVec.normalized : " + dirVec.normalized);
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(!trigger.CompareTag("Bullet") || !isLive) return;

        health -= trigger.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // # Live, Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {
            // # Die
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false; // 물리적 비활성화
            sprite.sortingOrder = 1;

            // Exp && Kill
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            anim.SetBool("Dead", true);
        }
    }
}

