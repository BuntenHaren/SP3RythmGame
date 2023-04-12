using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName, Vector3 spawnPoint)
    {
        SceneManager.LoadScene(sceneName);
        player.position = spawnPoint;
    }
}
