using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Monster targetMonster;
    public Vector3 targetPos;

    public float bulletSpeed;
    public int catapultTime;
    public bool bulletFromPlane;
    public float damage;
    Vector3 dir;
    

    private void FixedUpdate()
    {
        if (targetMonster)
        {
            targetPos = targetMonster.transform.position;
            dir = (targetPos - transform.position).normalized;
        }

        //if (targetMonster==null)
        //{
        //    targetMonster = MonsterManager.cloestMonster(transform.position);
        //    if (targetMonster==null)
        //    {
        //        transform.position += dir * Time.deltaTime * bulletSpeed;
        //        return;
        //    }
        //}

        //  dir = (targetMonster.transform.position - transform.position).normalized ;
        
        transform.position += dir * Time.deltaTime * bulletSpeed;
        if (targetMonster)
        {
            if (distanceToMonster < .2f)
            {
                if (bulletFromPlane)
                {
                    targetMonster.HittedByBullet(targetMonster.lifeMax * .25f + damage, 0);
                }
                else
                {
                    targetMonster.HittedByBullet(damage, 0);
                }

                WeaponDamageSettlementManager.instance.GunDealDamage(damage);
                EffectManager.instance.SpawnEffect("bulletHit", transform.position, Quaternion.identity);
                Catapult();
            }
        }
        else 
        {
            //if (Vector3.Distance(transform.position,targetPos)<.2f)
            //{
            //    EffectManager.instance.SpawnEffect("bulletHit", transform.position, Quaternion.identity);
            //}
        }

       

        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
    float distanceToMonster
    {
        get
        {
            return Vector3.Distance(transform.position, targetMonster.transform.position);
        }
    }

    public void SetCatapultTime(int count)
    {
        catapultTime = count;
    }
    void Catapult()
    {
        if (catapultTime > 0)
        {
            Monster newTargetMonster = MonsterManager.cloestNextMonster(transform.position, targetMonster);
            targetMonster = newTargetMonster;
            catapultTime--;
        }
        else
        {
            GetComponent<GameObjectPoolInfo>().RemoveFast();
        }
    }

   
}
