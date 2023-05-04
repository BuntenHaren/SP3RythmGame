using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;

    [SerializeField] 
    private Health playerHealth;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    private float scaleUpTime;
    [SerializeField]
    private float scaleDownTime;
    [SerializeField]
    private Vector3 scaleTo;

    private void Start()
    {
        SetMaxHealth(playerHealth.CurrentMaxHealth);
        eventPort.onBeat += HealthBarBeatAnimation;
    }

    private void OnEnable()
    {
        playerHealth.onChange += SetHealth;
    }

    private void OnDisable()
    {
        playerHealth.onChange -= SetHealth;
    }

    private void SetMaxHealth(float health)
    {
        slider.maxValue = playerHealth.CurrentMaxHealth;
        slider.value = playerHealth.CurrentHealth;
    }

    private void SetHealth(float health)
    {
        slider.value = playerHealth.CurrentHealth;
    }

    private void HealthBarBeatAnimation()
    {
        transform.DOScale(scaleTo, scaleUpTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), scaleDownTime).SetEase(Ease.InOutSine);
        });
    }

}
