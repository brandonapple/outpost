using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLightningBullet : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    float timer;

    public float radius;
    public float duration;
    public float attackSpeed;
    public float damage;

    public GameObject rangeRoot;
    private void Awake()
    {
       
    }
    private void Start()
    {
        rangeRoot.transform.localScale = Vector3.one * radius;
    }
    private void OnEnable()
    {
        Start();

    }
    private void FixedUpdate()
    {
        transform.position += dir * Time.deltaTime * speed;

        timer += Time.deltaTime * attackSpeed;
        if (timer>1)
        {
            timer = 0;
            DealDamage();
        }

    }
    public void DealDamage()
    {
        Monster[] monsters = MonsterManager.instance.monsters;

        bool hitMonster = false;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<radius)
            {
                monster.HittedByElectric(damage);
                LineRenderer line = GameObjectPoolManager.instance.ballLightningLinePool.Get(transform.position,.5f).GetComponent<LineRenderer>();
                line.transform.position = transform.position;
                line.SetPosition(0, Vector3.zero);
                line.SetPosition(1, monster.transform.position - transform.position);
              //  Destroy(line.gameObject, .5f);
                hitMonster = true;

                WeaponDamageSettlementManager.instance.BallLightningDealDamage(damage);
            }
        }
        if (hitMonster)
        {
          AudioManager.PlayClip("ballLightning");
        }

    }

   
}
