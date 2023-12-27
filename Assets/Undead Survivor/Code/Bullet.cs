using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 관통

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per; 

        if(per > -1) // 관통이 있으면 원거리 무기로 
        {
            rigid.velocity = dir * 15.0f; // 일정한 방향으로 속도로 날아감
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (!trigger.CompareTag("Enemy") || per == -1) return;

        per--;
        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
