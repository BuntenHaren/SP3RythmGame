using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField]
    private MusicEventPort eventPort;

    [SerializeField] 
    private Health playerHealth;
>>>>>>> Stashed changes
    [SerializeField]
    public Slider slider;
    [SerializeField]
    private float scaleUpTime;
    [SerializeField]
    private float scaleDownTime;
    [SerializeField]
    private Vector3 scaleTo;

<<<<<<< Updated upstream
    public void SetMaxHealth(float health)
=======
    private void Start()
    {
        SetMaxHealth(playerHealth.MaxHealth);

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
>>>>>>> Stashed changes
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    private void HealthBarBeatAnimation()
    {
        transform.DOScale(scaleTo, scaleUpTime).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), scaleDownTime).SetEase(Ease.InOutSine);
        });
    }
}
