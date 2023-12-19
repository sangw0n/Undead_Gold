using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class PoolManager : MonoBehaviour
{
    // # �����յ��� ������ ����
    public GameObject[] prefabs;

    // # Ǯ ����� �ϴ� ����Ʈ�� 
    private List<GameObject>[] pools;

    private void Awake()
    {
        // # Ǯ ���� ����� 
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // GameObject �� ��ȯ�ϴ� �Լ� 
    // ������ ������Ʈ�� �����ϴ� �Ű�����
    public GameObject Get(int index)
    {
        GameObject select = null;

        // # ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ��) ���� ������Ʈ ����
            // # �߰��ϸ� select ������ �Ҵ�

        foreach (GameObject item in pools[index])
        {
            // Ȱ��ȭ ����
            if(!item.activeSelf)
            {
                /* ��Ȱ��ȭ ���¶�� select �� item �� �ְ� ���� �����ش�*/
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // # �� ã������ 
        if(!select)
        {
            // ���Ӱ� �����ϰ� select
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);                                               
        }

        return select;
    }
}
