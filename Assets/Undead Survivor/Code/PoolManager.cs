using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // # �����յ��� ������ ����
    public GameObject[] prefabs;

    // # Ǯ ����� �ϴ� ����Ʈ�� 
    public List<GameObject>[] pools;

    private void Awake()
    {
        // # Ǯ ���� ����� 
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

        // # ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ��) ���� ������Ʈ ����
        
            // # �߰��ϸ� select ������ �Ҵ�

        // # �� ã������ 
            // ���Ӱ� �����ϰ� select

        return select;
    }
}
