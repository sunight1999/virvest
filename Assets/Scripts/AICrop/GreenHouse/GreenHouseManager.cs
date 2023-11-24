using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropStatus
{
    public GameObject goodUI;
    public GameObject badUI;

    public CropStatus(GameObject goodUI, GameObject badUI)
    {
        this.goodUI = goodUI;
        this.badUI = badUI;

        Hide();
    }

    public void SetStatus(bool isGood)
    {
        if (isGood)
        {
            goodUI.SetActive(true);
            badUI.SetActive(false);
        }
        else
        {
            goodUI.SetActive(false);
            badUI.SetActive(true);
        }
    }

    public void Hide()
    {
        goodUI.SetActive(false);
        badUI.SetActive(false);
    }
}

public class GreenHouseManager : SingletonMono<GreenHouseManager>
{
    public bool isReset;

    public GreenHouseInfoSO greenHouseInfoSO;
    public List<GameObject> cropStepPrefabs;
    public GameObject goodStatusUIPrefab;
    public GameObject badStatusUIPrefab;

    private int day;
    private int step;
    private bool isStepChanged;
    private bool isStatusChanged;
    private bool isPositive;
    private List<GameObject> crops;
    private CropStatus[,] cropStatuses;
    private Transform tr;

    private float finalPrediction;

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

        if (isStatusChanged)
        {
            foreach (CropStatus cropStatus in cropStatuses)
            {
                cropStatus.SetStatus(isPositive);
            }

            isStatusChanged = false;
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
        cropStatuses = new CropStatus[(int)GreenHouseGrid.Instance.gridWorldSize.x, (int)GreenHouseGrid.Instance.gridWorldSize.y];
        for (int i = 0; i < (int)GreenHouseGrid.Instance.gridWorldSize.x; i++)
        {
            for (int j = 0; j < (int)GreenHouseGrid.Instance.gridWorldSize.y; j++)
            {
                
                Node node = GreenHouseGrid.Instance.Grid[i, j];
                GameObject goodUI = Instantiate(goodStatusUIPrefab, node.worldPosition + Vector3.up * 1.1f, Quaternion.identity, tr);
                GameObject badUI = Instantiate(badStatusUIPrefab, node.worldPosition + Vector3.up * 1.1f, Quaternion.identity, tr);
                cropStatuses[i, j] = new CropStatus(goodUI, badUI);
            }
        }

        ProcessCropGrowth(true);
    }

    public void HandlePrediction(GreenHouseInfo greenHouseInfo)
    {
        greenHouseInfoSO.predictDates.Add(greenHouseInfo.predictDate);
        greenHouseInfoSO.predictValues.Add(greenHouseInfo.predictValue);

        ProcessCropGrowth();
    }

    public void ProcessCropGrowth(bool isDataLoading = false)
    {
        day = greenHouseInfoSO.predictValues.Count;
        int latestIndex = day - 1;

        // LoadGreenHouseInfoSO()에서 정보를 초기화하는 경우 
        if (latestIndex < 0)
            return;

        float latestPrediction = greenHouseInfoSO.predictValues[latestIndex];
        isPositive = true;

        // 예측 수확량 추이 확인
        if (latestIndex > 0)
        {
            float beforePrediction = greenHouseInfoSO.predictValues[latestIndex - 1];
            if (beforePrediction > latestPrediction)
                isPositive = false;
        }

        // 추이에 따라 Good/Bad 상태 UI 교체
        isStatusChanged = true;

        // day에 따라 현재 작물 step 설정
        if (isDataLoading)
            FindStepFromDayRange(day);
        else
            FindStepFromDay(day);
    }

    public void FindStepFromDay(int day)
    {
        step = -1;

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

    public void FindStepFromDayRange(int day)
    {
        step = -1;

        if (day >= 0 && day <= 1)
            step = 0;
        else if (day <= 3)
            step = 1;
        else if (day <= 5)
            step = 2;
        else if (day <= 8)
            step = 3;
        else if (day <= 10)
            step = 4;
        else
            step = -1;

        if (step >= 0)
        {
            isStepChanged = true;
        }
    }
}
