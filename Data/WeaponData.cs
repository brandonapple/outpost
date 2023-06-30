using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


[System.Serializable]
public class WeaponData 
{
    public bool weaponFlameThrowerUnlocked;
    public bool weaponSniperUnlocked;
    public bool weaponBiologicalBombUnlocked;
    public bool weaponShurikenBUnlocked;
    public bool weaponMagneticFieldUnlocked;

    public bool weaponTruckUnlocked;
    public bool weaponFreezingAirUnlocked;
    public bool weaponBallLightningUnlocked;
    public bool weaponEnergyBallUnlocked;
    public bool weaponGatlingUnlocked;



    [Header("-----weapons on off -----")]
    public bool weaponGunOn;
    public bool weaponMissileOn;
    public bool weaponLaserOn;
    public bool weaponLightningOn;
    public bool weaponTeslaTowerOn;

    public bool weaponFlameThrowerOn;
    public bool weaponSniperOn;
    public bool weaponBiologicalBombOn;
    public bool weaponShurikenBOn;
    public bool weaponMagneticFieldOn;

    public bool weaponTruckOn;
    public bool weaponFreezingAirOn;
    public bool weaponBallLightningOn;
    public bool weaponEnergyBallOn;
    public bool weaponGatlingOn;





    public int gunSpeedLevel;
    public int gunDamageLevel;
    public int gunRangeLevel;
    public int gunCountLevel;
    public int gunCatapultLevel;

    public int flameThrowerLengthLevel;
    public int flameThrowerDamageLevel;
    public int flameThrowerAngleLevel;
    public int flameThrowerSpeedLevel;
    public int flameThrowerCountLevel;
    public bool flameThrowerShortFireOnUnlocked;

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

    public WeaponData()
    {
        weaponFlameThrowerUnlocked = WeaponDataManager.instance.weaponFlameThrowerUnlocked;
        weaponSniperUnlocked = WeaponDataManager.instance.weaponSniperUnlocked;
        weaponBiologicalBombUnlocked = WeaponDataManager.instance.weaponBiologicalBombUnlocked;
        weaponShurikenBUnlocked = WeaponDataManager.instance.weaponShurikenBUnlocked;
        weaponMagneticFieldUnlocked = WeaponDataManager.instance.weaponMagneticFieldUnlocked;

        weaponTruckUnlocked = WeaponDataManager.instance.weaponTruckUnlocked;
        weaponFreezingAirUnlocked = WeaponDataManager.instance.weaponFreezingAirUnlocked;
        weaponBallLightningUnlocked = WeaponDataManager.instance.weaponBallLightningUnlocked;
        weaponEnergyBallUnlocked = WeaponDataManager.instance.weaponEnergyBallUnlocked;
        weaponGatlingUnlocked = WeaponDataManager.instance.weaponGatlingUnlocked;


        weaponGunOn = WeaponDataManager.instance.weaponGunOn;
        weaponMissileOn = WeaponDataManager.instance.weaponMissileOn;
        weaponLaserOn = WeaponDataManager.instance.weaponLaserOn;
        weaponLightningOn = WeaponDataManager.instance.weaponLightningOn;
        weaponTeslaTowerOn = WeaponDataManager.instance.weaponTeslaTowerOn;

        weaponFlameThrowerOn = WeaponDataManager.instance.weaponFlameThrowerOn;
        weaponSniperOn = WeaponDataManager.instance.weaponSniperOn;
        weaponBiologicalBombOn = WeaponDataManager.instance.weaponBiologicalBombOn;
        weaponShurikenBOn = WeaponDataManager.instance.weaponShurikenBOn;
        weaponMagneticFieldOn = WeaponDataManager.instance.weaponMagneticFieldOn;

        weaponTruckOn = WeaponDataManager.instance.weaponTruckOn;
        weaponFreezingAirOn = WeaponDataManager.instance.weaponFreezingAirOn;
        weaponBallLightningOn = WeaponDataManager.instance.weaponBallLightningOn;
        weaponEnergyBallOn = WeaponDataManager.instance.weaponEnergyBallOn;
        weaponGatlingOn = WeaponDataManager.instance.weaponGatlingOn;


        flameThrowerLengthLevel = WeaponDataManager.instance.flameThrowerLengthLevel;
        flameThrowerDamageLevel = WeaponDataManager.instance.flameThrowerDamageLevel;
        flameThrowerAngleLevel = WeaponDataManager.instance.flameThrowerAngleLevel;
        flameThrowerSpeedLevel = WeaponDataManager.instance.flameThrowerSpeedLevel;
        flameThrowerCountLevel = WeaponDataManager.instance.flameThrowerCountLevel;
        flameThrowerShortFireOnUnlocked = WeaponDataManager.instance.flameThrowerShortFireOnUnlocked;

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
}
