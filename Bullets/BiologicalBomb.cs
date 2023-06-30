using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologicalBomb : MonoBehaviour
{
    public Monster targetMonster;
    public Vector3 dirToTargetMonster;

    public bool explosioned;
    float timer;
    float attackTimer;


    public float damageRadius;
    public float damageValue;
    public float damageDuration;

    public Vector3 targetPos;

    public LineRenderer rangeLine;
    Monster[] monsters;
    private void Awake()
    {
        rangeLine = GetComponentInChildren<LineRenderer>();
        rangeLine.gameObject.SetActive(false);
    }

    private void Start()
    {
        timer = damageDuration + .25f;
        targetPos = targetMonster.transform.position;
    }

    private void Update()
    {
        if (explosioned)
        {
            timer -= Time.deltaTime;
            if (timer<0)
            {
                Destroy(gameObject,.25f);
            }

            if (timer>0)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer>.2f)
                {
                    attackTimer = 0;
                    monsters = MonsterManager.instance.monsters;
                    if (monsters == null) return;


                    bool playClip = false;
                    foreach (Monster monster in monsters)
                    {
                        if (monster!=null)
                        {
                            if (Vector3.Distance(monster.transform.position, transform.position) < damageRadius)
                            {
                                monster.Hitted(damageValue);
                                monster.Posioned();

                                playClip = true;
                                WeaponDamageSettlementManager.instance.BiologicalBombDealDamage(damageValue);
                            }
                        }
                       
                    }

                    if (playClip)
                    {
                        AudioManager.PlayClip("biologicalBombDealDamage");
                    }
                   
                }
            }

        }
        else
        {
            if (Vector3.Distance(targetPos, transform.position) > .3f)
            {
                dirToTargetMonster = Vector3.Lerp(dirToTargetMonster, (targetPos - transform.position).normalized, .05f);
                transform.position += dirToTargetMonster * Time.deltaTime * 5;
            }
            else
            {
                if (!explosioned)
                {
                    explosioned = true;
                    transform.position = targetPos;
                    Explosion();

                }
            }
        }
    }
    void Explosion()
    {
        GameObject explosionEffect = Instantiate(Resources.Load<GameObject>("Effect/BiologicalBombExplosionEffect"));
        explosionEffect.transform.position = transform.position;
        Destroy(explosionEffect, damageDuration);

        ParticleSystem [] particleSystems = explosionEffect.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem par in particleSystems)
        {
            var shape = par.shape;
            shape.radius = damageRadius;

            var main = par.main;
            par.Stop();
            main.duration = damageDuration;
            par.Play();

        }

        AudioManager.PlayClip("biologicalBombExplosion");

        rangeLine.gameObject.SetActive(true);
        rangeLine.positionCount = 20;
        for (int i = 0; i < 20; i++)
        {
            float angle = Mathf.PI * 2 * i / 19;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) *damageRadius;
            rangeLine.SetPosition(i, pos);
        }
       
    }
   
}
