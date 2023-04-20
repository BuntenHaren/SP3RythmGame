using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
        rb = GetComponent<Rigidbody>();
        if(!TryGetComponent<PlayerHealth>(out playerHealth))
            playerHealth = null;
            
        //Set some current variables
        dashTimer = new Timer();
        dashTimer.TimerDone += () => dashReady = true;
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
        
        newMove = new Vector3(moveDir.x, 0, moveDir.y) * currentMovementSpeed;
        newMove.y = rb.velocity.y;
        rb.velocity = newMove;
    }

    private void Dash()
    {
        if(!dashReady)
            return;

        Vector3 targetPosition = transform.position + new Vector3(moveDir.x, 0, moveDir.y) * currentDashDistance;
        StartCoroutine(LerpPosition(targetPosition, currentDashDuration));
        playerHealth.MakeInvurnerableForTime(currentDashDuration);
        dashTimer.StartTimer(currentDashCooldownAmount);
        dashReady = false;
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
