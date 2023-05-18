using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RythmGame/Enemy/Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> objects;
}
