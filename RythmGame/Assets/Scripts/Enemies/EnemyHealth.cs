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
    private ExitBlockage exitBlockage;
    private VirtualCameraController cameraController;

    //Stats
    [SerializeField]
    private float maxHealth;
    private float health;

    //Visual effects
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private float damageColorTime;
    [SerializeField]
    private GameObject onBeatParticles;
    [SerializeField]
    private SpriteRenderer[] childSr;

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
    private bool changeColors;
    private bool colorsHaveBeenChanged;

    void Awake()
    {
        if (GameObject.Find("ExitBlockage") != null)
        {
            exitBlockage = GameObject.Find("ExitBlockage").GetComponent<ExitBlockage>();
        }
        if(exitBlockage != null)
            exitBlockage.AddEnemyToList(enemyBehavior);
        rb = gameObject.GetComponent<Rigidbody>();
        //originalColor = sr.color;
        cameraController = GameObject.Find("CM vcam1").GetComponent<VirtualCameraController>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        colorTimer += Time.deltaTime;
        if(colorsHaveBeenChanged && colorTimer > damageColorTime)
        {
            ChangeBackColor();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;
        if(!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        ChangeColor();
        changeColors = true;
        health -= damage;
        healthBar.SetHealth(health);
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        if (health <= 0)
        {
            isDead = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            //death sound
            RuntimeManager.PlayOneShot(enemyDeathSound);
            //Add timer
            Destroy(healthBar.gameObject);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            enemyBehavior.isDead = true;
            enemiesInCombatCounter.RemoveEnemyFromList(enemyBehavior);
            if (exitBlockage != null)
                exitBlockage.RemoveEnemyFromList(enemyBehavior);
        }
    }

    public void TakeDamageOnBeat(float damage)
    {
        if (isDead)
            return;
        if (!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        ChangeColor();
        changeColors = true;
        health -= damage;
        healthBar.SetHealth(health);
        cameraController.CameraZoomBeatAttack();
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        Instantiate(onBeatParticles, transform);
        if (health <= 0)
        {
            isDead = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            //death sound
            RuntimeManager.PlayOneShot(enemyDeathSound);
            //Add timer
            Destroy(healthBar.gameObject);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            enemyBehavior.isDead = true;
            enemiesInCombatCounter.RemoveEnemyFromList(enemyBehavior);
            if (exitBlockage != null)
                exitBlockage.RemoveEnemyFromList(enemyBehavior);
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
        colorTimer = 0f;
        for (int i = 0; i < childSr.Length; i++)
        {
            childSr[i].color = damageColor;
        }
        colorsHaveBeenChanged = true;
    }

    private void ChangeBackColor()
    {
        for (int i = 0; i < childSr.Length; i++)
        {
            childSr[i].color = originalColor;
        }
        colorsHaveBeenChanged = false;
    }

}
