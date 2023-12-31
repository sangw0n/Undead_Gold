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
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (!GameManager.instance.isLive) return;

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
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0) Batch();
           
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // 모든 자식한테 applyGear 적용
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId; // Object Name
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        // Prefab ID Setting
        for(int index = 0; index < GameManager.instance.poolManager.prefabs.Length; index++) 
        {
            if(data.projecttile == GameManager.instance.poolManager.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed; // 마이너스 -> 시계 방향으로
                Batch();
                break;

            default:
                speed = 0.3f * Character.WeaponRate; // 연사속도
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.sprite.sprite = data.hand;
        hand.gameObject.SetActive(true);       

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // 모든 자식한테 applyGear 적용
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
            // Space.World 를 사용하지 않고 이동시키면 회전 상태에 영향을 받아서 이동
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // 근접은 관통이 필요없어서 -1 -> 무한
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is 무한
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

        AudioManager.instance.PlaySfx(AudioManager.sfx.Range);
    }
}
