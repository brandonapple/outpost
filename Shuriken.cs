using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public Vector3 dir;
    public float shurikenSpeed;

    private void Start()
    {
        Destroy(gameObject, 5);
    }
    private void FixedUpdate()
    {
        transform.position += dir * Time.deltaTime * shurikenSpeed;

        Monster[] monsters = MonsterManager.instance.monsters;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<1)
            {
                float damageValue = monster.lifeMax * .2f;
                if (damageValue <= 1) damageValue = 1;
                monster.Hitted(damageValue);
            }
        }

    }
}
