using UnityEngine;

public class ControlCharms : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    // private charm variables
    [Header("Charms")]
    // passive
    [SerializeField]
    private EmptyCharm emptyCharm;
    [SerializeField]
    private BeatMaster beatMaster;
    [SerializeField]
    private ArcaneGorger arcaneGorger;
    // active
    [SerializeField]
    private ActiveEmptyCharm activeEmptyCharm;
    [SerializeField]
    private ArcaneSurge arcaneSurge;
    // change charm icon
    [SerializeField]
    private PassiveCharmIcon passiveCharmIcon;
    [SerializeField]
    private ActiveCharmIcon activeCharmIcon;

    // Timer for delayed heal (after deactivating arcane gorger)
    private float delayTime = 0.05f;
    protected Timer delayedHealTimer;

    void Start()
    {
        playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();

        // timer for delayed heal
        delayedHealTimer = new Timer();
        delayedHealTimer.TimerDone += EndDelayedHeal;
    }

    void Update()
    {
        playerStats.CurrentActiveCharm.Update();
        playerStats.CurrentPassiveCharm.Update();

        // increment delayed heal timer
        delayedHealTimer.UpdateTimer(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        playerStats.CurrentActiveCharm.FixedUpdate();
        playerStats.CurrentPassiveCharm.FixedUpdate();
    }

    private void SwitchPassiveCharm(PassiveCharm newPassiveCharm)
    {
        Debug.Log("Switching charm from " + playerStats.CurrentPassiveCharm + " to " + newPassiveCharm);
        playerStats.CurrentPassiveCharm.Finish();
        playerStats.CurrentPassiveCharm = newPassiveCharm;
        playerStats.CurrentPassiveCharm.Start();
        //Insert your thing here for switching the passive charm if you have something :)
        
    }
    
    private void SwitchActiveCharm(ActiveCharm newActiveCharm)
    {
        playerStats.CurrentActiveCharm.Finish();
        playerStats.CurrentActiveCharm = newActiveCharm;
        playerStats.CurrentActiveCharm.Start();
        //Insert your thing here for switching the active charm if you have something :)
        
    }

    private void OnActivateCharm()
    {
        playerStats.CurrentActiveCharm.ActivateCharm();
    }

    private void OnEnable()
    {
        playerStats.CurrentActiveCharm.Start();
        playerStats.CurrentPassiveCharm.Start();
    }

    private void OnDisable()
    {
        playerStats.CurrentActiveCharm.Finish();
        playerStats.CurrentPassiveCharm.Finish();
    }

    public void OnSwitchPassiveCharm()
    {
        if (playerStats.CurrentPassiveCharm == emptyCharm && (playerStats.BeatMasterEnabled))
        {
            SwitchPassiveCharm(beatMaster);
            passiveCharmIcon.ChangeIcon(passiveCharmIcon.BeatMasterIcon);
        }
        else
        if (playerStats.CurrentPassiveCharm == beatMaster)
        {
            if (playerStats.ArcaneGorgerEnabled)
            {
                SwitchPassiveCharm(arcaneGorger);
                passiveCharmIcon.ChangeIcon(passiveCharmIcon.ArcaneGorgerIcon);
            }
            else
            {
                SwitchPassiveCharm(emptyCharm);
                passiveCharmIcon.ChangeIcon(passiveCharmIcon.emptyIcon);
            }
        }
        else
        if (playerStats.CurrentPassiveCharm == arcaneGorger)
        {
            SwitchPassiveCharm(emptyCharm);
            passiveCharmIcon.ChangeIcon(passiveCharmIcon.emptyIcon);

            // activate delayed heal timer
            delayedHealTimer.StartTimer(delayTime);
        }
    }

    private void EndDelayedHeal()
    {
        // heal after delay has finished
        arcaneGorger.Heal();
    }
}