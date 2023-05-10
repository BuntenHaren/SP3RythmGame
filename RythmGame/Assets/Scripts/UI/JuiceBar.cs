using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JuiceBar : MonoBehaviour
{
    [SerializeField]
    private JuiceCounter juiceCounter;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float sliderUpdateTime;

    private void Start()
    {
        if(slider == null)
            slider = GetComponent<Slider>();
        
        slider.maxValue = juiceCounter.MaxJuice;
        juiceCounter.onChange += ChangeSliderValue;
    }

    private void ChangeSliderValue(float amount)
    {
        var changeTo = slider.value + amount;
        DOTween.To(() => slider.value, x => slider.value = x, changeTo, sliderUpdateTime).SetEase(Ease.OutElastic);
    }
    
}
