using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RythmGame/Counters/JuiceCounter")]
public class JuiceCounter : ScriptableObject
{
    public UnityAction<float> onChange = delegate {};

    public float MaxJuice { private set; get; } = 10;
    
    private float currentJuice;

    public float CurrentJuice
    {
        get
        {
            return currentJuice;
        }
        set
        {
            if(currentJuice != value && value <= MaxJuice)
            {
                float juiceChange = value - currentJuice;
                currentJuice = value;
                onChange.Invoke(juiceChange);
            }
        }
    }

    private void OnEnable()
    {
        currentJuice = 0;
    }
}