using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenGate : MonoBehaviour
{
    [SerializeField]
    private GameObject Gate1;
    [SerializeField]
    private GameObject Gate2;
    [SerializeField]
    private Transform gateTarget1;
    [SerializeField]
    private Transform gateTarget2;
    private int counter;

    public void MoveGate()
    {
        if(counter == 0)
        {
            Gate1.transform.DOMove(gateTarget1.position, 1f, false);
        }
        if(counter == 1)
        {
            Gate2.transform.DOMove(gateTarget2.position, 1f, false);
        }
        counter++;
    }
}
