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
        // �۹� step�� ����� ��� �۹� �𵨸� ��ü
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

        // LoadGreenHouseInfoSO()���� ������ �ʱ�ȭ�ϴ� ��� 
        if (latestIndex < 0)
            return;

        float latestPrediction = greenHouseInfoSO.predictValues[latestIndex];
        bool isPositive = true;

        // ���� ��Ȯ�� ���� Ȯ��
        if (latestIndex > 0)
        {
            float beforePrediction = greenHouseInfoSO.predictValues[latestIndex - 1];
            if (beforePrediction > latestPrediction)
                isPositive = false;
        }

        // ���̿� ���� Good/Bad ���� UI ��ü

        // day�� ���� ���� �۹� step ����
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
