using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // 근접, 원거리, 장갑, 신발, 회복
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("[ Main Info ]")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc; // 아이템 설명
    public Sprite itemIcon;

    [Header("[ Level Data ]")]
    public float baseDamage;
    public int baseCount; // 관통 Or 개수
    public float[] damages;
    public int[] counts;

    [Header("[ Weapon ]")]
    public GameObject projecttile; // 투사체
}
