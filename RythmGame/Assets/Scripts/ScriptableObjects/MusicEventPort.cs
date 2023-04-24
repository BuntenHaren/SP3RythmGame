using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Ports/Music Event Port")]
public class MusicEventPort : ScriptableObject
{
    public UnityAction onBeat = delegate {};

    private float timeForLastBeat;
    private float timeBetweenBeats;
    
    public float GetDistanceToLastBeat()
    {
        return Time.realtimeSinceStartup - timeForLastBeat;
    }

    public float GetDistanceToNextBeat()
    {
        return timeForLastBeat + timeBetweenBeats;
    }

    private void OnBeat()
    {
        timeBetweenBeats = Time.realtimeSinceStartup - timeForLastBeat;
        timeForLastBeat = Time.realtimeSinceStartup;
    }

    private void OnEnable()
    {
        onBeat += OnBeat;
    }

    private void OnDisable()
    {
        onBeat -= OnBeat;
    }
}