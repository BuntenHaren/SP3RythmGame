using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortAxis : MonoBehaviour
{
    [SerializeField]
    private Vector3 sortAxis;

    void Awake()
    {
        //Camera.main.transparencySortAxis = sortAxis;
    }
}
