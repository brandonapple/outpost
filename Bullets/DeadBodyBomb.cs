using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyBomb : MonoBehaviour
{
    public float damageValue;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.25f);
        List<Monster> monsters = MonsterManager.monstersInView;
        if (monsters == null)
        {
            yield return null;
        }
        else
        {
            foreach (Monster monster in monsters)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < 2)
                {
                    monster.Hitted(damageValue);
                }
            }
        }
       
        yield return new WaitForSeconds(1);
        GetComponent<GameObjectPoolInfo>().RemoveFast();

    }
}
