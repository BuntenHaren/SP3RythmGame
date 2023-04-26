using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RythmGame/Event Ports/Death Port")]
public class DeathPort : ScriptableObject
{
    public UnityAction<GameObject> onEnemyDeath = delegate {};
    public UnityAction<GameObject> onPlayerDeath = delegate {};
}
