using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    float timer;
    Monster targetMonster;
    public void Update()
    {

        timer += Time.deltaTime;
        if (timer>4)
        {
            targetMonster = MonsterManager.cloestMonster(transform.position);
            if (targetMonster)
            {
                timer = 0;
                Fire();
            }
        }
    }


    void Fire()
    {
        SpikeBullet spikeBullet = Instantiate(Resources.Load<SpikeBullet>("Bullets/spikeBullet"));
        spikeBullet.transform.position = transform.position;
        spikeBullet.moveDir = (targetMonster.transform.position - transform.position).normalized;

    }
}
