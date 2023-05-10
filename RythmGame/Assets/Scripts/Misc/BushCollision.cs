using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BushCollision : MonoBehaviour
{
    private Transform childTransform;

    [SerializeField]
    private Vector3 scalePunch;
    [SerializeField]
    private float positiveRotationPunch;
    [SerializeField]
    private float negativeRotationPunch;
    [SerializeField]
    private float duration;
    [SerializeField]
    private int vibrato;
    [SerializeField]
    private float elasticity;

    void Start()
    {
        childTransform = gameObject.transform.GetChild(0);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            BushWiggle();
        }
    }

    private void BushWiggle()
    {
        var rotation = new Vector3(0f, 0f, Random.Range(positiveRotationPunch, negativeRotationPunch));
        childTransform.DOPunchScale(scalePunch, duration, vibrato, elasticity);
        childTransform.DOPunchRotation(rotation, duration, vibrato, elasticity);
    }
}
