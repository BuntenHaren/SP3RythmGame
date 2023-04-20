using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomTrigger : MonoBehaviour
{
    private GameObject vCam;
    [SerializeField]
    private float targetFOV;
    [SerializeField]
    private float originalFOV;
    [SerializeField]
    private float zoomDuration;

    void Start()
    {
        vCam = GameObject.Find("CM vcam1");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            vCam.GetComponent<VirtualCameraController>().CameraZoom(targetFOV, zoomDuration);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            vCam.GetComponent<VirtualCameraController>().CameraZoom(originalFOV, zoomDuration);
        }
    }
}
