using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCoolDownUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    private Image image;

    private float coolDownTime;
    private float coolDownTimer;
    private bool isCoolDown = false;

    void Start()
    {
        coolDownTime = (playerStats.CurrentDashCooldown * playerStats.CurrentDashCooldownMultiplier) + 1f;
        image = gameObject.GetComponent<Image>();
        image.fillAmount = 0.0f;
    }

    private void Update()
    {
        if(isCoolDown)
            OnCD();
    }

    private void OnCD()
    {
        coolDownTimer -= Time.deltaTime;

        if(coolDownTimer < 0.0f)
        {
            isCoolDown = false;
            image.fillAmount = 0.0f;
        }
        else
            image.fillAmount = coolDownTimer / coolDownTime;
    }

    public void DashIconCD()
    {
        isCoolDown = true;
        coolDownTimer = coolDownTime;
    }
}
