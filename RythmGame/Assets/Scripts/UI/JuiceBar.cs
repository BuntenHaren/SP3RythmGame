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
    [SerializeField]
    private float juiceDrainPerSecond;
    [SerializeField]
    private float timeBeforeDrain;

    [HideInInspector]
    public bool startDrain = false;

    private Timer drainTimer;

    private void Start()
    {
        drainTimer = new Timer();
        drainTimer.TimerDone += () => startDrain = true;

        if (slider == null)
            slider = GetComponent<Slider>();
        
        slider.maxValue = juiceCounter.MaxJuice;
        juiceCounter.onChange += ChangeSliderValue;
        slider.value = juiceCounter.CurrentJuice;
    }

    void Update()
    {
        drainTimer.UpdateTimer(Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if (startDrain == true && juiceCounter.CurrentJuice > 0f)
        {
            juiceCounter.CurrentJuice -= juiceDrainPerSecond * Time.deltaTime;
            if (juiceCounter.CurrentJuice < 0f)
                juiceCounter.CurrentJuice = 0f;
        }
    }

    private void ChangeSliderValue(float amount)
    {
        var changeTo = slider.value + amount;
        if (amount > 0.2f)
        {
            DOTween.To(() => slider.value, x => slider.value = x, changeTo, sliderUpdateTime).SetEase(Ease.OutElastic).OnComplete(() =>
            {
                startDrain = false;
                drainTimer.StartTimer(timeBeforeDrain);
            });
        }
        else
        {
            slider.value = changeTo;
        }
    }
    
}
