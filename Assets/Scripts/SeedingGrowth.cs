using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingGrowth : MonoBehaviour
{
    [Tooltip("각 모종의 성장 5단계를 다음 리스트에 할당할 것. 피봇까지 검토할 것.")]
    [SerializeField] private List<GameObject> seedingStep;

    private int seedDay = 1;


    public void UpdateSeeding()
    {
        seedDay++;
        if (seedDay == 3 || seedDay == 5 || seedDay == 8 || seedDay == 10) //총 3일차 5일차 8일차 10일차로 각각 단계별로 오브젝트 변경
        {
            for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
            seedingStep.RemoveAt(0);
            Instantiate(seedingStep[0], transform.position, Quaternion.identity, transform).SetActive(false);
        }
    }

}
