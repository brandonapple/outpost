using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControllerDevice : MonoBehaviour
{
    float timer;
    private void Start()
    {
        timer = 4;
        
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        
        if (timer>4.5f)
        {
            List<Monster> monsters = MonsterManager.monstersInView;
            if (monsters == null) return;

            List<Monster> notConfusedMonsters = new List<Monster>();
            foreach (Monster monster in monsters)
            {
                if (!monster.confused)
                {
                    notConfusedMonsters.Add(monster);
                }
            }


            if (notConfusedMonsters.Count>0)
            {
                Monster cloestMonster = notConfusedMonsters[0];
                foreach (Monster monster in notConfusedMonsters)
                {
                    if (Vector3.Distance(transform.position,monster.transform.position)< 
                        Vector3.Distance(transform.position,cloestMonster.transform.position))
                    {
                        cloestMonster = monster;
                    }
                }

                Fire(cloestMonster);
                timer = 0;
            }
           
        }

    }

    public void Fire(Monster targetMonster)
    {
        MindControllerBullet mindControllerBullet =
            Instantiate(Resources.Load<MindControllerBullet>("Bullets/mindControllerBullet"));
        mindControllerBullet.transform.position = transform.position;
        mindControllerBullet.targetMonster = targetMonster;
        targetMonster.Confused();

        Destroy(mindControllerBullet.gameObject, 5);
    }
}
