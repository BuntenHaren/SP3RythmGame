using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JuiceBar : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;

    [Header("JuiceBar")]
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

    [Header("Glow")]
    [SerializeField]
    private Image glowImage;
    [SerializeField]
    private float glowFadeInTime;
    [SerializeField]
    private float glowFadeOutTime;
    [SerializeField]
    private float glowFadeTo;

    //Private
    private Timer drainTimer;
    private bool glow = false;
    private float timeSinceBeat;

    private void Start()
    {
        eventPort.onBeat += OnBeat;
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

        timeSinceBeat += Time.deltaTime;
        if (timeSinceBeat >= eventPort.TimeBetweenBeats - glowFadeInTime)
        {
            timeSinceBeat = 0f;
            GlowPulse();
        }
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
            drainTimer.StartTimer(timeBeforeDrain);
        }
    }

    public void ActivateGlow()
    {
        glowImage.DOFade(glowFadeTo, 0.5f);
        glow = true;
    }

    public void RemoveGlow()
    {
        glow = false;
        glowImage.DOFade(0f, 0.5f);
    }

    private void OnBeat()
    {
        timeSinceBeat = 0f;
    }

    private void GlowPulse()
    {
        if(glow)
        {
            glowImage.DOFade(1f, glowFadeInTime).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                glowImage.DOFade(glowFadeTo, glowFadeOutTime).SetEase(Ease.InOutSine);
            });
        }
    }

}
