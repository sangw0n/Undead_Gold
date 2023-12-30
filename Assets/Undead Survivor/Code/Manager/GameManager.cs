using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    [Header("[ Game Control ]")]
    public bool isLive;
    public float gameTime = 0.0f;
    public float maxGameTime = 2 * 10.0f; // 20초

    [Header("[ Manager Header ]")]
    public PoolManager poolManager;

    [Header("[ Other Header ]")]
    public Player player;
    public LevelUp uiLevelUp;

    [Header("[ Player Data Header ]")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        health = maxHealth;

        // 임시 스크립트 
        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        // 멈춤
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        // 작동
        isLive = true;
        Time.timeScale = 1;
    }
}
