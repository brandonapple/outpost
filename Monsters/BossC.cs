using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossC : Slime
{

    public enum BossState {
      normal,
      rollIdle,
      rollIng,
      rollOver,
    }

    public BossState thisBossState = BossState.normal;
    float rollTimer;
    Vector3 rollDir;

    public GameObject monsterSpriteRoot;
    bool hitBase;

    public override void Awake()
    {
        //base.Awake();
        GetSprite();
        SetLifeSlider();
    }
    public override void FixedUpdate()
    {
      //  base.FixedUpdate();
      //  if (GameManager.instance.thisGameState == GameManager.GameState.gameOver) return;
        UpdateLifeSliderPos();
        

        switch (thisBossState)
        {
            case BossState.normal:
                if (distanceToBase > 4)
                {
                    MoveToBase();
                }
                else
                {
                    thisBossState = BossState.rollIdle;
                }

                break;
            case BossState.rollIdle:
                rollTimer += Time.deltaTime;
                if (rollTimer>1)
                {
                    rollTimer = 0;
                    thisBossState = BossState.rollIng;
                    rollDir = -transform.position;
                    rollDir = rollDir.normalized;
                    hitBase = false;
                }
                break;
            case BossState.rollIng:
                transform.position += rollDir* Time.deltaTime * 5;
                rollTimer += Time.deltaTime;

                float rotationZ = rollTimer * 3 * 360;
                if (transform.rotation.eulerAngles.y==180)
                {
                    rotationZ *= -1;
                }
                monsterSpriteRoot.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

                if (rollTimer>1.75f)
                {
                    rollTimer = 0;
                    thisBossState = BossState.rollOver;
                    monsterSpriteRoot.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if (!hitBase && distanceToBase<1.5f)
                {
                    hitBase = true;
                    Base.instance.Hitted(5,transform.position);
                }
                break;
            case BossState.rollOver:
                rollTimer += Time.deltaTime;
                if (rollTimer>1)
                {
                    rollTimer = 0;
                    thisBossState = BossState.normal;
                    CheckMonsterSpriteRendererRotation();
                }

                break;
            default:
                break;
        }
    }
}
