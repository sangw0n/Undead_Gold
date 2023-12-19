using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class PoolManager : MonoBehaviour
{
    // # 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // # 풀 담당을 하는 리스트들 
    private List<GameObject>[] pools;

    private void Awake()
    {
        // # 풀 공간 만들기 
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // GameObject 를 반환하는 함수 
    // 가져올 오브젝트를 결정하는 매개변수
    public GameObject Get(int index)
    {
        GameObject select = null;

        // # 선택한 풀의 놀고 있는(비활성화된) 게임 오브젝트 접근
            // # 발견하면 select 변수에 할당

        foreach (GameObject item in pools[index])
        {
            // 활성화 상태
            if(!item.activeSelf)
            {
                /* 비활성화 상태라면 select 에 item 을 넣고 적을 보여준다*/
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // # 못 찾았으면 
        if(!select)
        {
            // 새롭게 생성하고 select
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);                                               
        }

        return select;
    }
}
