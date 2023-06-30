using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : WeaponControllerBase
{
    public SpriteRenderer gunTopSpriteRenderer;
    float damageTem;
    public override void LoadData()
    {
        attackSpeed = DataManager.instance.getCurrentValueByString("gunSpeed");
        catapultTime = (int)DataManager.instance.getCurrentValueByString("gunCatapult");
        damage = DataManager.instance.getCurrentValueByString("gunDamage");

        FixedData();
        FixedBulletDamage();
    }
    public override void Fire()
    {
        if (targetMonster.transform.position.x> transform.position.x)
        {
            gunTopSpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, 0);
            firePoint.transform.localPosition = new Vector3(.3f, firePoint.transform.position.y, 0);
            gunTopSpriteRenderer.transform.DOLocalMoveX(-.05f, .1f);
        }
        else
        {
            gunTopSpriteRenderer.transform.localRotation = Quaternion.Euler(0, 180, 0);
            firePoint.transform.localPosition = new Vector3(-.3f, firePoint.transform.position.y, 0);
            gunTopSpriteRenderer.transform.DOLocalMoveX(.05f, .1f);
        }

        StartCoroutine(GunTopBackToIdlePos());
        IEnumerator GunTopBackToIdlePos()
        {
            yield return new WaitForSeconds(.1f);
            gunTopSpriteRenderer.transform.localPosition = new Vector3(0, gunTopSpriteRenderer.transform.position.y, 0);
        }

        Bullet bullet = GameObjectPoolManager.instance.gunBulletPool.Get(firePoint.transform.position, 2).GetComponent<Bullet>();
        bullet.SetCatapultTime(catapultTime);

        bullet.transform.position = firePoint.transform.position;

        damageTem = damage;
        if (RelicManager.instance.aiLearningCount > 0)
        {
            damageTem += RelicManager.instance.gunBulletAddValue;
        }
        bullet.damage = damageTem;


        bullet.targetMonster = targetMonster;
        bullet.bulletFromPlane = false;

        if (RelicManager.instance.engineOilCount>0)
        {
            attackSpeed += RelicManager.instance.engineOilSpeedFactor;
            WeaponsManager.instance.UpdateGunControllerEngineOilBuffs();
        }

    }
 

   
}
