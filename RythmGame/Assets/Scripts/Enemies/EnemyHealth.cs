using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    //Other scripts
    [SerializeField]
    private EnemyBehavior enemyBehavior;
    [SerializeField]
    private EnemiesInCombatCounter enemiesInCombatCounter;
    [SerializeField]
    private EnemyHealthBar healthBar;
    private VirtualCameraController cameraController;

    //Stats
    [SerializeField]
    private float maxHealth;
    private float health;

    //Visual effects
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private float damageColorTime;
    [SerializeField]
    private GameObject onBeatParticles;

    [SerializeField]
    private Animator anim;
    private Rigidbody rb;
    [SerializeField]
    private DeathPort deathPort;

    //Sound
    [SerializeField]
    public EventReference enemyHitSound;
    public EventReference enemyDeathSound;

    private bool isDead = false;
    private float colorTimer;
    private Color originalColor;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originalColor = sr.color;
        cameraController = GameObject.Find("CM vcam1").GetComponent<VirtualCameraController>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        colorTimer += Time.deltaTime;
        if(colorTimer > damageColorTime)
        {
            sr.color = originalColor;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;
        if(!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        health -= damage;
        healthBar.SetHealth(health);
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        if (health <= 0)
        {
            isDead = true;
                //death sound
                RuntimeManager.PlayOneShot(enemyDeathSound);
            //Add timer
            Destroy(healthBar.gameObject);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            enemyBehavior.isDead = true;
            enemiesInCombatCounter.RemoveEnemyFromList(enemyBehavior);
        }
    }

    public void TakeDamageOnBeat(float damage)
    {
        if (isDead)
            return;
        if (!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        health -= damage;
        healthBar.SetHealth(health);
        cameraController.CameraZoomBeatAttack();
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        Instantiate(onBeatParticles, transform);
        if (health <= 0)
        {
            isDead = true;
            //death sound
            RuntimeManager.PlayOneShot(enemyDeathSound);
            //Add timer
            Destroy(healthBar.gameObject);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            enemyBehavior.isDead = true;
            enemiesInCombatCounter.RemoveEnemyFromList(enemyBehavior);
        }
    }

    public void HealDamage(float damage)
    {
        health = health + damage;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void ChangeColor()
    {
        sr.color = damageColor;
    }


}
