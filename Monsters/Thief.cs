using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Monster
{
    public int stealCoinsCount;
    public ThiefStealCountContainer thiefStealCountContainer;

    public override void Start()
    {
        base.Start();
        thiefStealCountContainer =
            Instantiate(Resources.Load<ThiefStealCountContainer>("Prefab/thiefStealCountContainer"));
        thiefStealCountContainer.transform.position = transform.position + Vector3.up;
        ShowStealCountContainer();
    }
    public override void AttackPoint()
    {
        base.AttackPoint();
      //  Debug.Log("attack point");

        Coin coin = GameObjectPoolManager.instance.coinPool.Get(Base.instance.transform.position + Vector3.up * .5f,10).GetComponent<Coin>();
        coin.moveTargetGameObject = this.gameObject;


    }



    public void StealCoin()
    {
        stealCoinsCount++;
        ShowStealCountContainer();
    }
    public override void FixedUpdate()
    {
        thiefStealCountContainer.transform.position = transform.position + Vector3.up;
        base.FixedUpdate();
    }
    public void ShowStealCountContainer()
    {
        thiefStealCountContainer.ShowCountText(stealCoinsCount);
    }

    public override void MeleeBehaviour()
    {
        if (stealCoinsCount>5)
        {
            transform.position += transform.position.normalized * Time.deltaTime * speed;
            MonsterScaling();
            BackToBase();
            if (distanceToBase>10)
            {
                Escape();
            }
            return;
        }
        base.MeleeBehaviour();
    }



    public override void Dead()
    {
        base.Dead();
        Destroy(thiefStealCountContainer.gameObject);
    }
}
