using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRootPanelPermanentLevel : MonoBehaviour
{
    public static UpgradeRootPanelPermanentLevel instance;

    public int gunDamageLevel;
    public int gunSpeedLevel;
    public int gunRangeLevel;
    public int gunCountLevel;
    public int gunCatapultLevel;

    public int lightningSpeedLevel;
    public int lightningLengthLevel;
    public int lightningDamageLevel;

    public int missileSpeedLevel;
    public int missileDamageLevel;
    public int missileRadiusLevel;
    public int missileCountLevel;

    public int laserCountLevel;
    public int laserSpeedLevel;
    public int laserLengthLevel;
    public int laserDamageLevel;

    public int teslaTowerSpeedLevel;
    public int teslaTowerLengthLevel;
    public int teslaTowerDamageLevel;
    public int teslaTowerTriggerAgainLevel;


    [Header("------- weapon group b -------")]
    public int flameThrowerLengthLevel;
    public int flameThrowerDamageLevel;
    public int flameThrowerAngleLevel;
    public int flameThrowerSpeedLevel;
    public int flameThrowerCountLevel;
   // public bool flameThrowerShortFireOnUnlocked;

    public int sniperSpeedLevel;
    public int sniperDamageLevel;
    public int sniperPenetrateLevel;

    public int biologicalBombRadiusLevel;
    public int biologicalBombDamageLevel;
    public int biologicalBombDurationLevel;

    public int shurikenBAdditionalCountLevel;
    public int shurikenBDamageLevel;
    public int shurikenBFlyingSpeedLevel;
    public int shurikenBSpeedLevel;

    public int magneticFieldSpeedLevel;
    public int magneticFieldDurationLevel;
    public int magneticFieldRadiusLevel;
    public int magneticFieldDragForceLevel;

    [Header("------- weapon group c -------")]
    public int truckSpeedLevel;
    public int truckCountLevel;
    public int truckDamageLevel;
    public int truckRadiusLevel;

    public int freezingAirFreezingSpeedLevel;
    public int freezingAirRotateSpeedLevel;
    public int freezingAirFreezonDurationLevel;
    public int freezingAirDamageLevel;

    public int ballLightningRadiusLevel;
    public int ballLightningSpeedLevel;
    public int ballLightningDurationLevel;
    public int ballLightningDamageLevel;

    public int energyBallSpeedLevel;
    public int energyBallDiffusionRadiusLevel;
    public int energyBallDamageLevel;

    public int gatlingClipCountLevel;
    public int gatlingReloadingSpeedLevel;
    public int gatlingDamageLevel;
    public int gatlingRotateSpeedLevel;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadDatas();
    }
    public void LoadDatas()
    {
        gunDamageLevel = WeaponDataManager.instance.gunDamageLevel;
        gunSpeedLevel = WeaponDataManager.instance.gunSpeedLevel;
        gunRangeLevel = WeaponDataManager.instance.gunRangeLevel;
        gunCountLevel = WeaponDataManager.instance.gunCountLevel;
        gunCatapultLevel = WeaponDataManager.instance.gunCatapultLevel;

        lightningSpeedLevel = WeaponDataManager.instance.lightningSpeedLevel;
        lightningLengthLevel = WeaponDataManager.instance.lightningLengthLevel;
        lightningDamageLevel = WeaponDataManager.instance.lightningDamageLevel;

        missileSpeedLevel = WeaponDataManager.instance.missileSpeedLevel;
        missileDamageLevel = WeaponDataManager.instance.missileDamageLevel;
        missileRadiusLevel = WeaponDataManager.instance.missileRadiusLevel;
        missileCountLevel = WeaponDataManager.instance.missileCountLevel;

        laserCountLevel = WeaponDataManager.instance.laserCountLevel;
        laserSpeedLevel = WeaponDataManager.instance.laserSpeedLevel;
        laserLengthLevel = WeaponDataManager.instance.laserLengthLevel;
        laserDamageLevel = WeaponDataManager.instance.laserDamageLevel;

        teslaTowerSpeedLevel = WeaponDataManager.instance.teslaTowerSpeedLevel;
        teslaTowerLengthLevel = WeaponDataManager.instance.teslaTowerLengthLevel;
        teslaTowerDamageLevel = WeaponDataManager.instance.teslaTowerDamageLevel;
        teslaTowerTriggerAgainLevel = WeaponDataManager.instance.teslaTowerTriggerAgainLevel;

        flameThrowerLengthLevel = WeaponDataManager.instance.flameThrowerLengthLevel;
        flameThrowerDamageLevel = WeaponDataManager.instance.flameThrowerDamageLevel;
        flameThrowerAngleLevel = WeaponDataManager.instance.flameThrowerAngleLevel;
        flameThrowerSpeedLevel = WeaponDataManager.instance.flameThrowerSpeedLevel;
        flameThrowerCountLevel = WeaponDataManager.instance.flameThrowerCountLevel;
      //  flameThrowerShortFireOnUnlocked = WeaponDataManager.instance.flameThrowerShortFireOnUnlocked;

        sniperSpeedLevel = WeaponDataManager.instance.sniperSpeedLevel;
        sniperDamageLevel = WeaponDataManager.instance.sniperDamageLevel;
        sniperPenetrateLevel = WeaponDataManager.instance.sniperPenetrateLevel;

        biologicalBombRadiusLevel = WeaponDataManager.instance.biologicalBombRadiusLevel;
        biologicalBombDamageLevel = WeaponDataManager.instance.biologicalBombDamageLevel;
        biologicalBombDurationLevel = WeaponDataManager.instance.biologicalBombDurationLevel;

        shurikenBAdditionalCountLevel = WeaponDataManager.instance.shurikenBAdditionalCountLevel;
        shurikenBDamageLevel = WeaponDataManager.instance.shurikenBDamageLevel;
        shurikenBFlyingSpeedLevel = WeaponDataManager.instance.shurikenBFlyingSpeedLevel;
        shurikenBSpeedLevel = WeaponDataManager.instance.shurikenBSpeedLevel;

        magneticFieldSpeedLevel = WeaponDataManager.instance.magneticFieldSpeedLevel;
        magneticFieldDurationLevel = WeaponDataManager.instance.magneticFieldDurationLevel;
        magneticFieldRadiusLevel = WeaponDataManager.instance.magneticFieldRadiusLevel;
        magneticFieldDragForceLevel = WeaponDataManager.instance.magneticFieldDragForceLevel;


        truckSpeedLevel = WeaponDataManager.instance.truckSpeedLevel;
        truckCountLevel = WeaponDataManager.instance.truckCountLevel;
        truckDamageLevel = WeaponDataManager.instance.truckDamageLevel;
        truckRadiusLevel = WeaponDataManager.instance.truckRadiusLevel;

        freezingAirFreezingSpeedLevel = WeaponDataManager.instance.freezingAirFreezingSpeedLevel;
        freezingAirRotateSpeedLevel = WeaponDataManager.instance.freezingAirRotateSpeedLevel;
        freezingAirFreezonDurationLevel = WeaponDataManager.instance.freezingAirFreezonDurationLevel;
        freezingAirDamageLevel = WeaponDataManager.instance.freezingAirDamageLevel;

        ballLightningRadiusLevel = WeaponDataManager.instance.ballLightningRadiusLevel;
        ballLightningSpeedLevel = WeaponDataManager.instance.ballLightningSpeedLevel;
        ballLightningDurationLevel = WeaponDataManager.instance.ballLightningDurationLevel;
        ballLightningDamageLevel = WeaponDataManager.instance.ballLightningDamageLevel;

        energyBallSpeedLevel = WeaponDataManager.instance.energyBallSpeedLevel;
        energyBallDiffusionRadiusLevel = WeaponDataManager.instance.energyBallDiffusionRadiusLevel;
        energyBallDamageLevel = WeaponDataManager.instance.energyBallDamageLevel;

        gatlingClipCountLevel = WeaponDataManager.instance.gatlingClipCountLevel;
        gatlingReloadingSpeedLevel = WeaponDataManager.instance.gatlingReloadingSpeedLevel;
        gatlingDamageLevel = WeaponDataManager.instance.gatlingDamageLevel;
        gatlingRotateSpeedLevel = WeaponDataManager.instance.gatlingRotateSpeedLevel;


    }

    public int GetLevelByString(string _string)
    {
        string valueName = _string + "Level";
        if (GetType().GetField(valueName)!=null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            return int.Parse(value);
        }
        else
        {
            return 0;
        }
        
    }
}
