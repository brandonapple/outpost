using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossB : Monster
{

    [Space(30)]

    public float summonTimer;
    public Monster[] summonMonstersPrefab;

    public override void Awake()
    {
        //base.Awake();
        GetSprite();
        SetLifeSlider();
    }

    public override void FixedUpdate()
    {
        //base.FixedUpdate();
        if (GameManager.instance.thisGameState == GameManager.GameState.gameOver) return;
        UpdateLifeSliderPos();

        summonTimer += Time.deltaTime;
        if (summonTimer > 5)
        {
            summonTimer = -2;
            Invoke(nameof(SummonMonsters), .5f);
        }

        if (summonTimer>0)
        {
            MeleeBehaviour();
        }

    }

    void SummonMonsters()
    {
        for (int i = -4; i < 5; i++)
        {
            Monster monster = Instantiate(summonMonstersPrefab[Random.Range(0, summonMonstersPrefab.Length)]);
            monster.transform.position = transform.position;


            Vector3 dirA = -transform.position.normalized;
            Vector3 dirB = Vector3.Cross(Vector3.up, dirA);

            Vector3 pos = dirA + dirB *i;
            pos = pos.normalized;

            monster.transform.position += pos;
        }
    }
}
