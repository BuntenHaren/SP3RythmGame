using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseMovementSpeed;
    [SerializeField]
    private float baseDashCooldown;
    [SerializeField]
    private float baseDashForce;

    private float currentDashForce;
    private float currentDashCooldownAmount;
    private bool dashReady = true;
    private float currentMovementSpeed;
    private Rigidbody rb;
    private Vector3 newMove;
    private Vector2 moveDir;
    private Timer dashTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //Set some dash variables
        dashTimer = new Timer();
        dashTimer.TimerDone += () => dashReady = true;
        currentDashForce = baseDashForce;
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
        
        dashReady = false;
        rb.AddForce(newMove * currentDashForce, ForceMode.Impulse);
        dashTimer.StartTimer(currentDashCooldownAmount);
        Debug.Log("Dashed");
    }

}
