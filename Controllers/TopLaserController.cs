using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLaserController : WeaponBase
{
    public float timer;
    private void Start()
    {
        timer = 3 - .1f;
    }
    private void Update()
    {
        if (RelicManager.instance.topLaserCount == 0) return;

        weaponCDIcon.SetCDCircle(timer / 3);

        float speed = Time.deltaTime;
       
        speed *= RelicManager.instance.topLaserControllerSpeed;
        speed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
       
        if (timer > 3)
        {
            List<Monster> randomMonsterList = MonsterManager.randomEnemiesList(RelicManager.instance.topLaserHitEmemiesCount);
            if (randomMonsterList == null) return;
           
            timer = 0;
            Fire();
        }
        else
        {
            timer += speed;
        }

    }

    void Fire()
    {
        List<Monster> randomMonsterList = MonsterManager.randomEnemiesList(RelicManager.instance.topLaserHitEmemiesCount);
        foreach (Monster monster in randomMonsterList)
        {
            TopLaserBullet laserBullet = Instantiate(Resources.Load<TopLaserBullet>("Bullets/topLaserBullet"));
            laserBullet.targetMonster = monster;
        }
    }
}
