using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private PlayerHealthBar healthBar;
    [SerializeField]
    private Health healthObject;
    [SerializeField]
    private GameObject youDiedText;
    private SceneChanger sceneChanger;

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
        healthBar.SetMaxHealth(healthObject.MaxHealth);
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    public void TakeDamage(float amount)
    {
        if(!healthObject.Invurnerable)
        {
            healthObject.CurrentHealth -= amount;
            healthBar.SetHealth(healthObject.CurrentHealth);
            if(healthObject.CurrentHealth <= 0)
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
        youDiedText.SetActive(true);
        StartCoroutine(sceneChanger.ReloadCurrentScene(2f));
    }

    public void Spawn()
    {
        enabled = true;
        GetComponent<PlayerAttacks>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        youDiedText.SetActive(false);
        HealDamage(healthObject.MaxHealth);
        healthBar.SetHealth(healthObject.CurrentHealth);
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
    }
}
