using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    [Header("[ Game Control ]")]
    public float gameTime = 0.0f;
    public float maxGameTime = 2 * 10.0f; // 20ÃÊ

    [Header("[ Manager Header ]")]
    public PoolManager poolManager;

    [Header("[ Other Header ]")]
    public Player player;

    [Header("[ Kill Data Header ]")]
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
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
