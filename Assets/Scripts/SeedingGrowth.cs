using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SeedingGrowth : SingletonMono<SeedingGrowth>
{
    [Tooltip("각 모종의 성장 5단계를 다음 리스트에 할당할 것. 피봇까지 검토할 것.")]
    [SerializeField] private List<GameObject> seedingStep;

    public void UpdateSeeding()
    {
        //if(TimeManager.Instance.Day == 3 || TimeManager.Instance.Day == 5 || //총 3일차 5일차 8일차 10일차로 각각 단계별로 오브젝트 변경
        //   TimeManager.Instance.Day == 8 || TimeManager.Instance.Day == 10)
        //{
        //    for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
        //    seedingStep.RemoveAt(0);
        //    Instantiate(seedingStep[0], transform.position, Quaternion.identity, transform).SetActive(false);
        //}
        
    }

}
