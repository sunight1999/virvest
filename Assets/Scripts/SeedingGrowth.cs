using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SeedingGrowth : SingletonMono<SeedingGrowth>
{
    [Tooltip("�� ������ ���� 5�ܰ踦 ���� ����Ʈ�� �Ҵ��� ��. �Ǻ����� ������ ��.")]
    [SerializeField] private List<GameObject> seedingStep;

    public void UpdateSeeding()
    {
        //if(TimeManager.Instance.Day == 3 || TimeManager.Instance.Day == 5 || //�� 3���� 5���� 8���� 10������ ���� �ܰ躰�� ������Ʈ ����
        //   TimeManager.Instance.Day == 8 || TimeManager.Instance.Day == 10)
        //{
        //    for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
        //    seedingStep.RemoveAt(0);
        //    Instantiate(seedingStep[0], transform.position, Quaternion.identity, transform).SetActive(false);
        //}
        
    }

}
