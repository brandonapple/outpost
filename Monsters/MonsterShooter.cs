using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MonsterShooter : Monster
{
    public override void AttackPoint()
    {
        transform.DOShakeRotation(.1f, 30, 30, 30, true);
        GameObject bullet = GameObjectPoolManager.instance.monsterBulletPool.Get(transform.position + Vector3.up * .5f, 5);
    }
}
