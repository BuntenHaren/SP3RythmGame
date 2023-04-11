using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        Debug.Log("a");
        SceneManager.LoadScene(sceneName);
    }
}
