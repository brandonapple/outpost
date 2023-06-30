using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Vector3 dir;
    Monster targetMonster;

    public float damage;
    public float radius;

    private void Start()
    {
        targetMonster = MonsterManager.randomMonster;
        dir = Vector3.up *2;
    }

    private void OnEnable()
    {
        targetMonster = MonsterManager.randomMonster;
        dir = Vector3.up * 2;
    }
    private void FixedUpdate()
    {
        if (targetMonster==null)
        {
            targetMonster = MonsterManager.randomMonster;
        }

        if (MonsterManager.randomMonster == null)
        {
            GetComponent<GameObjectPoolInfo>().RemoveFast();
            return;
        }

        dir = Vector3.Lerp(dir, dirToTargetMonster, .075f);
        transform.position += dir * Time.deltaTime * 4;
        transform.rotation = Quaternion.LookRotation(dir);


        if (distanceToTargetMonster<.2f)
        {
            transform.position = targetMonster.transform.position;

            GetComponentInChildren<TrailRenderer>().Clear();

            GetComponent<GameObjectPoolInfo>().RemoveFast();
            Monster[] monsters = MonsterManager.monstersInView.ToArray();

            foreach (Monster monster in monsters)
            {
                if (Vector3.Distance(transform.position,monster.transform.position) < radius)
                {
                    monster.Hitted(damage);
                    WeaponDamageSettlementManager.instance.MissileDealDamage(damage);
                }
            }


            EffectManager.instance.SpawnEffect("missileExplosion", transform.position, Quaternion.identity) ;
            GameObjectPoolManager.instance.explosionShadowEffectPool.Get(transform.position, 2).GetComponent<ExplosionShadow>().SetSize(radius *2);
        }
    }
    
    Vector3 dirToTargetMonster
    {
        get
        {
            return (targetMonster.transform.position - transform.position).normalized;
        }
    }
    float distanceToTargetMonster
    {
        get
        {
            return Vector3.Distance(transform.position, targetMonster.transform.position);
        }
    }
}
