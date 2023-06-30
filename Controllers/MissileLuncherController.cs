using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLuncherController : WeaponControllerBase
{
    public override void LoadData()
    {
        attackSpeed = DataManager.instance.getCurrentValueByString("missileSpeed");
        damage = DataManager.instance.getCurrentValueByString("missileDamage");
        radius = DataManager.instance.getCurrentValueByString("missileRadius");

        FixedData();
    }

    public override void Fire()
    {
        GameObject missile = GameObjectPoolManager.instance.missilePool.Get(firePoint.transform.position, 5);
        missile.transform.position = firePoint.transform.position;

        missile.GetComponent<Missile>().damage = damage;
        missile.GetComponent<Missile>().radius = radius;
    }
   
   
}
