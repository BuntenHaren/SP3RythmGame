using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private Vector3 scaleTo;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        HealthBarAnimation();
    }

    private void HealthBarAnimation()
    {
        transform.DOScale(scaleTo, animationTime).OnComplete(ResetTransformAnimation);
    }

    private void ResetTransformAnimation()
    {
        transform.DOScale(new Vector3 (1f, 1f, 1f), animationTime);
    }
}
