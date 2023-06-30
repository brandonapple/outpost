using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingController : WeaponControllerBase
{

    public enum GunState { reloading,fire}
    public GunState thisGunState = GunState.reloading;
    float reloadingTimer;
    public float reloadingSpeed;
    public int clipBulletCount;
   
    public int clipBulletCountMax;
    public WeaponCDIcon weaponCDIcon;
    public SpriteRenderer gunSpriteRenderer;

    float damageTem;
    private void Awake()
    {
        weaponCDIcon = Instantiate(Resources.Load<WeaponCDIcon>("Prefab/WeaponCDPercentCircleIcon"));
        weaponCDIcon.transform.parent = transform;
        weaponCDIcon.transform.localPosition = Vector3.zero;
        weaponCDIcon.SetCDCircle(0);

        faceDir = Vector3.forward;
    }
    public override void Start()
    {
        base.Start();
    }
    public override void LoadData()
    {
        attackSpeed = 1;
        clipBulletCountMax =(int)WeaponDataManager.instance.GetCurrentValueByString("gatlingClipCount");
        damage = WeaponDataManager.instance.GetCurrentValueByString("gatlingDamage");
        rotateSpeed = WeaponDataManager.instance.GetCurrentValueByString("gatlingRotateSpeed");
        reloadingSpeed = WeaponDataManager.instance.GetCurrentValueByString("gatlingReloadingSpeed");

        FixedData();
        FixedBulletDamage();

    }
    public override void FixedUpdate()
    {
        targetMonster = MonsterManager.cloestMonster(transform.position + faceDir*2);
        if (targetMonster==null)
        {
            if (thisGunState == GunState.fire)
            {
                //thisGunState = GunState.reloading;
            }

            if (reloadingTimer < 2)
            {
                reloadingTimer += Time.deltaTime;
                weaponCDIcon.SetCDCircle(reloadingTimer / 2);
            }
           
            return;
        }
        RotateToCloestMonster();
        RenderFaceDir(faceDir);

        switch (thisGunState)
        {
            case GunState.reloading:
                reloadingTimer += Time.deltaTime * reloadingSpeed;
                if (reloadingTimer>2)
                {
                    if (Vector3.Angle(faceDir,targetDir)<15)
                    {
                        reloadingTimer = 0;
                        thisGunState = GunState.fire;
                        clipBulletCount = clipBulletCountMax;
                    }
                }

                weaponCDIcon.SetCDCircle(reloadingTimer / 2);
                break;
            case GunState.fire:
                reloadingTimer += Time.deltaTime *attackSpeed;
                if (reloadingTimer>.15f)
                {
                    reloadingTimer = 0;
                    Fire();
                    clipBulletCount--;
                    if (clipBulletCount<=0)
                    {
                        thisGunState = GunState.reloading;

                    }
                }


                if (faceDir.x>0)
                {
                    gunSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                    firePoint.transform.localPosition = new Vector3(0.5f, 0, 0);
                }
                else
                {
                    gunSpriteRenderer.transform.rotation = Quaternion.Euler(0, 180, 0);
                    firePoint.transform.localPosition = new Vector3(-.5f, 0, 0);
                }


                break;
            default:
                break;
        }
    }
    public override void Fire()
    {
        GatlingBullet gatlingBullet = GameObjectPoolManager.instance.gatlingBulletPool.Get(firePoint.transform.position,3).GetComponent<GatlingBullet>();
      //  gatlingBullet.transform.position =firePoint.transform.position;
        gatlingBullet.dir = faceDir;

        damageTem = damage;
        if (RelicManager.instance.aiLearningCount>0)
        {
            damageTem += RelicManager.instance.gatlingBulletAddValue;
        }
        gatlingBullet.damage = damage;

        GameObject muzzleFlash = GameObjectPoolManager.instance.gatlingMuzzleFlashPool.Get(firePoint.transform.position, 2);
        muzzleFlash.transform.position = firePoint.transform.position;
        muzzleFlash.transform.rotation = Quaternion.LookRotation(faceDir);


        if (RelicManager.instance.engineOilCount > 0)
        {
            attackSpeed += RelicManager.instance.engineOilSpeedFactor * .25f;
            WeaponsManager.instance.UpdateGunControllerEngineOilBuffs();
        }

    }
}
