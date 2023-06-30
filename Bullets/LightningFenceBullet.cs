using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFenceBullet : MonoBehaviour
{
   public float rangeInside;
   public float rangeOutSide;
   public float damage;

    private void Start()
    {
     //   Destroy(gameObject, 1);
    }
    private void OnEnable()
    {
        DeadlDamage();
    }
    public void DeadlDamage()
    {
        List<Monster> targetMonsters = MonsterManager.monstersInRangeAAndB(rangeInside, rangeOutSide);

       
        foreach (Monster monster in targetMonsters)
        {
            //monster.Hitted(damage);
            monster.HittedByElectric(damage);
         //   monster.Slowed();
            WeaponDamageSettlementManager.instance.LightningDealDamage(damage);
            EffectManager.instance.SpawnEffect("lightningFenceHit", monster.transform.position, Quaternion.identity);
        }

        if (targetMonsters.Count>0)
        {
          AudioManager.PlayClip("weapon/lightning");
        }

    }
}
