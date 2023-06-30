using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterBomb : Monster
{
    public bool turnedRed;
    public override void AttackPoint()
    {
        Destroy(gameObject);
        EffectManager.instance.SpawnEffect("monsterBombExplosionEffect", transform.position, Quaternion.identity);
        GameObjectPoolManager.instance.explosionShadowEffectPool.Get(transform.position, 1);
        Base.instance.Hitted(3, transform.position);
        lifeSlider.UpdataValue(0);
    }
    public override void MeleeAttackment()
    {
        base.MeleeAttackment();
        if (!turnedRed)
        {
            turnedRed = true;
            TurnRed();
        }

    }
    public void TurnRed()
    {
        monsterSpriteRenderer.DOColor(Color.red, 1);
        monsterSpriteRenderer.transform.DOScale(monsterSpriteRenderer.transform.localScale.x*1.5f, 1);
    }
}
