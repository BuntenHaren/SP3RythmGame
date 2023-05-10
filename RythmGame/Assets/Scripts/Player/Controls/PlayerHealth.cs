using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private VirtualCameraController cameraController;
    [SerializeField]
    private GameObject deathMenu;

    [SerializeField]
    private Health healthObject;
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private DeathPort deathPort;

    [SerializeField]
    private EventReference playerHurtDeathSound;

    private Timer InvincibilityTimer;

    private void OnValidate()
    {
        if(healthObject == null)
            Debug.LogError("Health object is currently not set. Please set the Health Object to a viable object of type Health");
    }

    private void Start()
    {
        healthObject.ResetHealth();
        InvincibilityTimer = new Timer();
        InvincibilityTimer.TimerDone += MakeVurnerableAgain;
        cameraController = GameObject.Find("CM vcam1").GetComponent<VirtualCameraController>();
    }

    public void TakeDamage(float amount)
    {
        RuntimeManager.PlayOneShot(playerHurtDeathSound);
        cameraController.CameraShake();
        if (!healthObject.Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
            if(healthObject.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
    }

    public void TakeDamageOnBeat(float amount)
    {
        RuntimeManager.PlayOneShot(playerHurtDeathSound);
        cameraController.CameraShake();
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
        RuntimeManager.PlayOneShot(playerHurtDeathSound);
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
