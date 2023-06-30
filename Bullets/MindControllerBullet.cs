using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControllerBullet : MonoBehaviour
{
    public Monster targetMonster;
    public LineRenderer controllerLine;
    float timer;
    private void Start()
    {
        controllerLine.positionCount = 20;
    }
    private void FixedUpdate()
    {
        if (targetMonster==null)
        {
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < 20; i++)
        {
            float percent = (float)i / 20;
            Vector3 groundPos = Vector3.Lerp(transform.position, targetMonster.transform.position,percent) - transform.position;
            float yPos = Mathf.Sin(percent * Mathf.PI);
            controllerLine.SetPosition(i, new Vector3(groundPos.x, yPos, groundPos.z));
        }

        timer += Time.deltaTime;
        if (timer>1.25f)
        {
            timer = 0;
            float damage = targetMonster.lifeMax * .1f;
            if (damage < .3f) damage = .3f;
            targetMonster.Hitted(damage);
        }
    }
}
