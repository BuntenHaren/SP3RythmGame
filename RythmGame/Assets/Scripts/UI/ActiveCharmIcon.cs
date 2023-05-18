using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActiveCharmIcon : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    public Sprite ArcaneSurgeIcon;
    [SerializeField]
    private Sprite emptyIcon;
    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float pauseScaleDuration;
    [SerializeField]
    private float pauseMoveDuration;
    [SerializeField]
    private Vector2 pauseMoveTo;
    [SerializeField]
    private Vector2 pauseSizeTo;

    private Vector2 originalPos;
    private Vector2 originalScale;

    private Image image;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
        originalPos = rectTransform.anchoredPosition;
        originalScale = rectTransform.sizeDelta;
        if (playerStats.CurrentActiveCharm.name == "ArcaneSurge")
        {
            image.sprite = ArcaneSurgeIcon;
        }
    }

    public void ChangeIcon(Sprite sprite)
    {
        image.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            image.sprite = sprite;
            image.DOFade(255f, 100f);
        });
    }

    public void OnPause()
    {
        rectTransform.DOAnchorPos(pauseMoveTo, pauseMoveDuration).SetUpdate(true);
        rectTransform.DOSizeDelta(pauseSizeTo, pauseScaleDuration).SetUpdate(true);
    }

    public void OnResume()
    {
        rectTransform.DOAnchorPos(originalPos, pauseMoveDuration);
        rectTransform.DOSizeDelta(originalScale, pauseScaleDuration);
    }
}
