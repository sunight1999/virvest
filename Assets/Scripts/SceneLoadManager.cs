using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    public Animator crossFade;
    public GameObject[] mainSceneActivationTargets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                LoadScene("Scene_HouseIn");
            }
            //else if (SceneManager.GetActiveScene().buildIndex == 1)
            //{
            //    LoadScene("Scene_HouseOut");
            //    TimeManager.Instance.StartTime();
            //}
        }
    }

    public void LoadScene(string name, bool isAdditive = false)
    {
        //StartCoroutine(TriggerCrossFadeCoroutine());

        StartCoroutine(LoadSceneCoroutine(name));
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

        ToggleStatus();
    }

    IEnumerator TriggerCrossFadeCoroutine()
    {
        crossFade.SetTrigger("Start");
        yield break;
    }

    void ToggleStatus()
    {
        foreach (GameObject obj in mainSceneActivationTargets)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
    
}
