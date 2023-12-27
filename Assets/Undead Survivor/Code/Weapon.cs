using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // ���� ID
    public int prefabId; // ������ ID
    public float damage; // ������
    public int count; // ����
    public float speed; // ȸ���ӵ�

    private float timer;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();    
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0.0f;
                    Fire();
                }
                break;

        }

        // # Test Code
        if (Input.GetButtonDown("Jump")) LevelUp(20, 5);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0) Batch();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150; // ���̳ʽ� -> �ð� ��������
                Batch();
                break;

            default:
                speed = 0.3f; // ����ӵ�
                break;

        }
    }

    private void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            // �θ� ��ġ�� �÷��̾� ������ �ٲٱ� ���ؼ�
            Transform bullet;
            
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // index / count �м��� �����ϸ� ����
            // 3���� 1/3 -> 2/3 -> 3/3 �̷������� ���

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // ������ ������ �ʿ��� -1 -> ����
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget) return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        // ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
