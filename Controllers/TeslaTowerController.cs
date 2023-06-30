using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTowerController : WeaponControllerBase
{
    public int monsterTargetsCountMax;
    public override void LoadData()
    {
        attackSpeed = DataManager.instance.getCurrentValueByString("teslaTowerSpeed");
        damage = DataManager.instance.getCurrentValueByString("teslaTowerDamage");
        monsterTargetsCountMax =(int) DataManager.instance.getCurrentValueByString("teslaTowerLength");
        triggerAgainChance = DataManager.instance.getCurrentValueByString("teslaTowerTriggerAgain");

        FixedData();
        FixedElectricDamage();
    }
  
    public override void Fire()
    {
        FireAgain();

        float triggerValue = Random.value;
        if (triggerValue <= triggerAgainChance)
        {
            StartCoroutine(TriggerAgainIE());
        }
    }

    public void FireAgain()
    {
        LightningChainBullet lightningChainBullet;//= Instantiate(Resources.Load<LightningChainBullet>("Bullets/lightningChainBullet"));
        lightningChainBullet = GameObjectPoolManager.instance.lightningChainBulletPool.Get(firePoint.transform.position, 1).GetComponent<LightningChainBullet>();
        lightningChainBullet.damage = damage;
        lightningChainBullet.monsterTargetCountMax = monsterTargetsCountMax;
    }
    IEnumerator TriggerAgainIE()
    {
        yield return new WaitForSeconds(interval * .2f);
        FireAgain();
    }
}
