using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : WeaponControllerBase
{
    public int penetrateTime;
    float damageTem;
    public override void LoadData()
    {
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("sniperSpeed");
        damage = WeaponDataManager.instance.GetCurrentValueByString("sniperDamage");
        penetrateTime = (int)WeaponDataManager.instance.GetCurrentValueByString("sniperPenetrate");
      
        FixedData();
        FixedBulletDamage();
    }

    public override void Fire()
    {
        SniperBullet sniperBullet;
        sniperBullet = GameObjectPoolManager.instance.sniperBulletPool.Get(transform.position + Vector3.up * .15f, 2).GetComponent<SniperBullet>();

        sniperBullet.dir = (targetMonster.transform.position - transform.position).normalized;

        damageTem = damage;
        if (RelicManager.instance.aiLearningCount>0)
        {
            damageTem += RelicManager.instance.sniperBulletAddValue;
        }


        sniperBullet.bulletDamage = damageTem;
        sniperBullet.penetrateTime = penetrateTime;
        AudioManager.PlayClip("sniper");


        GameObject sniperMuzzleEffect = Instantiate(EffectManager.instance.sniperMuzzleEffect);
        sniperMuzzleEffect.transform.position = sniperBullet.transform.position;
        sniperMuzzleEffect.transform.rotation = Quaternion.LookRotation(sniperBullet.dir);
        Destroy(sniperMuzzleEffect, 2);

        if (RelicManager.instance.engineOilCount > 0)
        {
            attackSpeed += RelicManager.instance.engineOilSpeedFactor;
            WeaponsManager.instance.UpdateGunControllerEngineOilBuffs();
        }

    }
}
