using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomBomb : MonoBehaviour
{
    int frameIndex = 0;
    private void FixedUpdate()
    {
        frameIndex++;
        if (frameIndex<5)
        {
            return;
        }
        frameIndex = 0;

        Monster[] monsters = MonsterManager.instance.monsters;
        if (monsters.Length == 0) return;
        Monster cloestMonster = monsters[0];
        cloestMonster = MonsterManager.cloestMonster(transform.position);

        if (cloestMonster==null)
        {
            return;
        }

        if (Vector3.Distance(cloestMonster.transform.position,transform.position)<1)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        Destroy(gameObject);
        EffectManager.instance.SpawnEffect("mushRoomExplosion",transform.position,Quaternion.identity);

        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<2)
            {
                float damageValue = monster.lifeMax * .5f;
                if (damageValue <= 3) damageValue = 3;
                monster.Hitted(damageValue);
                monster.Posioned();
            }
        }

    }
}
