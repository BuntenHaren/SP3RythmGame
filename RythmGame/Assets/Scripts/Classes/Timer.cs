using UnityEngine.Events;

public class Timer
{
    public UnityAction TimerDone = delegate {};

    private float time;
    private bool active;

    public void StartTimer(float time)
    {
        this.time = time;
        active = true;
    }

    public void UpdateTimer(float deltaTime)
    {
        if(!active)
            return;
        
        time -= deltaTime;
        if(time <= 0)
        {
            active = false;
            TimerDone.Invoke();
        }
    }
}
