using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveSpeedDuringAttack;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private float attackDuration;
    [SerializeField]
    private float attackWindUp;
    [SerializeField]
    private GameObject attackObject;

    [SerializeField]
    private float dashForce;
    [SerializeField]
    private float dashCD;
    [SerializeField]
    private float dashDuration;
    private float dashTimer;
    [SerializeField]
    private ConstantForce constantForce;
    public bool isDashing;

    [SerializeField]
    private GameObject playerProjectile;

    private Vector2 moveDirection;

    public float test;

    void Awake()
    {
        dashTimer = dashCD;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Move();
        dashTimer += Time.deltaTime;
    }

    void ProcessInput()
    {
        //Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        anim.SetFloat("speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));
        if(moveX<0 && anim.GetBool("attacking") == false)
        {
            sr.flipX = false;
        }
        if (moveX > 0 && anim.GetBool("attacking") == false)
        {
            sr.flipX = true;
        }
        moveDirection = new Vector2(moveX, moveY).normalized;

        //Attack
        if(Input.GetKeyDown(KeyCode.Mouse0) && anim.GetBool("dashing") == false)
        {
            StartCoroutine(Attack(attackDuration));
        }

        //Dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer>dashCD && anim.GetBool("attacking") == false)
        {
            StartCoroutine(Dash(dashForce));
        }

        //Projectile
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ProjectileAttack();
        }
    }

    //Attack
    private IEnumerator Attack(float attackDuration)
    {
        var originalMoveSpeed = moveSpeed;
        moveSpeed = moveSpeedDuringAttack;
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(attackWindUp);
        attackObject.SetActive(true);
        yield return new WaitForSeconds(attackDuration - attackWindUp);
        anim.SetBool("attacking", false);
        attackObject.SetActive(false);
        moveSpeed = originalMoveSpeed;
    }

    //Dash
    private IEnumerator Dash(float dashForce)
    {
        isDashing = true;
        constantForce.force = rb.velocity * 100f;
        anim.SetBool("dashing", true);
        yield return new WaitForSeconds(dashDuration);
        constantForce.force = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        anim.SetBool("dashing", false);
        constantForce.force = new Vector3(0f, 0f, 0f);
    }
    
    void Move()
    {
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0f, moveDirection.y * moveSpeed);
    }

    void ProjectileAttack()
    {
        Instantiate(playerProjectile, gameObject.GetComponent<Transform>());
    }

}