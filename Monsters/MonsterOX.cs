using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOX : Monster
{
    public Vector3 monsterRunDir;
    public override void MeleeBehaviour()
    {
        MonsterScaling();
        transform.position += monsterRunDir * Time.deltaTime * .5f * speed * characterSpeed;
        if (Vector3.Dot(dirToBase, monsterRunDir) < 0
            && distanceToBase > WeaponDataManager.instance.gunRangeValueCurrent + 3)
        {
            Escape();
        }
    }

}
