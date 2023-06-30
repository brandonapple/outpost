using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailStoneBullet : MonoBehaviour
{
    public float radius;
    private void Awake()
    {
        Invoke(nameof(FreezeMonsters), 1);
        Destroy(gameObject, 3);

        AudioManager.PlayClip("hailStone");
        Invoke(nameof(HailStoneHitGroundAudio), 0.5f);
    }

    void HailStoneHitGroundAudio()
    {
        AudioManager.PlayClip("hailStoneB");
    }

    public void FreezeMonsters()
    {
        Monster [] monsters = MonsterManager.instance.monsters;
        if (monsters==null)
        {
            return;
        }
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position,monster.transform.position)<radius)
            {
                monster.AddFreezeValue(1);
            }
        }
    }


}
