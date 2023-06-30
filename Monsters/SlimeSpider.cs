using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpider : Slime
{
    public void Dash()
    {
        transform.position += dirToBase * Time.deltaTime * speed * 1.5f;
    }

    public override void MeleeAttackment()
    {
        if (attackRange == 0) attackRange = .75f;
        float attackRangeTemp = attackRange;
        if (currentTargetUnit.GetComponent<Base>()) attackRangeTemp = attackRange + 1;

        if (distanceToBase<1.15f)
        {
            MeleeAttackment();
        }
        else if (distanceToBase>=1.15f && distanceToBase<3)
        {
            Dash();
        }
        else
        {
            MoveToBase();
        }

    }


}
