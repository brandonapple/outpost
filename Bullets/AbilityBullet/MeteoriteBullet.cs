using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteBullet : MonoBehaviour
{
    public GameObject meteorite;
    public bool meteoriteFall;
    public GameObject explosionEffect;

    public float radius;
    public float damage;
    private void Start()
    {
        meteoriteFall = true;
    }
    private void FixedUpdate()
    {
        if (meteoriteFall)
        {
            meteorite.transform.position += (transform.position - meteorite.transform.position).normalized * Time.deltaTime * 2;
            if (Vector3.Distance(transform.position, meteorite.transform.position) < 1)
            {
                meteoriteFall = false;
                Destroy(meteorite, 1);
                explosionEffect.gameObject.SetActive(true);
                MonsterManager.instance.CircleDamage(transform.position, radius, damage);
            }
        }

    }
}
