using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float healthUpdateTime;

    void Start()
    {
        //slider = gameObject.GetComponent<Slider>();
        transform.SetParent(GameObject.Find("EnemyHealthBarCanvas").transform);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        DOTween.To(() => slider.value, x => slider.value = x, health, healthUpdateTime);
    }
}
