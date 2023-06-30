using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawnerController : WeaponControllerBase
{

    int laserLength;
    public override void LoadData()
    {
        attackSpeed = DataManager.instance.getCurrentValueByString("laserSpeed");
        bulletCount = (int)DataManager.instance.getCurrentValueByString("laserCount");
        damage = DataManager.instance.getCurrentValueByString("laserDamage");
        laserLength =(int)DataManager.instance.getCurrentValueByString("laserLength");

        FixedData();
    }
    public override void Fire()
    {
        Monster cloestMonster = MonsterManager.cloestMonster(transform.position);
        if (cloestMonster == null) return;

        List<Monster> randomMonsters = MonsterManager.randomEnemiesList(bulletCount);
        for (int i = 0; i < randomMonsters.Count; i++)
        {
            LaserBullet laserBullet = GameObjectPoolManager.instance.laserBulletPool.Get(firePoint.transform.position, 2).GetComponent<LaserBullet>();
           
            Monster targetMonster = randomMonsters[0];
            if (randomMonsters[i]!=null)
            {
               targetMonster = randomMonsters[i];
            }
           
            Vector3 dirToMonster = targetMonster.transform.position.normalized;
            Vector3 dirNormalToMonster = Vector3.Cross(dirToMonster, Vector3.up);
            Vector3 startPos = targetMonster.transform.position - targetMonster.transform.position.normalized * .2f;

            laserBullet.SetStartPosAndDir(startPos, dirNormalToMonster);
            laserBullet.damage = damage;
            laserBullet.laserLength = laserLength;

        }

    }
   
}
