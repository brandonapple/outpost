using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShooterBullet : MonoBehaviour
{
    public float damage;
    public SpriteRenderer bulletSpriteRenderer;
    public Color bulletColor;
   
    Vector3 dir;
    float distance;
    private void Awake()
    {
        bulletSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    private void Start()
    {
        SetColor();
    }
    private void OnEnable()
    {
        SetColor();
    }
    void SetColor()
    {
        bulletSpriteRenderer.color = bulletColor;
    }
    private void FixedUpdate()
    {
        dir = Base.instance.transform.position - transform.position;
        dir = dir.normalized;


        distance = Vector3.Distance(transform.position, Base.instance.transform.position);

        transform.position += dir * Time.deltaTime * .75f * 3;
        if (distance<.85f)
        {
            GetComponent<GameObjectPoolInfo>().RemoveFast();
            Base.instance.Hitted(damage,transform.position);
        }
    }
}
