using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDig : Monster
{
    public enum DigMonsterState { digging,diggingOut,moveOnGround}
    public DigMonsterState thisDigMonsterState = DigMonsterState.digging;
    public override void Start()
    {
        base.Start();
        monsterSpriteRenderer.gameObject.SetActive(false);
        avaliable = false;
    }
    public override void FixedUpdate()
    {
        switch (thisDigMonsterState)
        {
            case DigMonsterState.digging:
                base.FixedUpdate();
                if (distanceToBase < 2)
                {
                    thisDigMonsterState = DigMonsterState.diggingOut;
                    StartCoroutine(DigOutIE());
                    IEnumerator DigOutIE()
                    {
                        EffectManager.instance.SpawnEffect("digOutEffect", transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(1);
                        thisDigMonsterState = DigMonsterState.moveOnGround;
                        monsterSpriteRenderer.gameObject.SetActive(true);
                        avaliable = true;
                    }
                }
                break;
            case DigMonsterState.diggingOut:
                break;
            case DigMonsterState.moveOnGround:
                base.FixedUpdate();
                break;
            default:
                break;
        }
    }

    public override void Hitted(float damage)
    {
        switch (thisDigMonsterState)
        {
            case DigMonsterState.digging:
                break;
            case DigMonsterState.diggingOut:
                break;
            case DigMonsterState.moveOnGround:
                base.Hitted(damage);
                break;
            default:
                break;
        }
    }
}
