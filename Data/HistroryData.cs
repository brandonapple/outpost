using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


[System.Serializable]
public class HistroryData 
{
    public bool openingAnimationShowed;
    public bool tipUpgradeWithDiamondShowed;
    public bool tipUpgradeWithGoldShowed;
    public bool tipLunchShipShowed;
    public bool tipChangeGameSpeedShowed;
    public bool tipSkipAnimationShowed;


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

    public HistroryData()
    {
        openingAnimationShowed = HistoryManager.openingAnimationShowed;

        tipUpgradeWithDiamondShowed = HistoryManager.tipUpgraedeWithDiamondShowed;
        tipUpgradeWithGoldShowed = HistoryManager.tipUpgradeWithGoldShowed;
        tipLunchShipShowed = HistoryManager.tipLunchShipShowed;
        tipChangeGameSpeedShowed = HistoryManager.tipChangeGameSpeedShowed;
        tipSkipAnimationShowed = HistoryManager.tipSkipAnimationShowed;


        FieldInfo[] fieldInfos = GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.Name.Contains("relic"))
            {
              //  Debug.Log(fieldInfo.Name);
                string valueName = fieldInfo.Name;
                bool value = HistoryManager.instance.GetRelicUnlockStateByStraight(valueName);
                fieldInfo.SetValue(this, value);
            }
        }

    }
    public bool GetValueStraight(string valueName)
    {
        return (bool)GetType().GetField(valueName).GetValue(this);
    }
}
