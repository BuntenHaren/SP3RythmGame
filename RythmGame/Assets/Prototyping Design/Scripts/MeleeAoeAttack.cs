using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAoeAttack : MonoBehaviour
{
    [SerializeField]
    private MeleeEnemy meleeEnemy;
    [SerializeField]
    private BeatManager beatManager;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Color startingColor;
    [SerializeField]
    private Color windUpColor;
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private GameObject damageObject;

    public void ExecuteAttack()
    {
        StartCoroutine(AttackEnumerator());
    }

    private IEnumerator AttackEnumerator()
    {
        sr.color = windUpColor;
        yield return new WaitForSeconds(beatManager.beatsPerMinute / 60);
        damageObject.SetActive(true);
        sr.color = damageColor;
        yield return new WaitForSeconds(0.2f);
        damageObject.SetActive(false);
        meleeEnemy.attacking = false;
        sr.color = startingColor;
    }
}
