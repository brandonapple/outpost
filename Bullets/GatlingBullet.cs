using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBullet : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    bool working;
    public float damage;
    private void Awake()
    {
      //  working = true;
       //Destroy(gameObject, 2);
    }
    private void Start()
    {
        working = true;
        Vector3 normalDir = Vector3.Cross(Vector3.up, dir);
        transform.position += normalDir * Random.Range(-.1f, .1f);
    }

    private void OnEnable()
    {
        Start();
    }

    private void FixedUpdate()
    {
        if (!working)
        {
            return;
        }

        transform.position += dir * Time.deltaTime * speed;
        Monster[] monsters = MonsterManager.instance.monsters;
        if (monsters.Length==0)
        {
            working = false;
            //Destroy(gameObject,.5f);
            GetComponent<GameObjectPoolInfo>().RemoveFast();
        }
        foreach (Monster monster in monsters)
        {
            if (monster!=null)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < .5f)
                {
                    //Destroy(gameObject, .5f);
                    GetComponent<GameObjectPoolInfo>().RemoveFast();
                    //monster.Hitted(damage);
                    monster.HittedByBullet(damage, 2);
                    working = false;

                    GameObject explosionEffect = GameObjectPoolManager.instance.bulletHitEffectPool.Get(transform.position, 1);
                    WeaponDamageSettlementManager.instance.GatlingDealDamage(damage);
                }
            }
            
        }
    }
}
