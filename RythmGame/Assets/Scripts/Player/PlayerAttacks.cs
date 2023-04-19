using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]
    private MusicEventPort musicEventPort;
    
    [Header("Base Attack Values")]
    [SerializeField]
    private float baseAttackDistance = 1;
    [SerializeField]
    private int baseAttackDamage = 1;
    [SerializeField]
    private float baseAttackRate = 1;
    [SerializeField]
    private float baseAttackRadius = 1;
    [SerializeField]
    private double timeForBeatWindow;
    
    [Header("Animation and VFX")]
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private VisualEffect attackVFX;

    [Header("SFX")] 
    [SerializeField]
    private AudioClip normalAttackSFX;
    [SerializeField]
    private AudioClip onBeatAttackSFX;
    
    //Current variables which is used for attacks
    private int currentAttackDamage;
    private float currentAttackDistance;
    private float currentAttackRate;
    private float currentAttackRadius;
    
    //Other private variables
    private Timer attackCooldownTimer;
    private bool readyToAttack = true;
    private double lastBeatTime;
    private double timeBetweenBeats;
    private AudioClip attackSFXtoPlay;
    private AudioSource audio;
    private Vector3 finalPoint;
    private Camera cam;
    
    private void Start()
    {
        musicEventPort.onBeat += OnBeat;

        audio = GetComponent<AudioSource>();
        cam = Camera.main;
        
        currentAttackDamage = baseAttackDamage;
        currentAttackDistance = baseAttackDistance;
        currentAttackRate = baseAttackRate;
        currentAttackRadius = baseAttackRadius;
        
        attackCooldownTimer = new Timer();
        attackCooldownTimer.TimerDone += () => readyToAttack = true;
    }

    private void OnAttack()
    {
        if(!readyToAttack)
            return;

        attackSFXtoPlay = normalAttackSFX;
        
        if(CheckIfWithinBeatTimeframe())
        {
            ApplyOnBeatEffects();
            Debug.Log("Attack was on beat!");
        }
        
        //Start setting values and playing stuff for the attack like audio, animation, VFX etc.
        audio.PlayOneShot(attackSFXtoPlay);
        attackVFX.Play();
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

    private void OnBeat()
    {
        timeBetweenBeats = Time.realtimeSinceStartupAsDouble - lastBeatTime;
        lastBeatTime = Time.realtimeSinceStartupAsDouble;
    }

    private void ApplyOnBeatEffects()
    {
        attackSFXtoPlay = onBeatAttackSFX;
    }

    private bool CheckIfWithinBeatTimeframe()
    {
        return Time.realtimeSinceStartupAsDouble <= lastBeatTime + timeForBeatWindow * 0.5f || Time.realtimeSinceStartupAsDouble >= lastBeatTime + timeBetweenBeats - timeForBeatWindow * 0.5f;
    }
    
    private void FixedUpdate()
    {
        attackCooldownTimer.UpdateTimer(Time.fixedDeltaTime);
    }
    
}
