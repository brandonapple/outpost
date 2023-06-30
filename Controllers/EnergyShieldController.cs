using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldController : WeaponBase
{
    public static EnergyShieldController instance;
    public float timer;
    public bool rechargeEnough;
    public override void Awake()
    {
        base.Awake();
        instance = this;
    }
    private void Start()
    {
        timer = 5 - .1f;
    }
    private void Update()
    {
        if (RelicManager.instance.energyShieldCount == 0) return;

        weaponCDIcon.SetCDCircle(timer / 5);

        if (timer<5)
        {
            float speed = Time.deltaTime;
            speed *= RelicManager.instance.energyShieldRestoreSpeedValue;

            timer +=speed;
            rechargeEnough = false;
        }
        else
        {
            rechargeEnough = true;
        }
    }
    public void EnergyShieldWork()
    {
        timer = 0;
        EnergyShieldBullet bullet = Instantiate(Resources.Load<EnergyShieldBullet>("Bullets/energyShieldBullet"));
        bullet.transform.position = Vector3.zero;
    }
    public void BaseHitted()
    {
        if (rechargeEnough)
        {
            EnergyShieldWork();
        }
    }
}
