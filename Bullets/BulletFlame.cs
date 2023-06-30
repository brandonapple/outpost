using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlame : MonoBehaviour
{
    public float damageLength;
    public float damageAngle;
    public float damageWidth;
    public float damageValue;
    float timer;

    public ParticleSystem longFireEffect;
    public ParticleSystem shortFireEffect;

    public bool shortFireOn;
    List<Monster> monsters;
    private void Start()
    {
        SetFlameEffectShape();
    }
    private void OnEnable()
    {
        SetFlameEffectShape();
    }
    void SetFlameEffectShape()
    {
        var shape =longFireEffect.shape;
        shape.arc = damageAngle;
        shape.rotation = new Vector3(0,0,-damageAngle * .5f);

        var shortFireEffectShape = shortFireEffect.shape;
        if (shortFireOn)
        {
            shortFireEffect.Play();
        }
        else
        {
            shortFireEffect.Stop();
        }
        SetRangeLine();
        
        
    }
  
    void SetRangeLine()
    {
        int segmentCount = Mathf.FloorToInt(damageAngle / 5);

        float beginAngle = damageAngle * -.5f;
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        line.positionCount = segmentCount + 1 + 2;

        for (int i = 0; i < line.positionCount -2; i++)
        {
            float angle = (beginAngle + 5 * i) *(Mathf.PI /180);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * damageLength;
            line.SetPosition(i, pos);
        }
        line.SetPosition(line.positionCount - 2, Vector3.zero);


        float angleB = beginAngle * (Mathf.PI / 180);
        line.SetPosition(line.positionCount - 1, new Vector3(Mathf.Cos(angleB), 0, Mathf.Sin(angleB)) * damageLength);

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>.35f)
        {
            timer = 0;
            if (MonsterManager.monstersInView == null) return;

            monsters = MonsterManager.monstersInView;
            foreach (Monster monster in monsters)
            {
                float distance = Vector3.Distance(transform.position, monster.transform.position);
                float angle = Vector3.Angle(transform.right, monster.transform.position - transform.position);

                if (angle<90)
                {
                    if (distance < damageLength && angle < damageAngle * .5f)
                    {
                        monster.Hitted(damageValue);
                        monster.Burned();
                        WeaponDamageSettlementManager.instance.FlameThrowerDealDamage(damageValue);
                    }

                    if (distance < .25f)
                    {
                        monster.Hitted(damageValue);
                        monster.Burned();
                        WeaponDamageSettlementManager.instance.FlameThrowerDealDamage(damageValue);
                    }

                    if (shortFireOn)
                    {
                        if (distance < damageLength * .3f && angle < 90)
                        {
                            monster.Hitted(damageValue * 2);
                            monster.Burned();
                            WeaponDamageSettlementManager.instance.FlameThrowerDealDamage(damageValue);
                        }
                    }
                }
               
            }
        }
       
    }
}
