using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PassiveCharmIcon : MonoBehaviour
{
    public Sprite arcaneGorgerIcon;
    public Sprite beatMasterIcon;

    [SerializeField]
    private Sprite emptyIcon;

    [SerializeField]
    private float fadeDuration;

    private Sequence sequence;
    private Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void ChangeIcon(Sprite sprite)
    {
        image.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            image.sprite = sprite;
            //image.DOKill();
            image.DOFade(150f, 100f);
        });
    }

}
