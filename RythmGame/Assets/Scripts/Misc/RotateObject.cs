using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetRotation;
    [SerializeField]
    private float duration;

    public void StartRotation()
    {
        transform.DORotate(targetRotation, duration);
    }
}
