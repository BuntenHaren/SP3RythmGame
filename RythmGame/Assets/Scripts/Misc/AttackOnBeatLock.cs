using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackOnBeatLock : MonoBehaviour, IDamageable
{
    [SerializeField]
    private MusicEventPort eventPort;
    [SerializeField]
    private MoveObject[] moveObjects;
    [SerializeField]
    private RotateObject[] rotateObjects;
    [SerializeField]
    private GameObject wallCollider;

    //Beat Scale
    [SerializeField]
    private Vector3 scaleTo;
    [SerializeField]
    private float scaleUpTime;
    [SerializeField]
    private float scaleDownTime;
    [SerializeField]
    private Vector3 originalScale;

    //Color
    [SerializeField]
    private Color BeatMissColor;

    private SpriteRenderer sr;

    private float timeSinceBeat;

    void Update()
    {
        timeSinceBeat += Time.deltaTime;
        if (timeSinceBeat >= eventPort.TimeBetweenBeats - scaleUpTime)
        {
            timeSinceBeat = 0f;
            BeatAnimation();
        }
    }

    void OnDisable()
    {
        eventPort.onBeat -= OnBeat;
    }

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        eventPort.onBeat += OnBeat;
    }

    public void TakeDamage(float damage)
    {
        MissBeatAttack();
    }

    public void TakeDamageOnBeat(float damage)
    {
        sr.DOFade(0f, 0.7f).OnComplete(() =>
        {
            wallCollider.SetActive(false);
            gameObject.SetActive(false);
        });
        for (int i = 0; i < moveObjects.Length; i++)
        {
            moveObjects[i].StartMove();
        }

        for (int j = 0; j < rotateObjects.Length; j++)
        {
            rotateObjects[j].StartRotation();
        }
    }

    public void HealDamage(float damage)
    {

    }

    private void OnBeat()
    {
        timeSinceBeat = 0f;
    }

    private void BeatAnimation()
    {
        transform.DOScale(scaleTo, scaleUpTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.DOScale(originalScale, scaleDownTime).SetEase(Ease.InOutSine);
        });
    }

    private void MissBeatAttack()
    {
        var originalColor = sr.color;
        sr.DOColor(BeatMissColor, scaleUpTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            sr.DOColor(originalColor, scaleDownTime).SetEase(Ease.InOutSine);
        });
    }

}
