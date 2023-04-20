using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class VirtualCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    void Start()
    {
        vCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }
    public void CameraZoom(float zoomEndValue, float zoomDuration)
    {
        DOTween.To(() => vCam.m_Lens.FieldOfView, x => vCam.m_Lens.FieldOfView = x, zoomEndValue, zoomDuration);
    }
}
