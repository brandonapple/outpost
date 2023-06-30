using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime : Monster
{
    public float moveRate =12;
    public float slimeMoveCircleOffset;
    public Vector3 moveDir;
 

    public override void Start()
    {
        base.Start();
        slimeMoveCircleOffset = Random.value;
    }
   
    public override void MoveToBase()
    {
        float f = Mathf.Cos(Time.time *moveRate + slimeMoveCircleOffset*Mathf.PI) * scalingAmplitude;
        if (f > 0)
        {
            if (distanceToBase<5)
            {
                FixeMoveDir();
            }
            else
            {
                moveDir = dirToBase;
            }
           
            transform.position += moveDir * Time.deltaTime  * f *getMoveSpeed *2;
            monsterSpriteRenderer.transform.localScale = new Vector3(1 + f, 1 - f, 1) * .25f;
        }
    }

    public override void MeleeAttackment()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer>1.5f)
        {
            attackTimer = 0;
            StartCoroutine(AttackIE());
        }

        IEnumerator AttackIE()
        {
            monsterSpriteRenderer.transform.DOLocalMove(dirToBase *-.25f, .2f);
            yield return new WaitForSeconds(.2f);
            monsterSpriteRenderer.transform.DOLocalMove(dirToBase * .2f, .2f);
            yield return new WaitForSeconds(.2f);
           
            AttackPoint();

            monsterSpriteRenderer.transform.DOLocalMove(Vector3.zero, .5f);
        }
    }
    public virtual void FixeMoveDir()
    {
        moveDir = Vector3.Lerp(moveDir, dirToBase, .01f);
    }

}
