using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageFadeOut : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.DOFade(0f, 1f).SetEase(Ease.InOutSine);
    }
}
