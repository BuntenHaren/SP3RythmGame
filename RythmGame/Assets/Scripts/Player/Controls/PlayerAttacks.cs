using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttacks : MonoBehaviour
{
    public static UnityAction onPlayerAttackAction = delegate {};

    [Header("Event Ports and Counters")]
    [SerializeField]
    private MusicEventPort musicEventPort;
    [SerializeField]
    private JuiceCounter juiceCounter;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private Health playerHealth;
    
    [Header("Animation and VFX")]
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private VisualEffect attackVFX;

    [Header("SFX")]
    [SerializeField]
    private EventReference PlayerAttack;
    [SerializeField]
    private EventReference onBeatPlayerAttack;
    
    //Other private variables
    private Timer attackCooldownTimer;
    private bool readyToAttack = true;
    private double lastBeatTime;
    private double timeBetweenBeats;
    private EventReference actualAttackSFX;
    private Vector3 finalPoint;
    private Camera cam;
    private Animator currentDirectionAnimator;

    private void Start()
    {
        musicEventPort.onBeat += OnBeat;
        cam = Camera.main;

        attackCooldownTimer = new Timer();
        attackCooldownTimer.TimerDone += AttackOffCooldown;
    }

    private void OnAttack()
    {
        if (!readyToAttack)
            return;

        SetCurrentlyActiveAnimator();
        actualAttackSFX = PlayerAttack;
        currentDirectionAnimator.SetBool("Attack", true);
        
        //This is temporary testing
        onPlayerAttackAction.Invoke();

        if(CheckIfWithinBeatTimeframe())
        {
            ApplyOnBeatEffects();
        }
        
        ActivateAttackStuff();
        HitEverythingInRange();
        
    }

    private void SetCurrentlyActiveAnimator()
    {
        foreach (var anim in GetComponentsInChildren<Animator>())
        {
            if (anim != playerAnimator)
            {
                currentDirectionAnimator = anim;
                return;
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
        actualAttackSFX = onBeatPlayerAttack;
    }

    private void AttackOffCooldown()
    {
        currentDirectionAnimator.SetBool("Attack", false);
        readyToAttack = true;
    }

    private void ActivateAttackStuff()
    {
        //Start setting values and playing stuff for the attack like audio, animation, VFX etc.
        RuntimeManager.PlayOneShot(actualAttackSFX);
        attackCooldownTimer.StartTimer(playerStats.CurrentAttackRate * playerStats.CurrentAttackRateMultiplier);
        readyToAttack = false;
    }

    private bool CheckIfWithinBeatTimeframe()
    {
        return Time.realtimeSinceStartupAsDouble <= lastBeatTime + playerStats.CurrentTimeForBeatWindow * 0.5f || Time.realtimeSinceStartupAsDouble >= lastBeatTime + timeBetweenBeats - playerStats.CurrentTimeForBeatWindow * 0.5f;
    }

    private void HitEverythingInRange()
    {
        Vector2 mousePosOnScreen = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosOnScreen.x, mousePosOnScreen.y, transform.position.y));
        
        //Honestly I don't care enough to figure out exactly how the math on this works right now, but it works so I'm gonna use it
        //Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
        float t = cam.transform.position.y / (cam.transform.position.y - mousePos.y);
        finalPoint = new Vector3(t * (mousePos.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (mousePos.z - cam.transform.position.z) + cam.transform.position.z);
        Collider[] potentialHits = Physics.OverlapSphere(transform.position + 
                                                         (finalPoint - transform.position).normalized * playerStats.CurrentAttackDistance * playerStats.CurrentAttackDistanceMultiplier, playerStats.CurrentAttackRadius * playerStats.CurrentAttackRadiusMultiplier);
        
        
        //So we don't damage ourselves accidentally
        HashSet<IDamageable> selfDamageables = new HashSet<IDamageable>();
        foreach(var self in GetComponents<IDamageable>())
        {
            selfDamageables.Add(self);
        }

        bool hitSomething = false;
        
        //Hit everything
        foreach(var potHit in potentialHits)
        {
            if(potHit.TryGetComponent<IDamageable>(out IDamageable hit))
            {
                if(selfDamageables.Contains(hit))
                    continue;
                if(CheckIfWithinBeatTimeframe())
                    hit.TakeDamageOnBeat(playerStats.CurrentAttackDamage * playerStats.CurrentAttackDamageMultiplier);
                else
                    hit.TakeDamage(playerStats.CurrentAttackDamage * playerStats.CurrentAttackDamageMultiplier);
                hitSomething = true;
            }
        }

        if(hitSomething)
        {
            juiceCounter.CurrentJuice += playerStats.CurrentJuiceAmountOnBeat * playerStats.CurrentJuiceAmountOnBeatMultiplier;
            playerHealth.CurrentHealth += playerStats.CurrentHealOnAttack * playerStats.CurrentHealOnAttackMultiplier;
        }
    }
    
    private void FixedUpdate()
    {
        attackCooldownTimer.UpdateTimer(Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + (finalPoint - transform.position).normalized * playerStats.CurrentAttackDistance * playerStats.CurrentAttackDistanceMultiplier, playerStats.CurrentAttackRadius * playerStats.CurrentAttackRadiusMultiplier);
    }
}
