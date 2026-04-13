using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class AutoLoadScene : MonoBehaviour
{
    public string nextSceneName = "VR_GAME";
    public float waitTime = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextSceneName);
    }
}