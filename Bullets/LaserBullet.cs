using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    float timer;
    public LineRenderer lineA;
    public LineRenderer lineB;

    public Vector3 startPos;
    public Vector3 dir;
    int pointCount;
    public int laserLength;
    public float damage;
    List<Monster> monsters;

   
    private void OnEnable()
    {
        lineA.positionCount = 0;
        lineB.positionCount = 0;

        lineA.transform.localPosition = Vector3.zero;
        lineB.transform.localPosition = Vector3.zero;
        pointCount = 0;

        lineA.gameObject.SetActive(true);

        lineA.positionCount = 2;
        lineA.SetPosition(0, Vector3.zero);

        monsters = MonsterManager.monstersInView;
    }

    private void FixedUpdate()
    {
        if (pointCount>laserLength)
        {
            GetComponent<GameObjectPoolInfo>().RemoveFast();
            return;
        }

        timer += Time.deltaTime;
        if (timer>.01f)
        {
            timer = 0;
           
            lineA.SetPosition(1,  startPos-transform.position);

            startPos += dir.normalized * .2f;
            lineB.positionCount++;
            lineB.SetPosition(lineB.positionCount - 1, startPos -transform.position);

            pointCount++;
            if (pointCount>laserLength)
            {
                lineA.gameObject.SetActive(false);
            }
           
            if (monsters == null) return;

            


            foreach (Monster monster in monsters)
            {
                if (monster != null)
                {
                    if (Vector3.Distance(monster.transform.position, startPos) < .25f)
                    {
                        monster.Hitted(damage);
                        WeaponDamageSettlementManager.instance.LaserDealDamage(damage);
                    }
                }
            }


        }
    }

    public void SetStartPosAndDir(Vector3 _startPos,Vector3 _dir)
    {
        startPos = _startPos;
        dir = _dir;
        startPos -= dir * .2f * laserLength * .5f;
    }

}
