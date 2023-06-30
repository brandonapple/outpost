using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawnerController : WeaponBase
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
    private void Update()
    {
        
        if (RelicManager.instance.shurikenCount == 0) return;

        weaponCDIcon.SetCDCircle(timer / 3);

        if (timer<3)
        {
            float speed = Time.deltaTime;
           
            speed *= RelicManager.instance.shurikenSpawnerSpeed;
            speed *= RelicManager.instance.hourGlassCDSpeedMultiplier;
            timer += speed;
        }

        Monster targetMonster = MonsterManager.cloestMonster(transform.position);
        if (targetMonster == null) return;

        if (timer>3)
        {
            timer = 0;
            Fire();
        }
    }

    void Fire()
    {
        Monster targetMonster = MonsterManager.cloestMonster(transform.position);
        if (targetMonster == null) return;

        for (int i = 0; i < RelicManager.instance.shurikenCount; i++)
        {
            Shuriken shuriken = Instantiate(Resources.Load<Shuriken>("Bullets/shurikenBullet"));
            shuriken.transform.position = transform.position;
            Vector3 dirToMonster = targetMonster.transform.position - transform.position;
            dirToMonster = dirToMonster.normalized;

            float angleOffset = i * 2 - (RelicManager.instance.shurikenCount-1) * 1;
            Vector3 normalDir = Vector3.Cross(Vector3.up, dirToMonster);

            dirToMonster += normalDir * angleOffset*.1f;
            dirToMonster = dirToMonster.normalized;

            shuriken.dir = dirToMonster;
        }
    }
}
