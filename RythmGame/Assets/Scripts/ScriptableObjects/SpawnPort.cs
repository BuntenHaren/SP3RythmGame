using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RythmGame/Event Ports/Spawn Port")]
public class SpawnPort : ScriptableObject
{
    public UnityAction<GameObject> onEnemySpawn = delegate {};
}
