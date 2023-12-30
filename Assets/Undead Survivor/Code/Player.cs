using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void Update()
    {
        if (!GameManager.instance.isLive) return;
        // GetAxis : 가속도가 붙는 느낌, GetAxisRaw : 일정하게 이동
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }   

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;


        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    // 프레임이 종료되기 전 실행되는 생명주기 함수
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;

        // magnitude : 벡터의 순수한 크기값
        anim.SetFloat("Speed", inputVec.magnitude);
      
        // 키를 입력하지 않았으면 실행되면 안됨
        if(inputVec.x != 0) sprite.flipX = inputVec.x < 0;
    }
}
