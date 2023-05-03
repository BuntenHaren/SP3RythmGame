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
    [SerializeField]
    private DeathPort deathPort;

    //Sound
    [SerializeField]
    public EventReference enemyHitSound;
    public EventReference enemyDeathSound;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        if(health <= 0)
        {
            //death sound
            RuntimeManager.PlayOneShot(enemyDeathSound);
            deathPort.onEnemyDeath.Invoke(gameObject);
            anim.SetBool("Dead", true);
            enemyBehavior.isDead = true;
            enemiesInCombatCounter.RemoveEnemyFromList(enemyBehavior);
        }
    }

    public void TakeDamageOnBeat(float damage)
    {
        health -= damage;
        //Hit sound
        RuntimeManager.PlayOneShot(enemyHitSound);
        Instantiate(onBeatParticles);

        if (health <= 0)
        {
            //death sound
            RuntimeManager.PlayOneShot(enemyDeathSound);
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

    private IEnumerator ChangeColor()
    {
        var originalColor = sr.color;
        sr.color = damageColor;
        yield return new WaitForSeconds(damageColorTime);
        sr.color = originalColor;
    }
}
