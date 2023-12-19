using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Repostion : MonoBehaviour
{
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (!trigger.CompareTag("Area")) return;

        // # 플레이어 좌표 저장
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // # 나의 좌표 저장 
        Vector3 myPos = transform.position;

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        // # X축과 Y축 각각의 거리 
        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        // # 이동 방향
        dirX = dirX > -0.01f ? 1 : -1;
        dirY = dirY > -0.01f ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    // 수평이동
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy":
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3.0f, 3.0f),0.0f));
                }
                break;
        }
    }
}
