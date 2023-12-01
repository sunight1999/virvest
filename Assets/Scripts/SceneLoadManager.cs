using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject doorPanel;
    public FadeScreen fadeScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OffDoorPanel();
        }
    }

    public void OffDoorPanel()
    {
        doorPanel.SetActive(false);
    }

    public void LoadScene(string name)
    {
        //StartCoroutine(TriggerCrossFadeCoroutine());
        StartCoroutine(LoadSceneCoroutine(name));
    }

    public void CheckSceneChange()
    {
        if (!TimeManager.Instance.IsFirst()) Grid.Instance.ObjActive();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadScene("HouseOutScene");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            LoadScene("Scene_HouseIn");
        }
    }

    public void SleepingLoadScene()
    {
        TimeManager.Instance.UpdateDay();
        LoadScene("Scene_HouseIn");
    }

    IEnumerator LoadSceneCoroutine(string name)
    {
        //fadeScreen.FadeOut();
        AsyncOperation async = SceneManager.LoadSceneAsync(name);

        // 씬이 완전히 로드될 때까지 대기
        while (!async.isDone)
            yield return new WaitForEndOfFrame();
        // 씬이 로드된 다음 ActiveScene으로 설정
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
    }
}