using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunedMissileLuncher : WeaponBase
{
    public float timer;

    public override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {

        timer = 3 - .1f;
    }
    private void FixedUpdate()
    {
        if (RelicManager.instance.stunedMissileLuncherCount == 0) return;

        weaponCDIcon.SetCDCircle(timer / 3);

        float speed = Time.deltaTime;
        speed *= RelicManager.instance.stunedMissileLuncherSpeedValue;
        speed *= RelicManager.instance.hourGlassCDSpeedMultiplier;

        if (timer>3)
        {
            Monster targetMonster = MonsterManager.cloestMonster(transform.position);
            if (targetMonster == null) return;

            timer = 0;
            StartCoroutine(Fire());
        }
        else
        {
            timer += speed;
        }

    }

    IEnumerator Fire()
    {
        int missileCount = RelicManager.instance.stunedMissileLuncherMissileCount;
        

        Monster targetMonster = MonsterManager.cloestMonster(transform.position);
        Vector3 idlePos = targetMonster.transform.position;


        for (int i = 0; i < missileCount; i++)
        {
            
            StunedMissile stunedMissile = Instantiate(Resources.Load<StunedMissile>("Bullets/stunedMissile"));
            stunedMissile.transform.position = transform.position;

            Vector3 dir = idlePos.normalized * .1f * (i -1);
            idlePos += dir;

            stunedMissile.targetPos = idlePos;
            yield return new WaitForSeconds(.1f);
        }
    }
}
