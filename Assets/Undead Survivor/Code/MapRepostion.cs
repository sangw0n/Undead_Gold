using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapRepostion : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D trigger)
    {
        if(!trigger.CompareTag("Area")) return;

        // # 플레이어 좌표 저장
        Vector3 playerPos = GameManager.instance.player.transform.position;
        // # 나의 좌표 저장 
        Vector3 myPos = transform.position;
        
        // # X축과 Y축 각각의 거리 
        float diffX  = Mathf.Abs(playerPos.x - myPos.x);
        float diffY  = Mathf.Abs(playerPos.y - myPos.y);    

        // # 플레이어 이동 방향
        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
            if(diffX >= diffY) 
            {
                // 수평이동
                transform.Translate(Vector3.right * dirX * 40);
            }
            else if (diffX <= diffY)
            {
                transform.Translate(Vector3.up * dirY * 40);
            }

            break;

            case "Enemy":

            break;
        }
    }
}
