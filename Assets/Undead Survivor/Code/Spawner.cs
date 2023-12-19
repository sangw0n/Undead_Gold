using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ȭ : ��ü�� ���� Ȥ�� �����ϱ� ���� ��ȯ
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    [SerializeField]
    private int spawnLevel; 

    private float timer;

    private void Awake()
    {
        // �ڱ� �ڽŵ� ����
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // Mathf.FloorToInt : �Ҽ��� �Ʒ��� ������ int������ �ٲ��ִ� �Լ� 
        // Ex : 10.5 -> 10 (1����) / 20.55 -> 20 ( 2����)

        // �ε��� ���� ����
        spawnLevel = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),  spawnData.Length - 1);

        if (timer > spawnData[spawnLevel].spawnTime)
        {
            timer = 0.0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        /*
         Enemy ��ũ��Ʈ���� ������Ʈ�� Start ���� �ʱ�ȭ �ϸ� Init �� ȣ���ϴ� �ӵ�����
         ���� ������ �߻��� ���ǿ��� �۾��ؾ� �� 
        */

        enemy.GetComponent<Enemy>().Init(spawnData[spawnLevel]);
    }
}
