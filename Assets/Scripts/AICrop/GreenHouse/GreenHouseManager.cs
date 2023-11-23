using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHouseManager : SingletonMono<GreenHouseManager>
{
    public bool isReset;

    public GreenHouseInfoSO greenHouseInfoSO;
    public List<GameObject> cropStepPrefabs;

    private int day;
    private int step;
    private bool isStepChanged;
    private List<GameObject> crops;
    private Transform tr;

    // Start is called before the first frame update
    private void Start()
    {
        tr = transform;
        LoadGreenHouseInfoSO();
        AIClient.Instance.dispatchPrediction = HandlePrediction;
    }

    private void Update()
    {
        // 작물 step이 변경된 경우 작물 모델링 교체
        if (isStepChanged)
        {
            while (crops.Count > 0)
            {
                GameObject crop = crops[0];
                crops.RemoveAt(0);
                Destroy(crop);
            }

            foreach (Node node in GreenHouseGrid.Instance.Grid)
            {
                crops.Add(Instantiate(cropStepPrefabs[step], node.worldPosition, Quaternion.identity, tr));
            }

            isStepChanged = false;
            step = -1;
        }
    }

    public void LoadGreenHouseInfoSO()
    {
        if (isReset)
        {
            greenHouseInfoSO.predictDates.Clear();
            greenHouseInfoSO.predictValues.Clear();
        }

        isStepChanged = false;
        crops = new List<GameObject>();
        ProcessCropGrowth();
    }

    public void HandlePrediction(GreenHouseInfo greenHouseInfo)
    {
        greenHouseInfoSO.predictDates.Add(greenHouseInfo.predictDate);
        greenHouseInfoSO.predictValues.Add(greenHouseInfo.predictValue);

        ProcessCropGrowth();
    }

    public void ProcessCropGrowth()
    {
        day = greenHouseInfoSO.predictValues.Count;
        step = -1;
        int latestIndex = day - 1;

        // LoadGreenHouseInfoSO()에서 정보를 초기화하는 경우 
        if (latestIndex < 0)
            return;

        float latestPrediction = greenHouseInfoSO.predictValues[latestIndex];
        bool isPositive = true;

        // 예측 수확량 추이 확인
        if (latestIndex > 0)
        {
            float beforePrediction = greenHouseInfoSO.predictValues[latestIndex - 1];
            if (beforePrediction > latestPrediction)
                isPositive = false;
        }

        // 추이에 따라 Good/Bad 상태 UI 교체

        // day에 따라 현재 작물 step 설정
        switch (day)
        {
            case 1:
                step = 0;
                break;

            case 3:
                step = 1;
                break;

            case 5:
                step = 2;
                break;

            case 8:
                step = 3;
                break;

            case 10:
                step = 4;
                break;
        }

        if (step >= 0)
        {
            isStepChanged = true;
        }
    }
}
