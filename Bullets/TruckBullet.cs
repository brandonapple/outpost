using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TruckBullet : MonoBehaviour
{
    public float speed;
    Vector3 dir;
    float timer;

    public GameObject truck;
    public GameObject[] wheels;
    float shakeTimer;

    public float truckSpeed;
    public bool truckMoving;
    public AudioSource truckAudioSource;

    public GameObject smokeEffectRoot;

    public float damage;
    public float speedMultiplier;
    public float radiusMultiplier;
    public float damageRadius;


    private void FixedUpdate()
    {
        switch (MonsterManager.instance.thisGameState)
        {
            case MonsterManager.GameState.MonsterTime:
                if (!truckMoving)
                {
                    truckMoving = true;
                    SmokeOn();
                }
                break;
            case MonsterManager.GameState.MiningTime:
                if (truckMoving)
                {
                    truckMoving = false;
                    SmokeOff();
                }
                break;
            default:
                break;
        }

        dir = Vector3.Cross(Vector3.up, transform.position);
        dir = dir.normalized;
        transform.position += dir * Time.deltaTime * speed * truckSpeed;

        float distanceToBase = Vector3.Distance(transform.position, Base.instance.transform.position);
        Vector3 dirToBase = Base.instance.transform.position - transform.position;
        dirToBase = dirToBase.normalized;

        Monster targetMonster = MonsterManager.cloestMonster(transform.position);

        if (targetMonster)
        {
            float targetDistanceToBase = Vector3.Distance(targetMonster.transform.position, Base.instance.transform.position);
            if (distanceToBase > targetDistanceToBase)
            {
                transform.position += dirToBase * Time.deltaTime * speed * .3f;
            }
            else
            {
                transform.position += dirToBase * Time.deltaTime * speed * -.3f;
            }
           
        }
        
        truckSpeed = Mathf.Lerp(truckSpeed, truckMoving ? 1 : 0, .01f);
        truckAudioSource.volume = truckSpeed;
        truckAudioSource.pitch = 1 + truckSpeed * .25f;
        timer += Time.deltaTime;
        if (timer>.2f)
        {
            timer = 0;
            HurtMonsters();
        }

       

        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(Vector3.forward, 10 * truckSpeed );
            float angle = Vector3.Angle(wheel.transform.right, Vector3.right);
            float a = Mathf.Deg2Rad * angle;
            float offset = Mathf.Sin(a);
            offset = Mathf.Abs(offset);
            float xPos = wheel.transform.localPosition.x;
            wheel.transform.localPosition = new Vector3(xPos, .15f + .05f * offset, 0);

        }
        Vector3 targetPos = (wheels[0].transform.localPosition + wheels[1].transform.localPosition) / 2 + Vector3.up * .15f;
        truck.transform.localPosition = Vector3.Lerp(targetPos, truck.transform.localPosition, .1f);

        Vector3 dirA = wheels[1].transform.position - wheels[0].transform.position;
        Vector3 dirB = -Vector3.Cross(Vector3.forward, dirA);

        if (dir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            truck.transform.localRotation = Quaternion.LookRotation(Vector3.forward, dirB);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            truck.transform.localRotation = Quaternion.LookRotation(Vector3.forward, -dirB);
        }

        shakeTimer += Time.deltaTime * truckSpeed;
        if (shakeTimer > .25f)
        {
            shakeTimer = 0;
            truck.transform.DOShakePosition(.15f, .05f, 1, 1, false, true);
            truck.transform.DOShakeRotation(.15f, 3, 10, 1, true);
        }

    }
    public void HurtMonsters()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        bool hitMonster = false;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<damageRadius)
            {
                monster.Hitted(damage);
                hitMonster = true;

                WeaponDamageSettlementManager.instance.TruckDealDamage(damage);
            }
        }
        if (hitMonster)
        {
            AudioManager.PlayClips("truckHit");
        }
    }
    public void SmokeOn()
    {
        ParticleSystem[] pars = smokeEffectRoot.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem par in pars)
        {
            par.Play();
        }
    }
    public void SmokeOff()
    {

        ParticleSystem[] pars = smokeEffectRoot.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem par in pars)
        {
            par.Stop();
        }
    }
    public void ResetSpeed()
    {
        truckSpeed = 0;
    }
   
}
