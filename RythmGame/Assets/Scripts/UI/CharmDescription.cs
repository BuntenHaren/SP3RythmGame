using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CharmDescription : MonoBehaviour, IPointerExitHandler
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private bool isActiveCharm;

    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private TMP_Text title;

    [SerializeField]
    private Vector2 targetPos;
    [SerializeField]
    private float animationDuration;

    private Vector2 originalPos;
    private bool animationFinished = true;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    public void ShowDescription()
    {
        if (animationFinished)
        {
            //Fix when charm scriptable objects are fixed
            if (isActiveCharm)
            {
                title.text = playerStats.CurrentActiveCharm.CharmDescription.CharmTitle;
                description.text = playerStats.CurrentActiveCharm.CharmDescription.CharmDescription;
            }
            else
            {
                title.text = playerStats.CurrentPassiveCharm.CharmDescription.CharmTitle;
                description.text = playerStats.CurrentPassiveCharm.CharmDescription.CharmDescription;
            }
            animationFinished = false;
            rectTransform.DOAnchorPos(targetPos, animationDuration).SetEase(Ease.OutBounce).SetUpdate(true).OnComplete(() =>
            {
                animationFinished = true;
            });
        }
    }

    public void HideDescription()
    {
        if (animationFinished)
        {
            animationFinished = false;
            rectTransform.DOAnchorPos(originalPos, animationDuration).SetEase(Ease.OutBounce).SetUpdate(true).OnComplete(() =>
            {
                animationFinished = true;
            });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideDescription();
    }
}
