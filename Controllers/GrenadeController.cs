using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    float timer;
    Monster targetMonster;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>3)
        {
            if (targetMonster==null)
            {
                return;
            }

            timer = 0;
            Fire();
        }
    }

    void Fire()
    {
        GrenadeBullet grenadeBullet = Instantiate(Resources.Load<GrenadeBullet>("Bullets/grenadeBullet"));
        grenadeBullet.transform.position = transform.position;
        grenadeBullet.targetMonster = targetMonster;
    }
}
