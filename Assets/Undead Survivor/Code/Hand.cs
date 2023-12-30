using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer sprite;

    private SpriteRenderer player;

    private Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    private Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    private Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    private Quaternion leftReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            // 근접무기
            transform.localRotation = isReverse ? leftReverse : leftRot;
            sprite.flipY = isReverse;
            sprite.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            // 원거리무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            sprite.flipX = isReverse;
            sprite.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
