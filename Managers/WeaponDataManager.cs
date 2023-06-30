using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeaponDataManager : MonoBehaviour
{
    public static WeaponDataManager instance;
    [Header("-----weapons----")]

    public bool weaponGunUnlocked;
    public bool weaponMissileUnlocked;
    public bool weaponLaserUnlocked;
    public bool weaponLightningUnlocked;
    public bool weaponTeslaTowerUnlocked;

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
    public bool weaponLightningOn;
    public bool weaponMissileOn;
    public bool weaponLaserOn;
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

    [Header("-----gun-----")]
    public int gunSpeedLevel = 0;
    public int gunDamageLevel = 0;
    public int gunRangeLevel = 0;
    public int gunCountLevel = 0;
    public int gunCatapultLevel = 0;

    [Header("-----lightning-----")]
    public  int lightningSpeedLevel = 0;
    public  int lightningLengthLevel = 0;
    public  int lightningDamageLevel = 0;

    [Header("-----missile-----")]
    public int missileSpeedLevel = 0;
    public int missileDamageLevel = 0;
    public int missileRadiusLevel = 0;
    public int missileCountLevel = 0;

    [Header("-----laser-----")]
    public int laserCountLevel = 0;
    public int laserSpeedLevel = 0;
    public int laserLengthLevel = 0;
    public int laserDamageLevel = 0;

    [Header("-----tesla tower-----")]
    public int teslaTowerSpeedLevel = 0;
    public int teslaTowerLengthLevel = 0;
    public int teslaTowerDamageLevel = 0;
    public int teslaTowerTriggerAgainLevel = 0;


    [Header("-----flame thrower-----")]
    public int flameThrowerLengthLevel;
    public int flameThrowerDamageLevel;
    public int flameThrowerAngleLevel;
    public int flameThrowerSpeedLevel;
    public int flameThrowerCountLevel;
    public bool flameThrowerShortFireOnUnlocked;

    [Header("-----sniper-----")]
    public int sniperSpeedLevel;
    public int sniperDamageLevel;
    public int sniperPenetrateLevel;

    [Header("-----biological bomb-----")]
    public int biologicalBombRadiusLevel;
    public int biologicalBombDamageLevel;
    public int biologicalBombDurationLevel;

    [Header("-----shuriken b-----")]
    public int shurikenBAdditionalCountLevel;
    public int shurikenBDamageLevel;
    public int shurikenBFlyingSpeedLevel;
    public int shurikenBSpeedLevel;

    [Header("-----magnetic field-----")]
    public int magneticFieldSpeedLevel;
    public int magneticFieldDurationLevel;
    public int magneticFieldRadiusLevel;
    public int magneticFieldDragForceLevel;




    [Header("-----truck luncher-----")]
    public int truckSpeedLevel;
    public int truckCountLevel;
    public int truckDamageLevel;
    public int truckRadiusLevel;

    [Header("-----freezing air luncher")]
    public int freezingAirFreezingSpeedLevel;
    public int freezingAirRotateSpeedLevel;
    public int freezingAirFreezonDurationLevel;
    public int freezingAirDamageLevel;

    [Header("-----ball lightning luncher")]
    public int ballLightningRadiusLevel;
    public int ballLightningSpeedLevel;
    public int ballLightningDurationLevel;
    public int ballLightningDamageLevel;

    [Header("-----energy ball luncher-----")]
    public int energyBallSpeedLevel;
    public int energyBallDiffusionRadiusLevel;
    public int energyBallDamageLevel;


    [Header("-----gatling ")]
    public int gatlingClipCountLevel;
    public int gatlingReloadingSpeedLevel;
    public int gatlingDamageLevel;
    public int gatlingRotateSpeedLevel;

    [Header("-----weapon prices-----")]
    public int weaponGunPrice = 0;
    public int weaponLightningPrice =10;
    public int weaponMissilePrice = 70;
    public int weaponLaserPrice = 500;
    public int weaponTeslaTowerPrice = 1500;

    public int weaponFlameThrowerPrice = 0;
    public int weaponSniperPrice = 20;
    public int weaponBiologicalBombPrice = 100;
    public int weaponShurikenBPrice = 800;
    public int weaponMagneticFieldPrice = 2000;

    public int weaponTruckPrice = 0;
    public int weaponFreezingAirPrice = 30;
    public int weaponBallLightningPrice = 200;
    public int weaponEnergyBallPrice = 1500;
    public int weaponGatlingPrice = 3000;


    [Header("-----weapon price parameter----")]
    public int weaponGunPriceParameter = 1;
    public int weaponLightningPriceParameter = 3;
    public int weaponMissilePriceParameter = 20;
    public int weaponLaserParameter = 150;
    public int weaponTeslaTowerPriceParameter = 800;

    public int weaponFlameThrowerPriceParameter = 2;
    public int weaponSniperPriceParameter = 6;
    public int weaponBiologicalBombPriceParameter = 40;
    public int weaponShurikenBPriceParameter = 300;
    public int weaponMagneticFieldPriceParameter = 1000;

    public int weaponTruckPriceParameter = 3;
    public int weaponFreezingAirPriceParameter = 10;
    public int weaponBallLightningPriceParameter = 70;
    public int weaponEnergyBallPriceParameter =500;
    public int weaponGatlingPriceparatemer =1800;

    public bool equalDamage;

    [Range(.1f,3)]
    public float weaponGroupDamageBalancerA,
     weaponGroupDamageBalancerB,
     weaponGroupDamageBalancerC;

    #region gun

    public  int gunSpeedPrice => (gunSpeedLevel  + 1)* weaponGunPriceParameter;
    public  float gunSpeedValueFunc(int level) => level * .3f + 1;
    public  int gunDamagePrice =>( gunDamageLevel + 2)* weaponGunPriceParameter;
    public  float gunDamageValueFunc(int level) => (level + 2)*.5f * weaponGunPriceParameter * weaponGroupDamageBalancerA;

    public  int gunRangePrice =>( gunRangeLevel + 1)* weaponGunPriceParameter;
    public  float gunRangeValueFunc(int level) => level * .3f + 4;
    public float gunRangeValueCurrent => gunRangeValueFunc(gunRangeLevel);


    public  int gunCountPrice => gunCountLevel * 60 + 70;
    public  int gunCountValueFunc(int level) => level * 1 + 1;


    public  int gunCatapultPrice => gunCatapultLevel * 50 + 100;
    public  int gunCatapultValueFunc(int level) => level + 0;

    #endregion

    #region lightning
    public int lightningSpeedPrice => (lightningSpeedLevel + 2)*weaponLightningPriceParameter;
    public float lightningSpeedValueFunc(int level) => level * .3f + .5f;

    public int lightningLengthPrice => (lightningLengthLevel + 2) * weaponLightningPriceParameter;
    public float lightningLengthValueFunc(int level) => level * .35f + .75f;


    public int lightningDamagePrice => (lightningDamageLevel + 2) * weaponLightningPriceParameter;
    public float lightningDamageValueFunc(int level) => (level +2)*.1f*weaponLightningPriceParameter * weaponGroupDamageBalancerA;

    #endregion

    #region missile

    public  int missileSpeedPrice => (missileSpeedLevel +3)*weaponMissilePriceParameter;
    public float missileSpeedValueFunc(int level) => level * .1f + .25f;

    public  int missileDamagePrice =>( missileDamageLevel +3)* weaponMissilePriceParameter;
    public  float missileDamageValueFunc(int level) => (level +2)*weaponMissilePriceParameter * weaponGroupDamageBalancerA;

    public  int missileRadiusPrice =>( missileRadiusLevel+3) * weaponMissilePriceParameter;
    public  float missileRadiusValueFunc(int level) => level * .2f + .75f;

    public  int missileCountPrice => missileCountLevel * 80 + 220;
    public  int missileCountValueFunc(int level) => level + 1;

    #endregion

    #region laser

    public int laserSpeedPrice => (laserSpeedLevel +3)*weaponLaserParameter;
    public float laserSpeedValueFunc(int level) => level * .15f + .3f;

    public int laserCountPrice => (laserCountLevel + 3) * weaponLaserParameter;
    public int laserCountValueFunc(int level) => level + 1;

    public int laserLengthPrice => (laserLengthLevel + 3) * weaponLaserParameter;
    public float laserLengthValueFunc(int level) => level * 4 + 10;

    public int laserDamagePrice => (laserDamageLevel + 3) * weaponLaserParameter;
    public float laserDamageValueFunc(int level) => (level +2)*.5f*weaponLaserParameter * weaponGroupDamageBalancerA;


    #endregion

    #region teslaTower
    public int teslaTowerSpeedPrice =>(teslaTowerSpeedLevel +2)* weaponTeslaTowerPriceParameter;
    public float teslaTowerSpeedValueFunc(int level) => level * .1f + .15f;

    public int teslaTowerLengthPrice => (teslaTowerLengthLevel + 2) * weaponTeslaTowerPriceParameter;
    public int teslaTowerLengthValueFunc(int level) => level + 3;

    public int teslaTowerDamagePrice => (teslaTowerDamageLevel + 2) * weaponTeslaTowerPriceParameter;
    public float teslaTowerDamageValueFunc(int level) => (level +2)*.75f*weaponTeslaTowerPriceParameter * weaponGroupDamageBalancerA;

    public int teslaTowerTriggerAgainPrice => (teslaTowerTriggerAgainLevel + 2) * weaponTeslaTowerPriceParameter;
    public float teslaTowerTriggerAgainValueFunc(int level) => level * .1f + 0f;

    #endregion




    #region flame thrower
    public int flameThrowerLengthPrice => (flameThrowerLengthLevel + 1)* weaponFlameThrowerPriceParameter;
    public float flameThrowerLengthValueFunc(int level) => level * .5f + 3;
    public int flameThrowerDamagePrice => (flameThrowerDamageLevel + 2)* weaponFlameThrowerPriceParameter;
    public float flameThrowerDamageValueFunc(int level) => (level * .2f + .45f)*weaponFlameThrowerPriceParameter*weaponGroupDamageBalancerB;

    public int flameThrowerAnglePrice => (flameThrowerAngleLevel + 1)*weaponFlameThrowerPriceParameter;
    public float flameThrowerAngleValueFunc(int level) => level * 15 + 20;

    public int flameThrowerSpeedPrice => (flameThrowerSpeedLevel  + 2)*weaponFlameThrowerPriceParameter;
    public float flameThrowerSpeedValueFunc(int level) => level * .1f + .2f;

    public int flameThrowerCountPrice => (flameThrowerCountLevel * 20 + 25)*weaponFlameThrowerPriceParameter;
    public float flameThrowerCountValueFunc(int level) => level * 1 + 1;
        
    public int flameThrowerShortFireOnPrice => 100;
    #endregion

    #region sniper
    public int sniperSpeedPrice => (sniperSpeedLevel + 1)*weaponSniperPriceParameter;
    public float sniperSpeedValueFunc(int level) => level * .1f + .2f;

    public int sniperDamagePrice => (sniperDamageLevel +2) *weaponSniperPriceParameter;
    public float sniperDamageValueFunc(int level) => (level * 2.5f + 5)*weaponSniperPriceParameter * weaponGroupDamageBalancerB;

    public int sniperPenetratePrice => (sniperPenetrateLevel +3)*weaponSniperPriceParameter;
    public float sniperPenetrateValueFunc(int level) => level * 1 + 3;
  

    #endregion

    #region biologicalBomb
    public int biologicalBombRadiusPrice => (biologicalBombRadiusLevel +1 )*weaponBiologicalBombPriceParameter;
    public float biologicalBombRadiusValueFunc(int level) => level * .25f + .75f;
    
    public int biologicalBombDamagePrice => (biologicalBombDamageLevel +2)*weaponBiologicalBombPriceParameter;
    public float biologicalBombDamageValueFunc(int level) => (level * .25f + .75f)*.75f*weaponBiologicalBombPriceParameter * weaponGroupDamageBalancerB;
   

    public int biologicalBombDurationPrice => (biologicalBombDurationLevel +3)*weaponBiologicalBombPriceParameter;
    public float biologicalBombDurationValueFunc(int level) => level * 1 + 2;

   

    #endregion

    #region shuriken B 

    public int shurikenBAdditionalCountPrice => (shurikenBAdditionalCountLevel +1)*weaponShurikenBPriceParameter;
    public float shurikenBAdditionalCountValueFunc(int level) => level * 2 + 1;
   
    public int shurikenBDamagePrice => (shurikenBDamageLevel +2)*weaponShurikenBPriceParameter;
    public float shurikenBDamageValueFunc(int level) => (level * .2f + .6f)*1.25f*weaponShurikenBPriceParameter * weaponGroupDamageBalancerB;
    
    public int shurikenBFlyingSpeedPrice => (shurikenBFlyingSpeedLevel +3)*weaponShurikenBPriceParameter;
    public float shurikenBFlyingSpeedValueFunc(int level) => level * 2 + 3;
   
    public int shurikenBSpeedPrice =>( shurikenBSpeedLevel +4)*weaponShurikenBPriceParameter;
    public float shurikenBSpeedValueFunc(int level) => level * .1f + .2f;
   
    #endregion

    #region magneticField
    public int magneticFieldSpeedPrice =>( magneticFieldSpeedLevel +1)*weaponMagneticFieldPriceParameter;
    public float magneticFieldSpeedValueFunc(int level) => level * .05f + .1f;
  

    public int magneticFieldDurationPrice => (magneticFieldSpeedLevel + 2) * weaponMagneticFieldPriceParameter;
    public float magneticFieldDurationValueFunc(int level) => level * .5f + 1f;
   
    public int magneticFieldRadiusPrice => (magneticFieldSpeedLevel + 3) * weaponMagneticFieldPriceParameter;
    public float magneticFieldRadiusValueFunc(int level) => level * .5f + 1;
    

    public int magneticFieldDragForcePrice => (magneticFieldSpeedLevel + 4) * weaponMagneticFieldPriceParameter;
    public float magneticFieldDragForceValueFunc(int level) => level * 0.15f + .25f;

    public float magneticFieldDamageValueFunc(int level) => 10 * weaponMagneticFieldPriceParameter * weaponGroupDamageBalancerB;


    #endregion



    #region truck
    public int truckSpeedPrice =>Mathf.RoundToInt((.5f + 1 * truckSpeedLevel)*weaponTruckPriceParameter);
    public float truckSpeedValueFunc(int level) => level * 1 + 1.5f;
    
    public int truckDamagePrice =>Mathf.RoundToInt((1 + 1 * truckDamageLevel)* weaponTruckPriceParameter);
    public float truckDamageValueFunc(int level) => (level * 1 + 2)*weaponTruckPriceParameter * weaponGroupDamageBalancerC;

    public int truckRadiusPrice =>Mathf.RoundToInt((1.5f + 1 * truckRadiusLevel)* weaponTruckPriceParameter);
    public float truckRadiusValueFunc(int level) => level * .25f + .5f;

    public int truckCountPrice => (5 + 3 * truckCountLevel)* weaponTruckPriceParameter;
    public float truckCountValueFunc(int level) => level * 1 + 1;

    #endregion

    #region freezing air
    public int freezingAirFreezingSpeedPrice => (freezingAirFreezingSpeedLevel * 1 + 1)*weaponFreezingAirPriceParameter;
    public float freezingAirFreezingSpeedValueFunc(int level) => level * .1f + .1f;

    public int freezingAirFreezonDurationPrice => (freezingAirFreezonDurationLevel * 1 + 2)* weaponFreezingAirPriceParameter;
    public float freezingAirFreezonDurationValueFunc(int level) => level * 1 + 2;

    public int freezingAirRotateSpeedPrice => (freezingAirRotateSpeedLevel * 1 + 3)* weaponFreezingAirPriceParameter;
    public float freezingAirRotateSpeedValueFunc(int level) => level * 1 + 2;

    public int freezingAirDamagePrice =>( freezingAirDamageLevel * 1 + 4)* weaponFreezingAirPriceParameter;
    public float freezingAirDamageValueFunc(int level) => (level * 1 + 2)*weaponFreezingAirPriceParameter * weaponGroupDamageBalancerC;

    #endregion

    #region ball lightning
    public int ballLightningRadiusPrice => (ballLightningRadiusLevel * 1 + 1)*weaponBallLightningPriceParameter;
    public float ballLightningRadiusValueFunc(int level) => level * .25f +1;

    public int ballLightningSpeedPrice =>(ballLightningSpeedLevel * 1 + 2)* weaponBallLightningPriceParameter;
    public float ballLightningSpeedValueFunc(int level) => level * .05f + .1f;
  
    public int ballLightningDurationPrice => (ballLightningDurationLevel * 1 + 3)* weaponBallLightningPriceParameter;
    public float ballLightningDurationValueFunc(int level) => level * 1 + 3;


    public int ballLightningDamagePrice => (ballLightningDamageLevel * 1 + 4)* weaponBallLightningPriceParameter;
    public float ballLightningDamageValueFunc(int level) => (level * 1 + 2)*8*weaponBallLightningPriceParameter * weaponGroupDamageBalancerC;


    #endregion

    #region energy ball 
    public int energyBallSpeedPrice => (energyBallSpeedLevel * 1 + 2)*weaponEnergyBallPriceParameter;
    public float energyBallSpeedValueFunc(int level) => level * .1f + .2f;

    public int energyBallDiffusionRadiusPrice => (energyBallDiffusionRadiusLevel * 1 + 2)* weaponEnergyBallPriceParameter;
    public float energyBallDiffusionRadiusValueFunc(int level) => level * .5f + 1;

    public int energyBallDamagePrice => (energyBallDamageLevel * 1 + 2)*weaponEnergyBallPriceParameter;
    public float energyBallDamageValueFunc(int level) =>( energyBallDamageLevel+2) *3* weaponEnergyBallPriceParameter * weaponGroupDamageBalancerC;

    #endregion

    #region gatling
    public int gatlingClipCountPrice => (gatlingClipCountLevel * 1 + 1)*weaponGatlingPriceparatemer;
    public float gatlingClipCountValueFunc(int level) => level * 10 + 20;
    public int gatlingReloadingSpeedPrice => (gatlingReloadingSpeedLevel * 1 + 2)* weaponGatlingPriceparatemer;
    public float gatlingReloadingSpeedValueFunc(int level) => level * .1f + .2f;
    public int gatlingDamagePrice => (gatlingDamageLevel * 1 + 3)* weaponGatlingPriceparatemer;
    public float gatlingDamageValueFunc(int level) => (level * 1 + 2)*1.5f * weaponGatlingPriceparatemer * weaponGroupDamageBalancerC;
    public int gatlingRotateSpeedPrice => (gatlingRotateSpeedLevel * 1 + 2)* weaponGatlingPriceparatemer;
    public float gatlingRotateSpeedValueFunc(int level) => level * 1 + 2;
    #endregion

    private void Awake()
    {
        instance = this;


        if (equalDamage)
        {
            weaponGunPriceParameter = 1;
            weaponLightningPriceParameter = 1;
            weaponMissilePriceParameter = 1;
            weaponLaserParameter = 1;
            weaponTeslaTowerPriceParameter = 1;

            weaponFlameThrowerPriceParameter = 1;
            weaponSniperPriceParameter = 1;
            weaponBiologicalBombPriceParameter = 1;
            weaponShurikenBPriceParameter = 1;
            weaponMagneticFieldPriceParameter = 1;

            weaponTruckPriceParameter = 1;
            weaponFreezingAirPriceParameter = 1;
            weaponBallLightningPriceParameter = 1;
            weaponEnergyBallPriceParameter = 1;
            weaponGatlingPriceparatemer = 1;
        }
    }
    private void Start()
    {
        LoadData();
    }
    [ContextMenu("load data")]
    public void LoadData()
    {
        WeaponData data = SaveSystem.LoadWeaponData();
        if (data == null) return;

        weaponFlameThrowerUnlocked = data.weaponFlameThrowerUnlocked;
        weaponSniperUnlocked = data.weaponSniperUnlocked;
        weaponBiologicalBombUnlocked = data.weaponBiologicalBombUnlocked;
        weaponShurikenBUnlocked = data.weaponShurikenBUnlocked;
        weaponMagneticFieldUnlocked = data.weaponMagneticFieldUnlocked;

        weaponTruckUnlocked = data.weaponTruckUnlocked;
        weaponFreezingAirUnlocked = data.weaponFreezingAirUnlocked;
        weaponBallLightningUnlocked = data.weaponBallLightningUnlocked;
        weaponEnergyBallUnlocked = data.weaponEnergyBallUnlocked;
        weaponGatlingUnlocked = data.weaponGatlingUnlocked;



        weaponGunOn = data.weaponGunOn;
        weaponMissileOn = data.weaponMissileOn;
        weaponLaserOn = data.weaponLaserOn;
        weaponLightningOn = data.weaponLightningOn;
        weaponTeslaTowerOn = data.weaponTeslaTowerOn;

        weaponFlameThrowerOn = data.weaponFlameThrowerOn;
        weaponSniperOn = data.weaponSniperOn;
        weaponBiologicalBombOn = data.weaponBiologicalBombOn;
        weaponShurikenBOn = data.weaponShurikenBOn;
        weaponMagneticFieldOn = data.weaponMagneticFieldOn;

        weaponTruckOn = data.weaponTruckOn;
        weaponFreezingAirOn = data.weaponFreezingAirOn;
        weaponBallLightningOn = data.weaponBallLightningOn;
        weaponEnergyBallOn = data.weaponEnergyBallOn;
        weaponGatlingOn = data.weaponGatlingOn;


        flameThrowerLengthLevel = data.flameThrowerLengthLevel;
        flameThrowerDamageLevel = data.flameThrowerDamageLevel;
        flameThrowerAngleLevel = data.flameThrowerAngleLevel;
        flameThrowerSpeedLevel = data.flameThrowerSpeedLevel;
        flameThrowerCountLevel = data.flameThrowerCountLevel;
        flameThrowerShortFireOnUnlocked = data.flameThrowerShortFireOnUnlocked;

        sniperSpeedLevel = data.sniperSpeedLevel;
        sniperDamageLevel = data.sniperDamageLevel;
        sniperPenetrateLevel = data.sniperPenetrateLevel;

        biologicalBombRadiusLevel = data.biologicalBombRadiusLevel;
        biologicalBombDamageLevel = data.biologicalBombDamageLevel;
        biologicalBombDurationLevel = data.biologicalBombDurationLevel;

        shurikenBAdditionalCountLevel = data.shurikenBAdditionalCountLevel;
        shurikenBDamageLevel = data.shurikenBDamageLevel;
        shurikenBFlyingSpeedLevel = data.shurikenBFlyingSpeedLevel;
        shurikenBSpeedLevel = data.shurikenBSpeedLevel;

        magneticFieldSpeedLevel = data.magneticFieldSpeedLevel;
        magneticFieldDurationLevel = data.magneticFieldDurationLevel;
        magneticFieldRadiusLevel = data.magneticFieldRadiusLevel;
        magneticFieldDragForceLevel = data.magneticFieldDragForceLevel;

        truckSpeedLevel = data.truckSpeedLevel;
        truckCountLevel = data.truckCountLevel;
        truckDamageLevel = data.truckDamageLevel;
        truckRadiusLevel = data.truckRadiusLevel;

        freezingAirFreezingSpeedLevel = data.freezingAirFreezingSpeedLevel;
        freezingAirRotateSpeedLevel = data.freezingAirRotateSpeedLevel;
        freezingAirFreezonDurationLevel = data.freezingAirFreezonDurationLevel;
        freezingAirDamageLevel = data.freezingAirDamageLevel;

        ballLightningRadiusLevel = data.ballLightningRadiusLevel;
        ballLightningSpeedLevel = data.ballLightningSpeedLevel;
        ballLightningDurationLevel = data.ballLightningDurationLevel;
        ballLightningDamageLevel = data.ballLightningDamageLevel;

        energyBallSpeedLevel = data.energyBallSpeedLevel;
        energyBallDiffusionRadiusLevel = data.energyBallDiffusionRadiusLevel;
        energyBallDamageLevel = data.energyBallDamageLevel;

        gatlingClipCountLevel = data.gatlingClipCountLevel;
        gatlingReloadingSpeedLevel = data.gatlingReloadingSpeedLevel;
        gatlingDamageLevel = data.gatlingDamageLevel;
        gatlingRotateSpeedLevel = data.gatlingRotateSpeedLevel;


        GameData gameData = SaveSystem.LoadGameData();
        if (gameData == null) return;

        weaponGunUnlocked = gameData.weaponGunUnlocked;
        weaponLightningUnlocked = gameData.weaponLightningUnlocked;
        weaponMissileUnlocked = gameData.weaponMissileUnlocked;
        weaponLaserUnlocked = gameData.weaponLaserUnlocked;
        weaponTeslaTowerUnlocked = gameData.weaponTeslaTowerUnlocked;

        gunSpeedLevel = gameData.gunSpeedLevelPermanent;
        gunDamageLevel = gameData.gunDamageLevelPermanent;
        gunRangeLevel = gameData.gunRangeLevelPermanent;
        gunCountLevel = gameData.gunNumLevelPermanent;
        gunCatapultLevel = gameData.gunCatapultLevelPermanent;

        lightningSpeedLevel = gameData.lightningSpeedLevelPermanent;
        lightningLengthLevel = gameData.lightningLengthLevelPermanent;
        lightningDamageLevel = gameData.lightningDamageLevelPermanent;

        missileSpeedLevel = gameData.missileSpeedLevelPermanent;
        missileDamageLevel = gameData.missileDamageLevelPermanent;
        missileRadiusLevel = gameData.missileRadiusLevelPermanent;
        missileCountLevel = gameData.missileCountLevelPermanent;

        laserCountLevel = gameData.laserCountLevelPermanent;
        laserSpeedLevel = gameData.laserSpeedLevelPermanent;
        laserDamageLevel = gameData.laserDamageLevelPermanent;
        laserLengthLevel = gameData.laserLengthLevelPermanent;

        teslaTowerSpeedLevel = gameData.teslaTowerSpeedLevelPermanent;
        teslaTowerLengthLevel = gameData.teslaTowerSpeedLevelPermanent;
        teslaTowerDamageLevel = gameData.teslaTowerDamageLevelPermanent;
        teslaTowerTriggerAgainLevel = gameData.teslaTowerTriggerAgainLevelPermanent;

    }

    [ContextMenu("save data")]
    public void SaveData()
    {
        SaveSystem.SaveWeaponData();
        SaveSystem.SaveGameData();
    }

    [ContextMenu("reset data")]
    public void ResetData()
    {
        Debug.Log("reset unlock ");
        FieldInfo[] fieldInfos = GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
            if (fieldInfo.Name.Contains("Level"))
            {
                GetType().GetField(fieldInfo.Name).SetValue(this, 0);
            }
            else if (fieldInfo.Name.Contains("Unlocked"))
            {
                GetType().GetField(fieldInfo.Name).SetValue(this, false);
            }
            else if (fieldInfo.Name.Contains("On"))
            {
                GetType().GetField(fieldInfo.Name).SetValue(this, false);
            }
        }

        weaponGunUnlocked = false;
        weaponMissileUnlocked = false;
        weaponLaserUnlocked = false;
        weaponLightningUnlocked = false;
        weaponTeslaTowerUnlocked = false;

        weaponFlameThrowerUnlocked = false;
        weaponSniperUnlocked = false;
        weaponBiologicalBombUnlocked = false;
        weaponShurikenBUnlocked = false;
        weaponMagneticFieldUnlocked = false;

        weaponTruckUnlocked = false;
        weaponFreezingAirUnlocked = false;
        weaponBallLightningUnlocked = false;
        weaponEnergyBallUnlocked = false;
        weaponGatlingUnlocked = false;



        SaveData();
    }

    public void DisableAllWeapon()
    {
        FieldInfo[] fieldInfos = GetType().GetFields();
        foreach (FieldInfo fieldInfo in fieldInfos)
        {
             if (fieldInfo.Name.Contains("On"))
            {
                GetType().GetField(fieldInfo.Name).SetValue(this, false);
            }
        }
        SaveData();
    }
    public int GetLevelByString(string _valueName)
    {
        string valueName = _valueName + "Level";
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
    public bool GetUnlockedByString(string _valueName)
    {
        string valueName = _valueName + "Unlocked";
        string value = GetType().GetField(valueName).GetValue(this).ToString();
        return bool.Parse(value);
    }
    public int GetCurrentPriceByString(string _valueName)
    {
        string valueName = _valueName + "Price";
        if (GetType().GetProperty(valueName)!=null)
        {
        //    Debug.Log("get price property");
            string value = GetType().GetProperty(valueName).GetValue(this).ToString();
            return int.Parse(value);
        }
        else if (GetType().GetField(valueName)!=null)
        {
           // Debug.Log("get price field");
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            return int.Parse(value);
        }
        else
        {
            return 0;
        }

    }
    public float GetCurrentValueByString(string _valueName)
    {
        string funcName = _valueName + "ValueFunc";
        int level = GetLevelByString(_valueName);
        object[] levelParm = new object[] { level };

        if (GetType().GetMethod(funcName)!=null)
        {
            string value = GetType().GetMethod(funcName).Invoke(this, levelParm).ToString();
            return float.Parse(value);
        }
        return 0;
      
      
    }
    public float GetNextValueByString(string _valueName)
    {
        string funcName = _valueName + "ValueFunc";
        int level = GetLevelByString(_valueName);
        object[] levelParm = new object[] { level+1};
        if (GetType().GetMethod(funcName) != null)
        {
            string value = GetType().GetMethod(funcName).Invoke(this, levelParm).ToString();
            return float.Parse(value);
        }
        return 0;
    }

    public void UpgradeLevelByString(string _valueName)
    {
        string valueName = _valueName + "Level";
        if (GetType().GetField(valueName)!=null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            int level = int.Parse(value);
            GetType().GetField(valueName).SetValue(this, level + 1);
        }

        string valueNameB = _valueName + "Unlocked";
        if (GetType().GetField(valueNameB)!=null)
        {
            GetType().GetField(valueNameB).SetValue(this, true);

            if (UpgradeRootPanel.instance.GetActiveWeaponsCount()<5)
            {
                SwitchWeaponByName(_valueName, true);
            }
          
        }
      
      

    }
    public void UnlockByString(string _valueName)
    {
        string valueName = _valueName + "Unlocked";
        GetType().GetField(valueName).SetValue(this, true);
    }

    public bool GetWeaponOnOffStateByString(string _valueName)
    {
        string valueName ="weapon"+ _valueName.ToString() + "On";
      //  Debug.Log(valueName);
        if (GetType().GetField(valueName).GetValue(this)!=null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            return bool.Parse(value);
        }
        else
        {
            return false;
        }
     
    }

    public void SwitchWeaponByName(string _valueName,bool _value)
    {
        string valueName =_valueName + "On";
        GetType().GetField(valueName).SetValue(this, _value);
    }
}
