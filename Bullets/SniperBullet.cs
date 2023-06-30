using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public Vector3 dir;
    public float bulletDamage;

    public int penetrateTime;
    public int penetrateTimeTotal;

    public List<Monster> hittedMonsterList;
    bool working;

    Monster[] monsters;
    

    private void OnEnable()
    {
        hittedMonsterList = new List<Monster>();
        working = true;
        penetrateTimeTotal = penetrateTime;
    }
    
    private void FixedUpdate()
    {
        if (!working) return;
        transform.position += dir * Time.deltaTime * 20;


        monsters = MonsterManager.instance.monsters;
        if (monsters == null) return;

        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<.3f)
            {
                if (!hittedMonsterList.Contains(monster))
                {
                    hittedMonsterList.Add(monster);
                    monster.HittedByBullet(bulletDamage,1);

                    WeaponDamageSettlementManager.instance.SniperDealDamage(bulletDamage);

                    GameObject sniperBulletHitEffect;// = Instantiate(EffectManager.instance.sniperBulletHitEffect);
                    sniperBulletHitEffect = GameObjectPoolManager.instance.sniperBulletHitEffectPool.
                        Get(monster.transform.position + Vector3.up * .5f, 1.5f);


                    if (penetrateTime>=2)
                    {
                      penetrateTime--;
                    }
                    if (penetrateTime<=1)
                    {
                        bulletDamage *= ((float)penetrateTimeTotal -(float) penetrateTime * .5f) / (float)penetrateTimeTotal;
                        working = false;
                        GetComponent<GameObjectPoolInfo>().RemoveFast();
                    }
                }
            }
        }
    }
}
