using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneChanger sceneChanger;
    [SerializeField]
    private string scene;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            sceneChanger.ChangeScene(scene);
        }
    }
}
