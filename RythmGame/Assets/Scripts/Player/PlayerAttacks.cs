using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    [Header("Base Attack Values")]
    [SerializeField]
    private float baseAttackDistance = 1;
    [SerializeField]
    private int baseAttackDamage = 1;
    [SerializeField]
    private float baseAttackRate = 1;
    [SerializeField]
    private float baseAttackRadius = 1;
    
    //Current variables which is used for everything
    private int currentAttackDamage;
    private float currentAttackDistance;
    private float currentAttackRate;
    private float currentAttackRadius;
    private Timer attackCooldownTimer;
    private bool readyToAttack = true;

    private Vector3 finalPoint;
    private Camera cam;
    
    private void Start()
    {
        currentAttackDamage = baseAttackDamage;
        currentAttackDistance = baseAttackDistance;
        currentAttackRate = baseAttackRate;
        currentAttackRadius = baseAttackRadius;
        
        attackCooldownTimer = new Timer();
        attackCooldownTimer.TimerDone += () => readyToAttack = true;
        cam = Camera.main;
    }

    private void OnAttack()
    {
        if(!readyToAttack)
            return;
        
        attackCooldownTimer.StartTimer(currentAttackRate);
        readyToAttack = false;
        Vector2 mousePosOnScreen = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosOnScreen.x, mousePosOnScreen.y, transform.position.y));
        
        //Honestly I don't care enough to figure out exactly how the math on this works right now, but it works so I'm gonna use it
        //Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
        float t = cam.transform.position.y / (cam.transform.position.y - mousePos.y);
        finalPoint = new Vector3(t * (mousePos.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (mousePos.z - cam.transform.position.z) + cam.transform.position.z);
        Collider[] potentialHits = Physics.OverlapSphere(transform.position + 
                                                         (finalPoint - transform.position).normalized * currentAttackDistance, currentAttackRadius);
        
        //So we don't damage ourselves accidentally
        HashSet<IDamageable> selfDamageables = new HashSet<IDamageable>();
        foreach(var self in GetComponents<IDamageable>())
        {
            selfDamageables.Add(self);
        }
        
        //Hit everything
        foreach(var potHit in potentialHits)
        {
            if(potHit.TryGetComponent<IDamageable>(out IDamageable hit))
            {
                if(selfDamageables.Contains(hit))
                    continue;
                hit.TakeDamage(currentAttackDamage);
            }
        }
    }

    private void FixedUpdate()
    {
        attackCooldownTimer.UpdateTimer(Time.fixedDeltaTime);
    }
    
}
