using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLightningChainBullet : MonoBehaviour
{
    public float damageRadius;
    public float damage;

    public List<Monster> monsters ;
    List<Monster> hittedMonsters;
    Vector3 startPos;
    LineRenderer lightningLine;
    
    private void Awake()
    {
        AudioManager.PlayClip("lightningChain");
    }

    private IEnumerator Start()
    {
        startPos = transform.position;
        monsters = new List<Monster>();

        foreach (Monster monster in MonsterManager.instance.monsters)
        {
            monsters.Add(monster);
        }


        
        hittedMonsters = new List<Monster>();

        lightningLine = GetComponentInChildren<LineRenderer>();
        lightningLine.positionCount = 2;
        lightningLine.SetPosition(0, Vector3.zero);
        lightningLine.SetPosition(1, Vector3.zero);

        for (int i = 0; i < 8; i++)
        {
            if (monsters.Count > 0)
            {
                Monster cloestMonster = monsters[0];
                foreach (Monster monster in monsters)
                {
                    if (Vector3.Distance(monster.transform.position, startPos) <
                        Vector3.Distance(cloestMonster.transform.position, startPos))
                    {
                        cloestMonster = monster;
                    }
                }

                startPos = cloestMonster.transform.position;
                hittedMonsters.Add(cloestMonster);
                monsters.Remove(cloestMonster);

                lightningLine.positionCount = hittedMonsters.Count + 1;
                lightningLine.SetPosition(i + 1, cloestMonster.transform.position - transform.position);
                cloestMonster.Paralyzed();
                cloestMonster.HittedByElectric(damage);

                GameObjectPoolManager.instance.lightningHitEffectPool.Get(cloestMonster.transform.position, 1);

                yield return new WaitForSeconds(.1f);
            }
        }

        lightningLine.SetPosition(0, lightningLine.GetPosition(1));
        Destroy(gameObject, 3);

        List<Vector3> pointPoss = new List<Vector3>();
        for (int i = 0; i < lightningLine.positionCount; i++)
        {
            pointPoss.Add(lightningLine.GetPosition(i));
        }
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < lightningLine.positionCount; j++)
            {
                lightningLine.SetPosition(j, pointPoss[j] + Random.insideUnitSphere * .1f);
            }
            yield return new WaitForSeconds(.35f);
        }


    }

}
