using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="monsterGroupsData",menuName ="ScriptableObject/monstersGroupData")]
public class MonsterGroupsDataSO : ScriptableObject
{
    public string monsterType;
    public string[] monsterGroups;

   
}
