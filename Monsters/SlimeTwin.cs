using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTwin : Slime
{
    public Slime childSlimePrefab;
    bool spawnChildren;
   
    public override void Hitted(float damage)
    {
        base.Hitted(damage);
        CheckSpawnChildren();
    }
    public virtual void CheckSpawnChildren()
    {
        if (spawnChildren) return;
        if (lifePercent <= .15f)
        {
            for (int i = 0; i < 2; i++)
            {
                Slime slime = Instantiate(childSlimePrefab);
                slime.transform.position = transform.position;
                slime.transform.position += (-.5f + i) * Vector3.right * 1;
                slime.moveDir = moveDir;
            }
            spawnChildren = true;
            Hitted(lifeCurrent);
        }
    }

}
