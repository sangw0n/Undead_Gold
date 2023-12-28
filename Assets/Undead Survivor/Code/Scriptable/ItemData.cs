using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // ����, ���Ÿ�, �尩, �Ź�, ȸ��
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("[ Main Info ]")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc; // ������ ����
    public Sprite itemIcon;

    [Header("[ Level Data ]")]
    public float baseDamage;
    public int baseCount; // ���� Or ����
    public float[] damages;
    public int[] counts;

    [Header("[ Weapon ]")]
    public GameObject projecttile; // ����ü
}
