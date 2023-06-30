using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : WeaponControllerBase
{
    public float damageLength;
    public float damageAngle;
    public bool shortFireEffectOn;
    
    public override void LoadData()
    {
        damageLength = WeaponDataManager.instance.GetCurrentValueByString("flameThrowerLength");
        damage = WeaponDataManager.instance.GetCurrentValueByString("flameThrowerDamage");
        damageAngle = WeaponDataManager.instance.GetCurrentValueByString("flameThrowerAngle");
        shortFireEffectOn = WeaponDataManager.instance.GetUnlockedByString("flameThrowerShortFireOn");
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("flameThrowerSpeed");

       // attackSpeed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
        duration = 2;
        //if (RelicManager.instance.gasolineCount>0)
        //{
        //    duration *= RelicManager.instance.gasolineBurnTimeMultiplier;
        //}
        FixedData();
    }
  
    public override void Fire()
    {
        BulletFlame bulletFlame;
        bulletFlame = GameObjectPoolManager.instance.flameBulletPool.Get(firePoint.transform.position, duration).GetComponent<BulletFlame>();


        Vector3 forwardDir = Vector3.Cross(targetMonster.transform.position - transform.position, Vector3.up);
        bulletFlame.transform.rotation = Quaternion.LookRotation(forwardDir);

        bulletFlame.damageLength = damageLength;
        bulletFlame.damageValue = damage;
        bulletFlame.damageAngle = damageAngle;
        bulletFlame.shortFireOn = shortFireEffectOn;
    }




}
