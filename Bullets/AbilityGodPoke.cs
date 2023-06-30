using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGodPoke : MonoBehaviour
{
    public float damage;
    private void Awake()
    {
        AudioManager.PlayClip("godPoke");
    }
    private IEnumerator Start()
    {
        Monster[] monsters = MonsterManager.instance.monsters;
        Monster cloestMonster = null;
        if (monsters != null)
        {
            cloestMonster = monsters[0];
            foreach (Monster monster in monsters)
            {
                if (Vector3.Distance(monster.transform.position, transform.position) <
                    Vector3.Distance(cloestMonster.transform.position, transform.position))
                {
                    cloestMonster = monster;
                }
            }
            transform.position = cloestMonster.transform.position;
        }

        cloestMonster.Paralyzed();
        yield return new WaitForSeconds(.5f);
        cloestMonster.Hitted(damage);
        Destroy(gameObject, 3);
    }

   

}
