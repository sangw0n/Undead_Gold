using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    private float timer;

    private void Awake()
    {
        // �ڱ� �ڽŵ� ����
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.0f)
        {
            timer = 0.0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
