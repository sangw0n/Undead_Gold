using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 직렬화 : 개체를 저장 혹은 전송하기 위해 변환
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
        // 자기 자신도 포함
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // Mathf.FloorToInt : 소수점 아래는 버리고 int형으로 바꿔주는 함수 
        // Ex : 10.5 -> 10 (1레벨) / 20.55 -> 20 ( 2레벨)

        // 인덱스 에러 방지
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
         Enemy 스크립트에서 컴포넌트를 Start 에서 초기화 하면 Init 을 호출하는 속도보다
         느려 오류가 발생함 주의에서 작업해야 함 
        */

        enemy.GetComponent<Enemy>().Init(spawnData[spawnLevel]);
    }
}
