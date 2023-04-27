using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public Vector3 checkpoint;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName, Vector3 spawnPoint)
    {
        SceneManager.LoadScene(sceneName);
        player.position = spawnPoint;
        checkpoint = spawnPoint;
    }

    public IEnumerator ReloadCurrentScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.position = checkpoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.gameObject.GetComponent<PlayerHealth>().Spawn();
    }
}
