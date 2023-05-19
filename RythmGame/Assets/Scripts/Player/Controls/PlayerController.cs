using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using FMODUnity;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField]
    private PlayerStats playerStats;
    
    [Header("Animation and VFX")]
    [SerializeField]
    private Animator playerAnimator;

    [Header("SFX")]
    [SerializeField]
    private EventReference playerDash;
    
    //private dash variables
    private bool dashReady = true;
    private Timer dashTimer;
    
    //private movement variables
    private Rigidbody rb;
    private Vector3 newMove;
    private Vector2 moveDir;
    private Animator currentDirectionAnimator;
    private float yHeight;

    private PlayerHealth playerHealth;

    private DashCoolDownUI dashCoolDownUI;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //Fix component variables
        rb = GetComponent<Rigidbody>();
        if(TryGetComponent(out PlayerHealth h))
            playerHealth = h;
        if(TryGetComponent(out Animator a))
            playerAnimator = a;
        
        SetCurrentlyActiveAnimator();
        
        //Get some new stuff ready
        dashTimer = new Timer();
        dashTimer.TimerDone += () => dashReady = true;
        yHeight = transform.position.y;

        if (GameObject.Find("DashCoolDown") != null)
            dashCoolDownUI = GameObject.Find("DashCoolDown").GetComponent<DashCoolDownUI>();
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnDash()
    {
        Dash();
    }

    private void Update()
    {
        dashTimer.UpdateTimer(Time.fixedDeltaTime);
        //Debug.Log((playerStats.BaseDashCooldown * playerStats.CurrentDashCooldown * playerStats.BaseDashCooldownMultiplier * playerStats.CurrentDashCooldownMultiplier));
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        SetAnimations();

        //Move the character in the right direction
        newMove = playerStats.CurrentMovementSpeed * playerStats.CurrentMovementSpeedMultiplier * new Vector3(moveDir.x, 0, moveDir.y);
        newMove.y = 0;
        rb.velocity = newMove;
    }

    private void SetAnimations()
    {
        if(moveDir == Vector2.up)
        {
            playerAnimator.SetTrigger("Up");
        }
        else if(moveDir == Vector2.down)
        {
            playerAnimator.SetTrigger("Down");
        }
        else if(moveDir == Vector2.left)
        {
            playerAnimator.SetTrigger("Left");
        }
        else if(moveDir == Vector2.right)
        {
            playerAnimator.SetTrigger("Right");
        }
        
        SetCurrentlyActiveAnimator();

        if(moveDir != Vector2.zero)
        {
            currentDirectionAnimator.SetBool("Run", true);
        }
        else
        {
            currentDirectionAnimator.SetBool("Run", false);
        }

        
    }

    private void SetCurrentlyActiveAnimator()
    {
        foreach(var anim in GetComponentsInChildren<Animator>())
        {
            if(anim != playerAnimator)
            {
                currentDirectionAnimator = anim;
                return;
            }
        }
    }
    
    private void Dash()
    {
        if(!dashReady)
            return;

        //Insert your SFX below this comment for the dash :)
        RuntimeManager.PlayOneShot(playerDash);
        
        //Set some stuff for dash functionality
        currentDirectionAnimator.SetBool("Dash", true);
        playerHealth.MakeInvurnerableForTime(playerStats.CurrentDashDuration * playerStats.CurrentDashDurationMultiplier);
        dashTimer.StartTimer(playerStats.CurrentDashCooldown * playerStats.CurrentDashCooldownMultiplier);
        dashReady = false;
        if(dashCoolDownUI != null)
            dashCoolDownUI.DashIconCD();

        //Actually make the player do the dash
        Vector3 targetPosition = transform.position + new Vector3(moveDir.x, 0, moveDir.y) * playerStats.CurrentDashDistance * playerStats.CurrentDashDistanceMultiplier;
        StartCoroutine(LerpPosition(targetPosition, playerStats.CurrentDashDuration * playerStats.CurrentDashDurationMultiplier));
    }

    private RaycastHit CheckForObstruction(Vector3 origin, Vector3 direction, float range)
    {
        RaycastHit hit = new RaycastHit();
        LayerMask mask = LayerMask.GetMask("Player");
        mask += LayerMask.GetMask("Ignore Raycast");
        mask += LayerMask.GetMask("EnemySpacing");
        mask = ~mask;
        Physics.Raycast(origin, direction, out hit, range, mask);
        return hit;
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 dashDirection = targetPosition - startPosition;
        
        //Check for obstruction and make it so you can't dash through walls
        RaycastHit potentialObstruction = CheckForObstruction(startPosition, dashDirection, dashDirection.magnitude);
        if (potentialObstruction.collider != null)
        {
            targetPosition = startPosition + dashDirection.normalized * (potentialObstruction.distance - 2f);
            Debug.Log(potentialObstruction.distance * 0.75f);
        }
        
        //Incremental movement towards the target position
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        //Finish up the dash movement
        transform.position = targetPosition;
        currentDirectionAnimator.SetBool("Dash", false);
    }
}
