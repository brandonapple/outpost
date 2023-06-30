using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLaserBullet : MonoBehaviour
{
    public Monster targetMonster;
    float timer;
    LineRenderer lineRenderer;

    public bool spike;
    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        if (targetMonster != null)
        {
            transform.position = targetMonster.transform.position;
        }
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth =0;
    }
    private void Update()
    {
        lineRenderer.startWidth = .05f * timer;
        lineRenderer.endWidth = .05f * timer;

        if (targetMonster!=null)
        {
           transform.position = targetMonster.transform.position;
        }

        timer += Time.deltaTime;

        if (timer>1)
        {
            timer = 0;
            if (targetMonster!=null)
            {
                if (spike)
                {
                    targetMonster.Hitted(targetMonster.lifeMax);
                }
                else
                {
                    targetMonster.Hitted(10);
                }
            }
            EffectManager.instance.SpawnEffect("topLaserHitEffect",transform.position+Vector3.up*.5f,Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
