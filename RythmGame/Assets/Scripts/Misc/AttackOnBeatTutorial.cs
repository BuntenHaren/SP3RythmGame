using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackOnBeatTutorial : MonoBehaviour, IDamageable
{
    [SerializeField]
    private MusicEventPort musicEventPort;
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

    private SpriteRenderer sr;

    void OnDisable()
    {
        musicEventPort.onBeat -= OnBeat;
    }

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        musicEventPort.onBeat += OnBeat;
    }

    public void TakeDamage(float damage)
    {

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
        transform.DOScale(scaleTo, scaleUpTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.DOScale(originalScale, scaleDownTime).SetEase(Ease.InOutSine);
        });
    }

}
