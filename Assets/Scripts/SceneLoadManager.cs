using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject doorPanel;

    public Animator crossFade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorPanel.SetActive(true);
            if(TimeManager.Instance.Day > 1 || TimeManager.Instance.HourOfTime() > 7) Grid.Instance.ObjActive();
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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LoadScene("HouseOutScene");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            LoadScene("Scene_HouseIn");
        }
    }

    IEnumerator LoadSceneCoroutine(string name)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);

        // 씬이 완전히 로드될 때까지 대기
        while (!async.isDone)
            yield return new WaitForEndOfFrame();
        // 씬이 로드된 다음 ActiveScene으로 설정
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
    }

    public void UnloadScene(string name)
    {
        StartCoroutine(TriggerCrossFadeCoroutine());

        SceneManager.UnloadSceneAsync(name);

    }

    IEnumerator TriggerCrossFadeCoroutine()
    {
        crossFade.SetTrigger("Start");
        yield break;
    }


}