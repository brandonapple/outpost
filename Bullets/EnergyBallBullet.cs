using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallBullet : MonoBehaviour
{
    public Monster targetMonster;

    public float flyingSpeed;
    public bool willExplosion;

    public GameObject blueEffect;
    public GameObject redEffect;

    public float damageRadius;
    public float damageValue;

    public List<Monster> targetMonsters;
    bool moving = false;

    private void Awake()
    {
        targetMonster = MonsterManager.cloestMonster(transform.position);
        moving = true;
    }
    private void FixedUpdate()
    {
        if (!moving) return;
        if (targetMonster==null)
        {
            targetMonster = MonsterManager.cloestMonster(transform.position);
        }

        if (targetMonster==null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += (targetMonster.transform.position - transform.position).normalized 
            * Time.deltaTime * flyingSpeed;


        if (Vector3.Distance(transform.position,targetMonster.transform.position)<.2f)
        {
          
            AddEnergyMarkMonsters(targetMonster);
            moving = false;
        }
    }

    void AddEnergyMarkMonsters(Monster originalMonster)
    {
        originalMonster.GetEnergyBallMark();

        Monster[] monsters = MonsterManager.instance.monsters;
        List<Monster> monstersWithEnergyMark = new List<Monster>();
        List<Monster> monstersWithoutEnergyMark = new List<Monster>();

        targetMonsters  = new List<Monster>();
        foreach (Monster monster in monsters)
        {
            if (monster.energyBallMarked)
            {
                monstersWithEnergyMark.Add(monster);
            }
            else
            {
                monstersWithoutEnergyMark.Add(monster);
            }
        }


        foreach (Monster monsterA in monstersWithEnergyMark)
        {
            foreach (Monster monsterB in monstersWithoutEnergyMark)
            {
                if (Vector3.Distance(monsterA.transform.position,monsterB.transform.position)<damageRadius)
                {
                    if (!targetMonsters.Contains(monsterB))
                    {
                        targetMonsters.Add(monsterB);
                    }
                }
            }
        }


        foreach (Monster monster in targetMonsters)
        {
            monster.GetEnergyBallMark();
        }

        blueEffect.gameObject.SetActive(false);
        Invoke(nameof(HurtMonstersWithMark), .25f);

    }

   
    void HurtMonstersWithMark()
    {
        Monster[] monsters = MonsterManager.instance.monsters;
        foreach (Monster monster in monsters)
        {
            if (monster.energyBallMarked)
            {
                monster.Hitted(damageValue);
                monster.EnergyBallInvoke();
                WeaponDamageSettlementManager.instance.EnergyBallDealDamage(damageValue);
            }
        }
        Destroy(gameObject);
    }
}
