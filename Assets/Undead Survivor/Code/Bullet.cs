using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // ����

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // �Ѿ��� �� ���̸� �ٽ� Pool�� ����
        Vector3 bulletPos = transform.position;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        float diff = Vector3.Distance(bulletPos, playerPos);

        if (diff > 15)
        {
            gameObject.SetActive(false);
        }

    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per; 

        if(per >= 0) // ������ ������ ���Ÿ� ����� 
        {
            rigid.velocity = dir * 15.0f; // ������ �������� �ӵ��� ���ư�
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (!trigger.CompareTag("Enemy") || per == -100) return;

        // ����
        per--;
        if(per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
