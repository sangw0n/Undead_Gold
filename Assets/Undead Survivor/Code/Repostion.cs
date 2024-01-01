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

        switch (transform.tag)
        {
            case "Ground":
                // # X축과 Y축 각각의 거리 
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                // # 이동 방향
                float dirX = diffX > 0 ? 1 : -1;
                float dirY = diffY > 0 ? 1 : -1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

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
                    Vector3 dist = playerPos - myPos;
                    Vector3 random = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(random + dist * 2);
                }
                break;
        }
    }
}
