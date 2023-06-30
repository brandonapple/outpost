using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public static HistoryManager instance;

    public static bool openingAnimationShowed;
    public static bool tipUpgraedeWithDiamondShowed;
    public static bool tipUpgradeWithGoldShowed;
    public static bool tipLunchShipShowed;
    public static bool tipChangeGameSpeedShowed;
    public static bool tipSkipAnimationShowed;


    public bool relicAiLearningUnlocked;
    public bool relicAnchorUnlocked;
    public bool relicAnotherBottleUnlocked;
    public bool relicBodyBombUnlocked;
    public bool relicBronzeScalesUnlocked;
    public bool relicCompensationUnlocked;
    public bool relicDiscountTodayUnlocked;
    public bool relicEnergyShieldUnlocked;
    public bool relicEngineOilUnlocked;
    public bool relicEverySevenTimePassUnlocked;
    public bool relicFundsTransferUnlocked;
    public bool relicGasolineUnlocked;
    public bool relicGiveAwayUnlocked;
    public bool relicGoldenBulletUnlocked;
    public bool relicGoldenEggUnlocked;
    public bool relicGunpowderUnlocked;
    public bool relicHammerUnlocked;
    public bool relicHourglassUnlocked;
    public bool relicInsuranceUnlocked;
    public bool relicLoneWolfUnlocked;
    public bool relicMoreDefenceUnlocked;
    public bool relicMoreMonsterValueUnlocked;
    public bool relicMushroomBombUnlocked;
    public bool relicOldDiamondUnlocked;
    public bool relicOverTimePayUnlocked;
    public bool relicPartnerUnlocked;
    public bool relicPiggyBankUnlocked;
    public bool relicRadarUnlocked;
    public bool relicRangeAmplifierUnlocked;
    public bool relicShowCaseUnlocked;
    public bool relicShurikenUnlocked;
    public bool relicSteelPlateUnlocked;
    public bool relicStunedMissileLuncherUnlocked;
    public bool relicSuperBatteryUnlocked;
    public bool relicSuperChargerUnlocked;
    public bool relicSupplyBoxUnlocked;
    public bool relicTelescopeUnlocked;
    public bool relicTexRefundUnlocked;
    public bool relicTopLaserUnlocked;


  //  public RelicUnlockUnit[] relicUnlockedUnits;
   
    private void Awake()
    {
        instance = this;
        LoadData();
    }
  

    [ContextMenu("load data")]
    public void LoadData()
    {
        HistroryData data = SaveSystem.LoadHistoryData();
        if (data == null) return;

        openingAnimationShowed = data.openingAnimationShowed ;
        tipUpgraedeWithDiamondShowed = data.tipUpgradeWithDiamondShowed;
        tipUpgradeWithGoldShowed = data.tipUpgradeWithGoldShowed;
        tipLunchShipShowed = data.tipLunchShipShowed;
        tipChangeGameSpeedShowed = data.tipChangeGameSpeedShowed;
        tipSkipAnimationShowed = data.tipSkipAnimationShowed;

        relicAiLearningUnlocked = data.relicAiLearningUnlocked;

        FieldInfo[] fieldInfos = GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.Name.Contains("relic"))
            {
                fieldInfo.SetValue(this, data.GetValueStraight(fieldInfo.Name));
            }
        }
       
    }
    [ContextMenu("save data")]
    public void SaveData()
    {
        SaveSystem.SaveHistoryData();
        Debug.Log("save data");
    }

    [ContextMenu("reset data")]
    public void ResetData()
    {
        openingAnimationShowed = false;

        tipUpgraedeWithDiamondShowed = false;
        tipUpgradeWithGoldShowed = false;
        tipLunchShipShowed = false;
        tipChangeGameSpeedShowed = false;
        tipSkipAnimationShowed = false;
        ResetAllRelicData();
        
    }

    [ContextMenu("reset all relic data")]
   public void ResetAllRelicData()
    {
        FieldInfo[] fieldInfos = GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.Name.Contains("relic"))
            {
                fieldInfo.SetValue(this, false);
            }
        }
        SaveData();
    }


    public bool GetRelicUnlockedStateByString(string relicName)
    {
        relicName= relicName.ToUpper()[0] + relicName.Substring(1);
        string valueName = "relic" + relicName + "Unlocked";
        if (GetType().GetField(valueName)!=null)
        {
            return (bool)(GetType().GetField(valueName).GetValue(this));
        }
        return true;
    }

    public bool GetRelicUnlockStateByStraight(string valueName)
    {
        return (bool)GetType().GetField(valueName).GetValue(this);
    }


    [ContextMenu("check if new relic unlocked")]
    public void CheckIfNewRelicUnlocked(ShipRoundData shipRoundData)
    {
        RelicDataSO[] relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            if (!relicDataSO.unlocked &&
                relicDataSO.conditionWaveIndex <= shipRoundData.waveIndex &&
                relicDataSO.conditionCoinsCount <= shipRoundData.coinsCount &&
                relicDataSO.conditionDiamondsCount <= shipRoundData.diamondsCount &&
                relicDataSO.conditionRefreshTime <= shipRoundData.refreshTime &&
                relicDataSO.conditionMonsterDeadCount <= shipRoundData.monsterDeadCount &&
                relicDataSO.conditionBaseLifeLowerThanValue <= shipRoundData.baseLifeLowest &&
                relicDataSO.conditionUpgradeTime <= shipRoundData.upgradeTime &&
                relicDataSO.conditionFlameThrowerDamageLevelHigherThan <= shipRoundData.flameThrowerDamageLevel &&
                relicDataSO.conditionRelicGotCount <= shipRoundData.relicGotCount &&
                relicDataSO.conditionBaseLifeMaxLevel <= shipRoundData.baseLifeMaxLevel &&
                relicDataSO.conditionActiveWeaponsCount <= shipRoundData.activewWeaponsCount &&
                relicDataSO.conditionMissileExplosionRadiusLevel <= shipRoundData.missileExplosionRadiusLevel &&
                relicDataSO.conditionLightningFenceDamageLevel <= shipRoundData.lightningFenceDamageLevel &&
                relicDataSO.conditionViewRangeLevel <= shipRoundData.viewRangeLevel &&
                relicDataSO.conditionGunSpeedLevel <= shipRoundData.gunSpeedLevel
                )
            {
                string relicDataSOName = relicDataSO.name.ToUpper()[0] + relicDataSO.name.Substring(1);
                string valueName = "relic" + relicDataSOName + "Unlocked";
                GetType().GetField(valueName).SetValue(this, true);
            }
        }
        SaveData();
    }



    [ContextMenu("got not condition relics")]
    public void GetNoConditionRelics()
    {
        RelicDataSO[] relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            if (relicDataSO.AllConditionsEmpty())
            {
                relicDataSO.unlocked = true;
                string relicDataSOName = relicDataSO.name.ToUpper()[0] + relicDataSO.name.Substring(1);
                string valueName = "relic" + relicDataSOName + "Unlocked";
                GetType().GetField(valueName).SetValue(this, true);
            }
        }
        SaveData();
    }


}



