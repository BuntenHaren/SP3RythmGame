using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using FMODUnity;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]
    private float baseMovementSpeed = 10;
    
    [Header("Dash Variables")]
    [SerializeField]
    private float baseDashCooldown = 2;
    [SerializeField]
    private float baseDashDistance = 5;
    [SerializeField] 
    private float baseDashDuration = 0.1f;
    
    [Header("Animation and VFX")]
    [SerializeField]
    private Animator playerAnimator;

    [Header("SFX")]
    [SerializeField]
    private EventReference playerDash;
    
    //private dash variables
    private float currentDashDistance;
    private float currentDashDuration;
    private float currentDashCooldownAmount;
    private bool dashReady = true;
    private Timer dashTimer;
    
    //private movement variables
    private float currentMovementSpeed;
    private Rigidbody rb;
    private Vector3 newMove;
    private Vector2 moveDir;

    private PlayerHealth playerHealth;

    private void Start()
    {
        //Fix component variables
        rb = GetComponent<Rigidbody>();
        if(TryGetComponent(out PlayerHealth h))
            playerHealth = h;
        if(TryGetComponent(out Animator a))
            playerAnimator = a;
        
        //Get some new stuff ready
        dashTimer = new Timer();
        dashTimer.TimerDone += () => dashReady = true;
        
        //Set some current variables
        currentDashDistance = baseDashDistance;
        currentDashDuration = baseDashDuration;
        currentDashCooldownAmount = baseDashCooldown;
        currentMovementSpeed = baseMovementSpeed;
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
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        SetAnimations();

        //Move the character in the right direction
        newMove = new Vector3(moveDir.x, 0, moveDir.y) * currentMovementSpeed;
        newMove.y = rb.velocity.y;
        rb.velocity = newMove;
    }

    private void SetAnimations()
    {
        //Set animator values
        if(moveDir != Vector2.zero)
            playerAnimator.SetBool("Run", true);
        else
        {
            playerAnimator.SetBool("Run", false);
        }

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
        
    }

    private void Dash()
    {
        if(!dashReady)
            return;

        //Insert your SFX below this comment for the dash :)
        RuntimeManager.PlayOneShot(playerDash);
        
        //Set some stuff for dash functionality
        playerAnimator.SetBool("Dash", true);
        playerHealth.MakeInvurnerableForTime(currentDashDuration);
        dashTimer.StartTimer(currentDashCooldownAmount);
        dashReady = false;
        
        //Actually make the player do the dash
        Vector3 targetPosition = transform.position + new Vector3(moveDir.x, 0, moveDir.y) * currentDashDistance;
        StartCoroutine(LerpPosition(targetPosition, currentDashDuration));
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
        if(potentialObstruction.collider != null)
            targetPosition = startPosition + dashDirection.normalized * potentialObstruction.distance;
        
        //Incremental movement towards the target position
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        //Finish up the dash movement
        transform.position = targetPosition;
        playerAnimator.SetBool("Dash", false);
    }
}
