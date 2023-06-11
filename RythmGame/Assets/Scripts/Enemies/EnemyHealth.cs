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
    [SerializeField]
    private WaveBlockage waveBlockage;
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
    private bool changeToDeadSprite = false;
    private bool colorsHaveBeenChanged;
    private float colorTimer;
    private Timer deathTimer;

    private GameObject deadSpriteLeft;
    private GameObject deadSpriteRight;
    private GameObject enemySpacing;

    void Awake()
    {
        if (GameObject.Find("ExitBlockage") != null)
        {
            exitBlockage = GameObject.Find("ExitBlockage").GetComponent<ExitBlockage>();
        }
        if(exitBlockage != null)
            exitBlockage.AddEnemyToList(enemyBehavior);

        if(transform.Find("DeadSpriteLeft") != null)
            deadSpriteLeft = transform.Find("DeadSpriteLeft").gameObject;
        if (transform.Find("DeadSpriteRight") != null)
            deadSpriteRight = transform.Find("DeadSpriteRight").gameObject;

        if (transform.Find("EnemySpacing") != null)
            enemySpacing = transform.Find("EnemySpacing").gameObject;

        deathTimer = new Timer();
        deathTimer.TimerDone += () => changeToDeadSprite = true;
        rb = gameObject.GetComponent<Rigidbody>();
        //originalColor = sr.color;
        cameraController = GameObject.Find("CM vcam1").GetComponent<VirtualCameraController>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        deathTimer.UpdateTimer(Time.fixedDeltaTime);
        colorTimer += Time.deltaTime;
        if(colorsHaveBeenChanged && colorTimer > damageColorTime)
        {
            ChangeBackColor();
        }
        if(changeToDeadSprite)
        {
            ChangetoDeadSprite();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;
        if(!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        ChangeColor();
        health -= damage;
        healthBar.SetHealth(health);
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        if (health <= 0)
        {
            deathTimer.StartTimer(1f);
            isDead = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            enemySpacing.SetActive(false);
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
            if (waveBlockage != null)
                waveBlockage.RemoveEnemyFromList(enemyBehavior);
        }
    }

    public void TakeDamageOnBeat(float damage)
    {
        if (isDead)
            return;
        if (!enemyBehavior.attacking)
                anim.SetTrigger("Hurt");
        ChangeColor();
        health -= damage;
        healthBar.SetHealth(health);
        cameraController.CameraZoomBeatAttack();
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        Instantiate(onBeatParticles, transform);
        if (health <= 0)
        {
            deathTimer.StartTimer(1.5f);
            isDead = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            enemySpacing.SetActive(false);
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
            if(waveBlockage != null)
                waveBlockage.RemoveEnemyFromList(enemyBehavior);
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

    private void ChangetoDeadSprite()
    {
        if(anim.GetBool("FacingLeft"))
        {
            if(deadSpriteLeft != null)
            deadSpriteLeft.SetActive(true);
        }
        else
        {
            if(deadSpriteRight != null)
            deadSpriteRight.SetActive(true);
        }
        enemyBehavior.SpritesParent.SetActive(false);
        changeToDeadSprite = false;
    }

}
