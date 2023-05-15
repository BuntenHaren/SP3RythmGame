using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PassiveCharmIcon : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    public Sprite ArcaneGorgerIcon;
    public Sprite BeatMasterIcon;

    [SerializeField]
    private Sprite emptyIcon;

    [SerializeField]
    private float fadeDuration;

    private Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
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
}
