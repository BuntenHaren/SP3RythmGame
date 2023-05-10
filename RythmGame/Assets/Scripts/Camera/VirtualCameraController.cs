using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class VirtualCameraController : MonoBehaviour
{
    //private variables
    private CinemachineVirtualCamera vCam;
    private CinemachineFramingTransposer vCamTransposer;
    private CinemachineBasicMultiChannelPerlin vCamPerlinChannel;
    private bool isAnimating = false;

    //Values
    [SerializeField]
    private Vector3 BeatZoomInValue;
    [SerializeField]
    private float BeatZoomInDuration;
    [SerializeField]
    private float BeatZoomOutDuration;
    [SerializeField]
    private float cameraShakeDuration;
    [SerializeField]
    private float cameraShakeAmplitude;
    [SerializeField]
    private float cameraShakeFrequency;

    void Start()
    {
        vCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        vCamTransposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        vCamPerlinChannel = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CameraZoom(float zoomEndValue, float zoomDuration)
    {
        DOTween.To(() => vCam.m_Lens.FieldOfView, x => vCam.m_Lens.FieldOfView = x, zoomEndValue, zoomDuration);
    }

    public void CameraZoomBeatAttack()
    {
        if(!isAnimating)
        {
            isAnimating = true;
            var originalZoom = vCamTransposer.m_TrackedObjectOffset;
            DOTween.To(() => vCamTransposer.m_TrackedObjectOffset, x => vCamTransposer.m_TrackedObjectOffset = x, BeatZoomInValue, BeatZoomInDuration).OnComplete(() =>
            {
                DOTween.To(() => vCamTransposer.m_TrackedObjectOffset, x => vCamTransposer.m_TrackedObjectOffset = x, originalZoom, BeatZoomOutDuration);
            });
            isAnimating = false;
        }
    }

    public void CameraShake()
    {
        vCamPerlinChannel.m_AmplitudeGain = cameraShakeAmplitude;
        DOTween.To(() => vCamPerlinChannel.m_AmplitudeGain, x => vCamPerlinChannel.m_AmplitudeGain = x, 0f, cameraShakeDuration);
    }
}
