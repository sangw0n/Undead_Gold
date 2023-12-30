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
        wait = new WaitForFixedUpdate(); // �ϳ��� ���������� ����
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;

        //anim.GetCurrentAnimatorStateInfo(0).IsName("Hit") : �ִϸ��̼��� ����������
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

        // # ��ǥ�� X�� ���� �ڽ��� X�� ���� ���Ͽ� ������ true
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


    // �ִϸ��̼� �̺�Ʈ�� ȣ���� 
    private void Dead()
    {
        gameObject.SetActive(false);
    }

    // �񵿱� �Լ� 
    private IEnumerator KnockBack()
    {
        //1. �ִϸ������� ���°� ���ϱ������ �ð��� �ʿ� -> FixedUpdate���� �������� ����ϱ� ����
        //2. �̹� ������ٵ��� MovePosition �Լ� ������� ���� �浹�� ���ϱ� ����

        yield return wait; // ���� �ϳ��� ���� ������ ������
        Vector3 playerPos =  GameManager.instance.player.transform.position;
        Vector3 dirVec = playerPos - transform.position; // �÷��̾� ���� �ݴ� ����

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
            rigid.simulated = false; // ������ ��Ȱ��ȭ
            sprite.sortingOrder = 1;

            // Exp && Kill
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            anim.SetBool("Dead", true);
        }
    }
}

