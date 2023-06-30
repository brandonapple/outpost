using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "monsterLevel",menuName ="ScriptableObject/monsterLevel")]
public class MonsterLevel : ScriptableObject
{
    public MonsterGroup[] monsterGroups;
}



[System.Serializable]
public struct MonsterGroup {
    public Monster monsterPrefab;
    public int monsterCount;
    public string thisSpawnWay;
   
}
