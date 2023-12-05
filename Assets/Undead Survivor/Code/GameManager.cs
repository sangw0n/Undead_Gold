using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    public Player player;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
}
