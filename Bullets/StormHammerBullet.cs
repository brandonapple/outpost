using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHammerBullet : MonoBehaviour
{
    public HammerSingle hammerPrefab;
    public Vector3 hammerCenterPos;


    public int hammerCount;
    public float hammerFlyingSpeed;
    public float hammerDamage;
    public float hammerAbilityRadius;
    private void Awake()
    {
       // hammerCenterPos = transform.position;
    }

    private void Start()
    {

    //    transform.position = Base.instance.transform.position;
        List<Monster> monstersList = new List<Monster>();

        Monster[] monsters = MonsterManager.instance.monsters;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(monster.transform.position,hammerCenterPos)<hammerAbilityRadius)
            {
                monstersList.Add(monster);
            }
        }

        List<Monster> hittedMonsters = new List<Monster>();

        for (int i = 0; i < hammerCount; i++)
        {
            if (monstersList.Count > 0)
            {
                Monster cloestMonster = monstersList[0];
                foreach (Monster monster in monstersList)
                {
                    if (Vector3.Distance(monster.transform.position, hammerCenterPos) <
                        Vector3.Distance(cloestMonster.transform.position, hammerCenterPos))
                    {
                        cloestMonster = monster;
                    }
                }
                monstersList.Remove(cloestMonster);
                hittedMonsters.Add(cloestMonster);
            }
        }


        for (int i = 0; i < hittedMonsters.Count; i++)
        {
            HammerSingle hammer = Instantiate(hammerPrefab,transform);
            hammer.transform.position =Base.instance.transform.position;

            hammer.speed = hammerFlyingSpeed;
            hammer.damage = hammerDamage;
            hammer.targetMonster = hittedMonsters[i];
        }
        hammerPrefab.gameObject.SetActive(false);

        AudioManager.PlayClip("hammerFly");
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hammerCenterPos, hammerAbilityRadius);
    }
}

