using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenBLuncherController : WeaponControllerBase
{
    public float shurikenSpeed;
    public override void LoadData()
    {
        bulletCount = (int)WeaponDataManager.instance.GetCurrentValueByString("shurikenBAdditionalCount");
        damage = WeaponDataManager.instance.GetCurrentValueByString("shurikenBDamage");
        shurikenSpeed = WeaponDataManager.instance.GetCurrentValueByString("shurikenBFlyingSpeed");
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("shurikenBSpeed");

        // attackSpeed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
        FixedData();
    }
  
    public override void Fire()
    {
        Vector3 dirToMonster = (targetMonster.transform.position - transform.position).normalized;
        Vector3 dirNormal = Vector3.Cross(Vector3.up, dirToMonster);
        AudioManager.PlayClip("shurikenB");

        Vector3 dirBase = dirToMonster;
        for (int i = 0; i < bulletCount; i++)
        {
            ShurikenBBullet shurikenBBulletNew;//= Instantiate(Resources.Load<ShurikenBBullet>("Bullets/shurikenBBullet"));
            shurikenBBulletNew = GameObjectPoolManager.instance.shurikenBPool.Get(transform.position + Vector3.up * .15f, 5).GetComponent<ShurikenBBullet>();
            //shurikenBBulletNew.transform.position = transform.position + Vector3.up * .15f;
           
            Vector3 dirFixed = dirBase; 
            bool left = (i % 2 == 0);
            if (left)
            {
                dirFixed += -dirNormal * ((i*.5f)*.2f);
            }
            else
            {
                dirFixed += dirNormal * ((i + 1) *.5f * .2f);
            }
            dirFixed = dirFixed.normalized;
            shurikenBBulletNew.dir = dirFixed;
            shurikenBBulletNew.speed = shurikenSpeed;
            shurikenBBulletNew.damageValue = damage;


        }

    }
}
