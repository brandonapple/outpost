using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunedMissile : MonoBehaviour
{
    public Vector3 targetPos;

    Vector3 dir;
    float speed;
    private void Start()
    {
        dir = new Vector3(Random.value - .5f, Random.value * .5f, Random.value - .5f);
        dir = dir.normalized;
    }

    private void FixedUpdate()
    {
        dir = Vector3.Lerp(dir, (targetPos - transform.position).normalized, .1f);

        if (speed < 5)
        {
            speed += .1f;
        }
        transform.position += dir * speed * Time.deltaTime * 4;

        if (Vector3.Distance(transform.position,targetPos)<.5f)
        {
            Explosion();   
        }

    }
    void Explosion()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<1)
            {
                float damageValue = monster.lifeMax * .75f;
                if (damageValue <= 5) damageValue = 5;
                monster.Hitted(damageValue);
                monster.Stuned();
            }
        }
        EffectManager.instance.SpawnEffect("stunedMissileExplosion",transform.position,Quaternion.identity);

        Destroy(gameObject);
    }
}
