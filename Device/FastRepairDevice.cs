using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FastRepairDevice : MonoBehaviour
{
    public static FastRepairDevice instance;
    public int monsterDeadCount;

    public TextMeshPro monsterDeadCountTextMeshPro;
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        RenderMonsterDeadCountTextMeshPro();
    }
    public void MonsterDead()
    {
        monsterDeadCount++;
        if (monsterDeadCount > 10)
        {
            monsterDeadCount = 0;
            Base.instance.Heal(5);
       //     Debug.Log("fast repair");
        }
        RenderMonsterDeadCountTextMeshPro();
    }
    public void RenderMonsterDeadCountTextMeshPro()
    {
        monsterDeadCountTextMeshPro.text = monsterDeadCount.ToString() + "/" + "10";
    }
}
