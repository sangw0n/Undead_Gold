using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    public float gameTime = 0.0f;
    public float maxGameTime = 2 * 10.0f; // 20ÃÊ

    [Header("[ Manager Header ]")]
    public PoolManager poolManager;

    [Header("[ Other Header ]")]
    public Player player;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
