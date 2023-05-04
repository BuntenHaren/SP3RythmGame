using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject deathMenu;

    [SerializeField]
    private Health healthObject;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private DeathPort deathPort;

    private Timer InvincibilityTimer;

    private void OnValidate()
    {
        if(healthObject == null)
            Debug.LogError("Health object is currently not set. Please set the Health Object to a viable object of type Health");
    }

    private void Start()
    {
        InvincibilityTimer = new Timer();
        InvincibilityTimer.TimerDone += MakeVurnerableAgain;
    }

    public void TakeDamage(float amount)
    {
        if(!healthObject.Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
            Debug.Log(healthObject.CurrentHealth);
            if(healthObject.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
    }

    public void TakeDamageOnBeat(float amount)
    {
        if (!healthObject.Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
            if (healthObject.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
    }

    private void OnDeath()
    {
        MakeInvurnerableForTime(2f);
        enabled = false;
        GetComponent<PlayerAttacks>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        deathPort.onPlayerDeath.Invoke(gameObject);
        deathMenu.SetActive(true);
    }

    public void Spawn()
    {
        enabled = true;
        GetComponent<PlayerAttacks>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        HealDamage(healthObject.CurrentMaxHealth);
        Debug.Log(healthObject.CurrentHealth);
    }

    public void HealDamage(float amount)
    {
        healthObject.CurrentHealth += amount;
    }

    public void MakeInvurnerableForTime(float time)
    {
        healthObject.Invurnerable = true;
        InvincibilityTimer.StartTimer(time);
    }

    private void MakeVurnerableAgain()
    {
        healthObject.Invurnerable = false;
    }

    private void FixedUpdate()
    {
        InvincibilityTimer.UpdateTimer(Time.fixedDeltaTime);
        healthObject.CurrentMaxHealth = healthObject.BaseMaxHealth * playerStats.CurrentMaxHealthMultiplier;
    }
}
