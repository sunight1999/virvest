using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingGrowth : MonoBehaviour
{
    [Tooltip("�� ������ ���� 5�ܰ踦 ���� ����Ʈ�� �Ҵ��� ��. �Ǻ����� ������ ��.")]
    [SerializeField] private List<GameObject> seedingStep;

    private int seedDay = 1;


    public void UpdateSeeding()
    {
        seedDay++;
        if (seedDay == 3 || seedDay == 5 || seedDay == 8 || seedDay == 10) //�� 3���� 5���� 8���� 10������ ���� �ܰ躰�� ������Ʈ ����
        {
            for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
            seedingStep.RemoveAt(0);
            Instantiate(seedingStep[0], transform.position, Quaternion.identity, transform).SetActive(false);
        }
    }

}
