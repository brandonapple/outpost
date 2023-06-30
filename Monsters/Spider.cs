using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Spider : Monster
{
    bool retreating;
    bool jumping;
    float jumpingTimer;

    [Space(20)]
    public int retreatTime;
    public float attackedSpeedAddValue;
    public int jumpTime;
    public bool flyingIn;
    public bool defenceBullet;

    public override void Start()
    {
        base.Start();

        if (flyingIn)
        {
            float radius = GunRangeManager.instance.rangeRadius * Random.Range(.6f, .9f);
            Vector3 dir = transform.position.normalized;
            Vector3 targetPos = dir * radius;

            transform.position = targetPos;
            monsterSpriteRenderer.transform.localPosition = new Vector3(0, 10, 0);
            monsterSpriteRenderer.transform.DOLocalMoveY(0, 1);
            avaliable = false;
            characterSpeed = .2f;
            Invoke(nameof(FlyingInOver), 1);
        }
        if (jumpTime>0)
        {
            jumpingTimer = Random.value;
        }
    }

    public override void FixLifeAndSprite()
    {
        //base.FixLifeAndSprite();
        MonsterBodyColor monsterBodyColorComponent = GetComponent<MonsterBodyColor>();
        switch (MonsterManager.instance.roundIndex)
        {
            case 3:
                bodyColor = monsterBodyColorComponent.levelOneColor;
                bloodColor = monsterBodyColorComponent.levelOneColor;

                lifeMax = 20 + 20 * lifeMax;
                break;
            case 6:
                bodyColor = monsterBodyColorComponent.levelTwoColor;
                bloodColor = monsterBodyColorComponent.levelTwoColor;

                monsterSpriteRenderer.transform.localScale = Vector3.one * .5f;
                lifeMax = 4000 +  4000 * lifeMax;
                break;
            default:
                break;
        }
        monsterSpriteRenderer.color = bodyColor;
    }
    void FlyingInOver()
    {
        avaliable = true;
        characterSpeed = 1;
    }

    public override void MoveToBase()
    {
        if (jumpTime>0)
        {
            jumpingTimer += Time.deltaTime;
            if (jumpingTimer>1.5f && !jumping)
            {
                jumpTime--;
                jumping = true;
                avaliable = false;
                Vector3 targetPos = transform.position + dirToBase * 1.5f;
                if (Vector3.Distance(targetPos,Base.instance.transform.position)<1.75f)
                {
                    targetPos = transform.position.normalized * 2f;
                }
                StartCoroutine(JumpIE());
                IEnumerator JumpIE()
                {
                    transform.DOMove(targetPos, .8f);
                    monsterSpriteRenderer.transform.DOLocalMoveY(.2f, .2f);
                    yield return new WaitForSeconds(.3f);
                    monsterSpriteRenderer.transform.DOLocalMoveY(0, .2f);
                    yield return new WaitForSeconds(.2f);
                    jumping = false;
                    avaliable = true;
                    jumpingTimer = 0;
                }
            }
        }

        if (jumping)
        {
            return;
        }

        if (retreating)
        {
            MonsterScaling();
            float speedTem = .5f * speed * characterSpeed *2;
            transform.position += -dirToBase * Time.deltaTime * speedTem ;


            if (distanceToBase>GunRangeManager.instance.rangeRadius)
            {
                retreating = false;
                CheckMonsterSpriteRendererRotation();
            }

            return;
        }
        base.MoveToBase();
    }

    public override void Hitted(float damage)
    {
        base.Hitted(damage);
        if (lifePercent < .5f && retreatTime>0)
        {
            retreating = true;
            retreatTime--;
            BackToBase();
        }
        if (attackedSpeedAddValue>0)
        {
            speed += attackedSpeedAddValue;
        }
    }

    public override void HittedByBullet(float damage, int gunIndex)
    {
        if (defenceBullet)
        {
          damage *= .1f;
        }
        base.HittedByBullet(damage, gunIndex);
    }

}
