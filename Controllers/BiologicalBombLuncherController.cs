using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologicalBombLuncherController : WeaponControllerBase
{
    public override void LoadData()
    {
        radius = WeaponDataManager.instance.GetCurrentValueByString("biologicalBombRadius");
        damage = WeaponDataManager.instance.GetCurrentValueByString("biologicalBombDamage");
        duration = WeaponDataManager.instance.GetCurrentValueByString("biologicalBombDuration");
        attackSpeed = .2f;

        //attackSpeed *= RelicManager.instance.hourGlassCDSpeedMultiplier;

        //if (RelicManager.instance.rangeAmplifierCount > 0)
        //{
        //    radius *= RelicManager.instance.rangeAmplifierMutiplier;
        //}
        //if (RelicManager.instance.gasolineCount>0)
        //{
        //    duration *= RelicManager.instance.gasolineBurnTimeMultiplier;
        //}
        FixedData();
    }

    public override void Fire()
    {

        BiologicalBomb biologicBomb = Instantiate(Resources.Load<BiologicalBomb>("Bullets/biologicalBomb"));
        biologicBomb.transform.position = transform.position + Vector3.up;

        biologicBomb.targetMonster = targetMonster;
        biologicBomb.damageRadius = radius;
        biologicBomb.damageValue = damage;
        biologicBomb.damageDuration = duration;
    }
}
