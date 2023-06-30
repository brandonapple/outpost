using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallLuncher : WeaponControllerBase
{
    public int energyBallBulletIndex;

    public int whiteBallCount;
    public override void Start()
    {
        base.Start();
    }
    public override void LoadData()
    {
        attackSpeed = WeaponDataManager.instance.GetCurrentValueByString("energyBallSpeed");
        radius = WeaponDataManager.instance.GetCurrentValueByString("energyBallDiffusionRadius");
        damage = WeaponDataManager.instance.GetCurrentValueByString("energyBallDamage");
        whiteBallCount = 1;

        FixedData();
    }
    public override void Fire()
    {

        EnergyBallBullet energyBallBullet = Instantiate(Resources.Load<EnergyBallBullet>("Bullets/energyBallBullet"));
        energyBallBullet.transform.position = transform.position;

        energyBallBullet.damageRadius = radius;
        energyBallBullet.damageValue = damage;

        AudioManager.PlayClip("energyBall");
       
    }

}
