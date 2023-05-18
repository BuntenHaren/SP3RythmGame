using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField]
    private string scene;

    private Image sceneChangeFade;

    void Start()
    {
        sceneChangeFade = GameObject.Find("SceneChangeFade").GetComponent<Image>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            sceneChangeFade.DOFade(1f, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                SceneManager.LoadScene(scene);
            });
        }
    }
}
