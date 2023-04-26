using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] 
    private Health playerHealth;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private Vector3 scaleTo;

    private void Start()
    {
        SetMaxHealth(playerHealth.MaxHealth);
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
        slider.maxValue = health;
        slider.value = health;
    }

    private void SetHealth(float health)
    {
        slider.value = health;
        HealthBarAnimation();
    }

    private void HealthBarAnimation()
    {
        transform.DOScale(scaleTo, animationTime).SetEase(Ease.InOutBounce).OnComplete(ResetTransformAnimation);
    }

    private void ResetTransformAnimation()
    {
        transform.DOScale(new Vector3 (1f, 1f, 1f), animationTime).SetEase(Ease.InOutBounce);
    }
}
