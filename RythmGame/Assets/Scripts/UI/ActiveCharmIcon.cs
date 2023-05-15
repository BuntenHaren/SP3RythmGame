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

    private Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if(playerStats.CurrentActiveCharm.name == "ArcaneSurge")
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
}
