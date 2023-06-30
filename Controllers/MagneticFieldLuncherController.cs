using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldLuncherController :  WeaponControllerBase
{
    public float dragForce;
    
    public override void LoadData()
    {
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("magneticFieldSpeed");
        duration = WeaponDataManager.instance.GetCurrentValueByString("magneticFieldDuration");
        radius = WeaponDataManager.instance.GetCurrentValueByString("magneticFieldRadius");
        dragForce = WeaponDataManager.instance.GetCurrentValueByString("magneticFieldDragForce");
        damage = WeaponDataManager.instance.GetCurrentValueByString("magneticFieldDamage");

        FixedData();
    }
    public override void Fire()
    {
        MagneticFieldBullet magneticFieldBullet = Instantiate(Resources.Load<MagneticFieldBullet>("Bullets/magneticFieldBullet"));
        magneticFieldBullet.transform.position = targetMonster.transform.position;

        magneticFieldBullet.fieldLaseDuration = duration;
        magneticFieldBullet.fieldRadius = radius;
        magneticFieldBullet.fieldDragForce = dragForce;
        magneticFieldBullet.damage = damage;

    }
}
