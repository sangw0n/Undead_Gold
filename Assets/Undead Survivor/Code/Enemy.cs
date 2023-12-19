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
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        // # 목표의 X축 값과 자신의 X축 값을 비교하여 작으면 true
        sprite.flipX = target.position.x < rigid.position.x; 
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(!trigger.CompareTag("Bullet")) return;

        health -= trigger.GetComponent<Bullet>().damage;
        if(health > 0)
        {
            // # Live, Hit Action

        }
        else
        {
            // # Die
            Dead();
        }
    }
}

