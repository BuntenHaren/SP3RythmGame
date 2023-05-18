using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;
    [SerializeField]
    private float duration;

    public void StartMove()
    {
        transform.DOLocalMove(targetPos, duration);
    }
}
