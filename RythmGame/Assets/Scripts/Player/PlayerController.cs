using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private float currentDashCooldown;
    private float currentMovementSpeed;
    private Rigidbody rb;
    private Vector2 moveDir;
    private Vector3 newMove;

    private void Start()
    {
        currentDashForce = baseDashForce;
        currentMovementSpeed = baseMovementSpeed;
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnDash()
    {
        Dash();
    }

    private void FixedUpdate()
    {
        Move();
        if(currentDashCooldown > -10)
            currentDashCooldown -= Time.fixedDeltaTime;
    }

    private void Move()
    {
        newMove = new Vector3(moveDir.x, 0, moveDir.y) * currentMovementSpeed;
        newMove.y = rb.velocity.y;
        rb.velocity = newMove;
    }

    private void Dash()
    {
        if(currentDashCooldown <= 0)
        {
            rb.AddForce(newMove * currentDashForce, ForceMode.Impulse);
            currentDashCooldown = baseDashCooldown;
            Debug.Log("Dashed");
        }
    }
    
}
