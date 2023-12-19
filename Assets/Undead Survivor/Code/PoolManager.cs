using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // # 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // # 풀 담당을 하는 리스트들 
    public List<GameObject>[] pools;

    private void Awake()
    {
        // # 풀 공간 만들기 
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // # 선택한 풀의 놀고 있는(비활성화된) 게임 오브젝트 접근
        
            // # 발견하면 select 변수에 할당

        // # 못 찾았으면 
            // 새롭게 생성하고 select

        return select;
    }
}
