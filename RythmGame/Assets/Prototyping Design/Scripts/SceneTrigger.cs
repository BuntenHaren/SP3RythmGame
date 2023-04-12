using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private SceneChanger sceneChanger;
    [SerializeField]
    private string scene;

    void Awake()
    {
        sceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChanger>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            sceneChanger.ChangeScene(scene);
        }
    }
}
