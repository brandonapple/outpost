using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlimeCurve : Slime
{
    public override void AttackPoint()
    {
        MonsterShooterBullet bullet = GameObjectPoolManager.instance.monsterBulletPool.
             Get(transform.position + Vector3.up * .5f, 3).GetComponent<MonsterShooterBullet>();
        bullet.bulletColor = Color.red;
    }
}
