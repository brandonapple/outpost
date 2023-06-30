using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenBBullet : MonoBehaviour
{
    public Vector3 dir;
   
    public float speedChangeSpeed;
    float timer;
    float attackTimer;

    public SpriteRenderer shurikenSpriteRenderer;


    public float speed;
    public float damageValue;


    public AnimationCurve speedCurve;

    public Vector3 newHitPos;
    Vector3 lastHitPos;
  
    private void OnEnable()
    {
        dir = dir.normalized;
        lastHitPos = transform.position;
        timer = 0;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime * speedChangeSpeed;
        if (timer<.5f)
        {
            speed = Mathf.Lerp(10, speed, .1f);
        }
        else if (timer>=.5f && timer<= 1.5f)
        {
            speed = Mathf.Lerp(1f, speed, .025f);
        }
        else
        {
            speed = Mathf.Lerp(-10, speed, .1f);
        }
        transform.position += dir * Time.deltaTime * speed ;
       




        if (timer>= 2)
        {
            GetComponent<GameObjectPoolInfo>().RemoveFast();
        }
        shurikenSpriteRenderer.transform.Rotate(new Vector3(0, 0, 50));

       
        attackTimer += Time.deltaTime;
        if (attackTimer>.1f)
        {
            attackTimer = 0;
            newHitPos = transform.position;
            CheckDamage(newHitPos,lastHitPos);
            lastHitPos = newHitPos;
        }
    }


    void CheckDamage(Vector3 posA,Vector3 posB)
    {
        List<Monster> monsters = MonsterManager.monstersInView;
        if (monsters == null) return;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Dot(posA-monster.transform.position,posB-monster.transform.position)<0)
            {
                Vector3 normalDir = Vector3.Cross(Vector3.up, (posB - posA).normalized);
                if (Mathf.Abs(Vector3.Dot((monster.transform.position - posB), normalDir)) < 1)
                {
                    monster.Hitted(damageValue);
                    WeaponDamageSettlementManager.instance.ShurikenBDealDamage(damageValue);
                    monster.SimpleHittedEffect();

                    GameObject hitEffect = Instantiate(EffectManager.instance.shurikenBHitEffect);
                    hitEffect.transform.position = monster.transform.position + Vector3.up * .5f;
                    hitEffect.transform.rotation = Quaternion.LookRotation(monster.transform.position.normalized);
                    Destroy(hitEffect, 2);
                }
                else if (Mathf.Abs(Vector3.Dot((monster.transform.position - posB), -normalDir)) < 1)
                {
                    monster.Hitted(damageValue);
                    WeaponDamageSettlementManager.instance.ShurikenBDealDamage(damageValue);
                    monster.SimpleHittedEffect();

                    GameObject hitEffect = Instantiate(EffectManager.instance.shurikenBHitEffect);
                    hitEffect.transform.position = monster.transform.position + Vector3.up * .5f;
                    hitEffect.transform.rotation = Quaternion.LookRotation(monster.transform.position.normalized);
                    Destroy(hitEffect, 2);
                }

            }
            if (Vector3.Distance(transform.position,monster.transform.position)<.25f)
            {
                monster.Hitted(damageValue);
                WeaponDamageSettlementManager.instance.ShurikenBDealDamage(damageValue);
                monster.SimpleHittedEffect();

                GameObject hitEffect;
                hitEffect = GameObjectPoolManager.instance.shurikenBHitEffectPool.Get(monster.transform.position + Vector3.up * .5f, 2);

            }
        }

    }
}
