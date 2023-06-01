using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bosses;
using Bosses.States;
using UnityEngine;

public class PieSliceTakeDamage : MonoBehaviour
{
    [SerializeField]
    private FirstPhaseStats firstPhaseStats;
    [SerializeField]
    private SecondPhaseStats secondPhaseStats;
    
    private bool useSecondPhase;
    private Timer timer;
    private bool hitOnce;

    private void OnEnable()
    {
        NextPhaseState.Enraged += delegate { useSecondPhase = true; };
        timer = new Timer();
        timer.TimerDone += () => hitOnce = false;
    }

    private void OnDisable()
    {
        NextPhaseState.Enraged -= delegate { useSecondPhase = true; };
        timer.TimerDone -= () => hitOnce = false;
    }

    private void Update()
    {
        timer.UpdateTimer(Time.deltaTime);
        LayerMask mask = LayerMask.GetMask("PieSlice");
        RaycastHit[] potentialHits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 5, mask);
        if(potentialHits.Length > 0 && !hitOnce)
        {
            if(!useSecondPhase)
                GetComponent<PlayerHealth>().TakeDamage(firstPhaseStats.PieSliceCircleDamage);
            else
                GetComponent<PlayerHealth>().TakeDamage(secondPhaseStats.PieSliceCircleDamage);
            timer.StartTimer(2.5f);
            hitOnce = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up, Vector3.down * 3);
    }
}
