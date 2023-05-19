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

    [SerializeField]
    private float healthUpdateTime;

    private float timeSinceBeat;
    private Vector3 originalSize;
    private bool heartReady;

    private void Start()
    {
        SetMaxHealth(playerHealth.CurrentMaxHealth);
        
        SetHealth(playerHealth.BaseMaxHealth);
        originalSize = transform.localScale;
    }

    void Update()
    {
        timeSinceBeat += Time.deltaTime;
        if(eventPort.GetDistanceToNextBeat() <= scaleUpTime && heartReady)
        {
            HealthBarBeatAnimation();
            heartReady = false;
        }
    }

    private void OnEnable()
    {
        playerHealth.onChange += SetHealth;
        eventPort.onBeat += OnBeat;
    }

    private void OnDisable()
    {
        playerHealth.onChange -= SetHealth;
        eventPort.onBeat -= OnBeat;
    }

    private void SetMaxHealth(float health)
    {
        slider.maxValue = playerHealth.CurrentMaxHealth;
        slider.value = playerHealth.CurrentHealth;
    }

    private void SetHealth(float health)
    {
        DOTween.To(() => slider.value, x => slider.value = x, playerHealth.CurrentHealth, healthUpdateTime);
    }

    private void OnBeat()
    {
        //HealthBarBeatAnimation();
        heartReady = true;
    }

    private void HealthBarBeatAnimation()
    {
        transform.DOScale(scaleTo, scaleUpTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.DOScale(originalSize, scaleDownTime).SetEase(Ease.InOutSine);
        });
    }

}
