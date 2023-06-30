using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldBullet : MonoBehaviour
{

    public float fieldRadius;
    public float fieldDragForce;
    public float fieldLaseDuration;
    public float damage;
    bool working;
    List<Monster> monsters;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.one * fieldRadius;
    }
    private IEnumerator Start()
    {
        transform.localScale = Vector3.one * fieldRadius;
        yield return new WaitForSeconds(.5f);
        working = true;
        yield return new WaitForSeconds(fieldLaseDuration);
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSys in particles)
        {
            particleSys.Stop();
        }


        yield return new WaitForSeconds(1);
        Destroy(gameObject);

        GameObject magneticFieldExplosion = Instantiate(EffectManager.instance.magneticFieldExpolosionEffecct);
        magneticFieldExplosion.transform.position = transform.position;
        magneticFieldExplosion.transform.localScale = Vector3.one * fieldRadius * .25f;
        Destroy(magneticFieldExplosion, 3);

        Monster[] monsters = MonsterManager.instance.monsters;
        foreach (Monster monster in monsters)
        {
            if (monster)
            {
                if (Vector3.Distance(transform.position, monster.transform.position) < fieldRadius)
                {
                    monster.Hitted(damage);
                    WeaponDamageSettlementManager.instance.MagneticFieldDealDamage(damage);
                }
            }
        }

    }
    private void Update()
    {
        if (!working) return;
        monsters = MonsterManager.monstersInView;
        if (monsters == null) return;

        foreach (Monster monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance<fieldRadius && distance>fieldRadius*.25f)
            {
                monster.transform.position += (transform.position - monster.transform.position).normalized * Time.deltaTime * fieldDragForce;
            }
        }
    }

   
}
