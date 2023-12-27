using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 ID
    public int prefabId; // 프리팹 ID
    public float damage; // 데미지
    public int count; // 개수
    public float speed; // 회전속도

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
                speed = 150; // 마이너스 -> 시계 방향으로
                Batch();
                break;

            default:
                speed = 0.3f; // 연사속도
                break;

        }
    }

    private void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            // 부모 위치를 플레이어 밑으로 바꾸기 위해서
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

            // index / count 분수로 생각하면 편함
            // 3개면 1/3 -> 2/3 -> 3/3 이런식으로 계산

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // 근접은 관통이 필요없어서 -1 -> 무한
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
        // 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
