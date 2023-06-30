using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezonBullet : MonoBehaviour
{
    private void Start()
    {
        foreach (Monster monster in MonsterManager.instance.monsters)
        {
            //  monster.timeFreezon = true;
            monster.TimeFreezon();
        }
        Invoke(nameof(TimeFreezonOver), 5);
    }



    void TimeFreezonOver()
    {
        foreach (Monster monster in MonsterManager.instance.monsters)
        {
            //monster.timeFreezon = false;
            monster.TimeFreezonOver();
        }
    }
}
