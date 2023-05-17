using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HurtOverlay : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort eventPort;
    [SerializeField]
    private Health playerHealth;

    [SerializeField]
    private float fadeInDuration;
    [SerializeField]
    private float fadeOutDuration;
    [SerializeField]
    private float alphaBaseValue;
    [SerializeField]
    private float alphaPeakValue;

    private Image image;
    private float timeSinceBeat;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        eventPort.onBeat += OnBeat;
    }

    void Update()
    {
        timeSinceBeat += Time.deltaTime;
        if (timeSinceBeat >= eventPort.TimeBetweenBeats - fadeInDuration)
        {
            timeSinceBeat = 0f;
            OnBeatPulse();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            image.DOFade(alphaPeakValue, fadeInDuration).OnComplete(() =>
            {
                image.DOFade(alphaBaseValue, fadeOutDuration);
            });
        }
    }

    private void OnBeat()
    {
        timeSinceBeat = 0f;
    }

    private void OnBeatPulse()
    {
        if(playerHealth.CurrentHealth < playerHealth.CurrentMaxHealth * 0.4)
        {
            image.DOFade(alphaPeakValue, fadeInDuration).OnComplete(() =>
            {
                image.DOFade(alphaBaseValue, fadeOutDuration);
            });
        }

        else if(playerHealth.CurrentHealth >= playerHealth.CurrentMaxHealth * 0.4)
        {
            image.DOFade(0f, fadeOutDuration);
        }
    }

}
