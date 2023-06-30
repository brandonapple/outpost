using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossD : Slime
{
    public bool isLeader;
    public BossD[] bossDs;
    float frameIndex;

    public override void Awake()
    {
        //base.Awake();
        GetSprite();
      //  FixLifeAndSprite();
        SetLifeSlider();

    }
    public override void Start()
    {
        base.Start();
        bossDs = FindObjectsOfType<BossD>();

        if (isLeader)
        {
            SpawnOtherBoss();
        }
        moveDir =- transform.position.normalized;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        frameIndex += Time.deltaTime;
        if (frameIndex>4)
        {
            frameIndex = 0;
            SeparateLife();
        }
    }
    public void SeparateLife()
    {
        float lifeTotal = 0;
        foreach (BossD boss in bossDs)
        {
            lifeTotal += boss.lifeCurrent;
        }
        float lifeAverage = lifeTotal / bossDs.Length;
        foreach (BossD boss in bossDs)
        {
            boss.lifeCurrent = lifeAverage;
        }

    }

    public void SpawnOtherBoss()
    {
        Vector3 norDir = Vector3.Cross(Vector3.up, transform.position.normalized);

        for (int i = -1; i < 2; i+=2)
        {
            BossD bossNew = Instantiate(gameObject).GetComponent<BossD>();
            bossNew.isLeader = false;
            //bossNew.transform.position += new Vector3(Random.value-.5f, 0, Random.value-.5f);
            bossNew.transform.position = transform.position;
            bossNew.transform.position += norDir * i * 2;

        }
    }
}
