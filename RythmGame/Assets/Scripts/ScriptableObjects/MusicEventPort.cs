using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Event Ports/Music Event Port")]
public class MusicEventPort : ScriptableObject
{
    public UnityAction onBeat = delegate {};
}