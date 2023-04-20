using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Juice Counter")]
public class JuiceCounter : ScriptableObject
{
    public UnityAction<int> onChange = delegate {};
    
    public int MaxJuice;
    
    private int currentJuice;

    public int CurrentJuice
    {
        get
        {
            return currentJuice;
        }
        set
        {
            if (currentJuice != value)
            {
                int juiceChange = value - currentJuice;
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