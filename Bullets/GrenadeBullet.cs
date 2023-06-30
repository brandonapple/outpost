using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public Monster targetMonster;

    Vector3 dir;
    bool explosioned;
    private void Awake()
    {
        dir = (targetMonster.transform.position - transform.position).normalized + Vector3.up;
    }

    private void Update()
    {
        dir = Vector3.Lerp(dir, (targetMonster.transform.position - transform.position).normalized,.1f);
        if (Vector3.Distance(transform.position,targetMonster.transform.position)<.1f)
        {
            transform.position = targetMonster.transform.position;
            if (!explosioned)
            {
                explosioned = true;
                Explosion();
            }
        }
    }
    void Explosion()
    {
        Monster[] monsters = FindObjectsOfType<Monster>(); 
    }
}
