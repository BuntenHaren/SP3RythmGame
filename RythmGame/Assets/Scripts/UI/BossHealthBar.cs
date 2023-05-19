using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHealthBar : MonoBehaviour
{

    [SerializeField]
    private Health bossHealth;
    [SerializeField]
    public Slider slider;

    [SerializeField]
    private float healthUpdateTime;

    private void Start()
    {
        SetMaxHealth(bossHealth.CurrentMaxHealth);
        SetHealth(bossHealth.BaseMaxHealth);
    }

    private void OnEnable()
    {
        bossHealth.onChange += SetHealth;
    }

    private void OnDisable()
    {
        bossHealth.onChange -= SetHealth;
    }

    private void SetMaxHealth(float health)
    {
        slider.maxValue = bossHealth.CurrentMaxHealth;
        slider.value = bossHealth.CurrentHealth;
    }

    private void SetHealth(float health)
    {
        DOTween.To(() => slider.value, x => slider.value = x, bossHealth.CurrentHealth, healthUpdateTime);
    }
}
