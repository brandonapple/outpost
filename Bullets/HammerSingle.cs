using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSingle : MonoBehaviour
{
    public Monster targetMonster;
    public Vector3 targetPos;

    public float speed;
    public float damage;

    public SpriteRenderer hammerSpriteRenderer;

    private void Awake()
    {
      
    }
    private void Start()
    {
        if (targetMonster)
        {
            if (targetMonster.transform.position.x>transform.position.x)
            {
                hammerSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, -55);
            }
            else
            {
                hammerSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 55);
            }
        }
    }
    private void FixedUpdate()
    {
        if (targetMonster)
        {
            transform.position += (targetMonster.transform.position - transform.position).normalized * Time.deltaTime *speed ;
            if (Vector3.Distance(transform.position,targetMonster.transform.position)<.25f)
            {
                Destroy(gameObject);
                targetMonster.Hitted(damage);
                targetMonster.Stuned();
                transform.position = targetMonster.transform.position;
                AudioManager.PlayClip("hammerHit");
            }

        }
        else
        {
            transform.position += (targetPos - transform.position).normalized * Time.deltaTime * speed;
            if (Vector3.Distance(transform.position,targetPos)<.5f)
            {
                Destroy(gameObject);
            }
        }

    }
}
