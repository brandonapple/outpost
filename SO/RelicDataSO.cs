using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


[CreateAssetMenu(fileName ="relic data so",menuName ="ScriptableObject/relic data so")]
public class RelicDataSO : ScriptableObject
{
    public string relicName;
    public string relicNameChinese;
   // public string relicDetail;
    public Sprite relicSprite;
    public bool inTest;

    [Space(20)]
    public string relicDescriptionEnglish;
    public string relicDescriptionChinese;
   // public string relicDescription;
    [Space(20)]
    public string specialValueNameString;
    public bool percentFormat;
    public float addValue;

    [Space(20)]
    public bool plus;
    public bool canPlus;

    [Space(20)]
    public bool unlocked;
    public bool canGotInThisRound;



    [Header("-----unlocked conditions-----")]
    public int conditionWaveIndex;
    public int conditionCoinsCount;
    public int conditionDiamondsCount;
    public int conditionRefreshTime;
    public int conditionMonsterDeadCount;
    public int conditionBaseLifeLowerThanValue;
    public int conditionUpgradeTime;
    public int conditionFlameThrowerDamageLevelHigherThan;
    public int conditionRelicGotCount;
    public int conditionBaseLifeMaxLevel;
    public int conditionActiveWeaponsCount;
    public int conditionMissileExplosionRadiusLevel;
    public int conditionLightningFenceDamageLevel;
    public int conditionViewRangeLevel;
    public int conditionGunSpeedLevel;



    public bool AllConditionsEmpty()
    {
        FieldInfo[] fieldInfos = GetType().GetFields();
       
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.Name.Contains("condition"))
            {
                if ((int)fieldInfo.GetValue(this)>0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public string UnlockedConditionString()
    {
        string content = "";
        if (conditionWaveIndex > 0)
            content = "抵达的波数高于 " + conditionWaveIndex.ToString();
        else if (conditionCoinsCount > 0)
            content = "获得多余的金币数高于 " + conditionCoinsCount.ToString();
        else if (conditionDiamondsCount > 0)
            content = "获得多余的钻石数高于 " + conditionDiamondsCount;
        else if (conditionRefreshTime > 0)
            content = "一轮游戏中刷新次数超过 "+conditionRefreshTime;
        else if (conditionMonsterDeadCount > 0)
            content = "一轮游戏中击杀的怪物超过 "+conditionMonsterDeadCount;
        else if (conditionBaseLifeLowerThanValue > 0)
            content = "一轮游戏中基地血量低于 " + conditionBaseLifeLowerThanValue;
        else if (conditionUpgradeTime > 0)
            content = "一轮游戏中升级次数高于 " +conditionUpgradeTime;
        else if (conditionFlameThrowerDamageLevelHigherThan > 0)
            content = "喷火器伤害等级高于 " +conditionFlameThrowerDamageLevelHigherThan;
        else if (conditionRelicGotCount > 0)
            content = "一轮游戏中补给获得个数多于 " + conditionRelicGotCount;
        else if (conditionBaseLifeMaxLevel > 0)
            content = "一轮游戏中基地血量高于 " + conditionBaseLifeMaxLevel ;
        else if (conditionActiveWeaponsCount > 0)
            content = "激活的武器数量多余 " +conditionActiveWeaponsCount;
        else if (conditionMissileExplosionRadiusLevel > 0)
            content = "导弹爆炸半径等级高于 " + conditionMissileExplosionRadiusLevel ;
        else if (conditionLightningFenceDamageLevel > 0)
            content = "电流伤害等级高于 " + conditionLightningFenceDamageLevel;
        else if (conditionViewRangeLevel > 0)
            content = "视野范围等级高于 " + conditionViewRangeLevel;
        else if (conditionGunSpeedLevel > 0)
            content = "机枪速度等级高于 " + conditionGunSpeedLevel;



        return content;
    }
}
