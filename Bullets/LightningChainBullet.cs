using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChainBullet : MonoBehaviour
{
    LineRenderer lightningLine;
    public float damage;
    public int monsterTargetCountMax;

   
    private void OnEnable()
    {
        lightningLine = GetComponentInChildren<LineRenderer>();

        List<Monster> targetMonsterList = new List<Monster>();
        List<Monster> monsterList = MonsterManager.monstersInView;
        if (monsterList == null) return;

        Vector3 centerPos = Vector3.zero;


        for (int i = 0; i < monsterTargetCountMax; i++)
        {
            if (monsterList.Count == 0)
            {
                break;
            }
            Monster _cloestMonster = monsterList[0];
            foreach (Monster monster in monsterList)
            {
                if (Vector3.Distance(centerPos, _cloestMonster.transform.position)
                    > Vector3.Distance(centerPos, monster.transform.position))
                {
                    _cloestMonster = monster;
                }
            }
            targetMonsterList.Add(_cloestMonster);

            //_cloestMonster.Hitted(damage);
            _cloestMonster.HittedByElectric(damage);
            _cloestMonster.Stuned();
            EffectManager.instance.SpawnEffect("teslaTowerHitEffect", _cloestMonster.transform.position, Quaternion.Euler(-90, 0, 0));
            centerPos = _cloestMonster.transform.position;

            WeaponDamageSettlementManager.instance.TeslaTowerDealDamage(damage);
            monsterList.Remove(_cloestMonster);
        }

        lightningLine.positionCount = targetMonsterList.Count + 1;
        lightningLine.SetPosition(0, Vector3.zero);
        for (int i = 0; i < targetMonsterList.Count; i++)
        {
            lightningLine.SetPosition(i + 1, targetMonsterList[i].transform.position - transform.position);
        }
        AudioManager.PlayClip("weapon/teslaTower");
    }
   
}
