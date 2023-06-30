using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLightningLuncher : WeaponControllerBase
{

    public override void Start()
    {
        base.Start();
    }
    public override void LoadData()
    {
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("ballLightningSpeed");
        radius = WeaponDataManager.instance.GetCurrentValueByString("ballLightningRadius");
        duration = WeaponDataManager.instance.GetCurrentValueByString("ballLightningDuration");
        damage = WeaponDataManager.instance.GetCurrentValueByString("ballLightningDamage");

        FixedData();
        FixedElectricDamage();
    }
    public override void Fire()
    {
        BallLightningBullet ballLightningBullet = GameObjectPoolManager.instance.ballLightningPool.Get(transform.position,duration).GetComponent<BallLightningBullet>();
        ballLightningBullet.transform.position = transform.position;
        ballLightningBullet.dir = (targetMonster.transform.position - transform.position).normalized;

        ballLightningBullet.attackSpeed = attackSpeed*3;
        ballLightningBullet.duration = duration;
        ballLightningBullet.radius = radius;
        ballLightningBullet.damage = damage;

        AudioManager.PlayClip("ballLightningB");
    }

}
