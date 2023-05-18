using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PassiveCharmIcon : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private CharmDescription charmDescription;

    public Sprite ArcaneGorgerIcon;
    public Sprite BeatMasterIcon;

    [SerializeField]
    public Sprite emptyIcon;

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
    private bool isPaused = false;

    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        image = gameObject.GetComponent<Image>();
        originalPos = rectTransform.anchoredPosition;
        originalScale = rectTransform.sizeDelta;
        if (playerStats.CurrentPassiveCharm.name == "ArcaneGorger")
        {
            image.sprite = ArcaneGorgerIcon;
        }
        else if (playerStats.CurrentPassiveCharm.name == "BeatMaster")
        {
            image.sprite = BeatMasterIcon;
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
        if (playerStats.CurrentPassiveCharm != null)
        {
            isPaused = true;
            rectTransform.DOAnchorPos(pauseMoveTo, pauseMoveDuration).SetUpdate(true);
            rectTransform.DOSizeDelta(pauseSizeTo, pauseScaleDuration).SetUpdate(true);
        }
    }

    public void OnResume()
    {
        isPaused = false;
        rectTransform.DOAnchorPos(originalPos, pauseMoveDuration);
        rectTransform.DOSizeDelta(originalScale, pauseScaleDuration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPaused)
        {
            charmDescription.ShowDescription();
        }
    }
}
