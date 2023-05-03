using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceBar : MonoBehaviour
{
    [SerializeField]
    private JuiceCounter juiceCounter;
    [SerializeField]
    private Slider slider;
    
    private void Start()
    {
        if(slider == null)
            slider = GetComponent<Slider>();
        
        slider.maxValue = juiceCounter.MaxJuice;
        juiceCounter.onChange += ChangeSliderValue;
    }

    private void ChangeSliderValue(float amount)
    {
        slider.value += amount;
    }
    
}
