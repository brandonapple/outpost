using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


[System.Serializable]
public class GameData 
{
    public int motherShipDiamondCount;
    public int diamondsInventoryCount;


    public int miningModeIndex;
    public int mapIndex;
    public int weaponGroupIndex;



    public bool weaponGunUnlocked;
    public bool weaponMissileUnlocked;
    public bool weaponLaserUnlocked;
    public bool weaponLightningUnlocked;
    public bool weaponTeslaTowerUnlocked;
    public bool miningIgnoreMonsterPermanent;

    public int gunSpeedLevelPermanent;
    public int gunDamageLevelPermanent;
    public int gunSplitLevelPermanent;
    public int gunRangeLevelPermanent;
    public int gunNumLevelPermanent;
    public int gunPenetrateLevelPermanent;
    public int gunCatapultLevelPermanent;

    public int missileSpeedLevelPermanent;
    public int missileDamageLevelPermanent;
    public int missileRadiusLevelPermanent;
    public int missileCountLevelPermanent;

    public int lightningSpeedLevelPermanent;
    public int lightningLengthLevelPermanent;
    public int lightningDamageLevelPermanent;

    public int laserCountLevelPermanent;
    public int laserSpeedLevelPermanent;
    public int laserDamageLevelPermanent;
    public int laserLengthLevelPermanent;



    public int teslaTowerSpeedLevelPermanent;
    public int teslaTowerLengthLevelPermanent;
    public int teslaTowerDamageLevelPermanent;
    public int teslaTowerTriggerAgainLevelPermanent;

    public int miningSpeedLevelPermanent;
    public int miningGoldRobotLevelPermanent;
    public int miningDiamonRobotLevelPermanent;
    public int miningMovingSpeedLevelPermanent;
    public int miningMineralValueLevelPermanent;

    public int diamondAutoSpawnerWaveDiamondBaseValueLevelPermanent;
    public int diamondAutoSpawnerSpawnerCountLevelPermanent;

    public int baseLifeMaxLevelPermanent;
    public int baseLifeRestoreLevelPermanent;
    public int baseDiamondsResetLevelPermanent;
    public int baseRelicChooseCountLevelPermanent;
    public int baseDiamondKeepPercentLevelPermanent;
    public int baseIntervalBetweenWavesLevelPermanent;
    public int baseWaveSkipperLevelPermanent;
    public int baseSetSailLevelPermanent;

    public GameData()
    {
        motherShipDiamondCount = DataManager.motherShipDiamondCount;
        diamondsInventoryCount = DataManager.diamondsInventoryCount;


        miningModeIndex = DataManager.instance.miningModeIndex;
        mapIndex = DataManager.instance.mapIndex;
        weaponGroupIndex = DataManager.instance.weaponGroupIndex;

        weaponGunUnlocked = WeaponDataManager.instance.weaponGunUnlocked;
        weaponMissileUnlocked = WeaponDataManager.instance.weaponMissileUnlocked;
        weaponLaserUnlocked = WeaponDataManager.instance.weaponLaserUnlocked;
        weaponLightningUnlocked = WeaponDataManager.instance.weaponLightningUnlocked;
        weaponTeslaTowerUnlocked = WeaponDataManager.instance.weaponTeslaTowerUnlocked;

        gunSpeedLevelPermanent = WeaponDataManager.instance.gunSpeedLevel;
        gunDamageLevelPermanent = WeaponDataManager.instance.gunDamageLevel;
        gunRangeLevelPermanent = WeaponDataManager.instance.gunRangeLevel;
        gunNumLevelPermanent = WeaponDataManager.instance.gunCountLevel;
        gunCatapultLevelPermanent = WeaponDataManager.instance.gunCatapultLevel;

        missileSpeedLevelPermanent = WeaponDataManager.instance.missileSpeedLevel;
        missileDamageLevelPermanent = WeaponDataManager.instance.missileDamageLevel;
        missileRadiusLevelPermanent = WeaponDataManager.instance.missileRadiusLevel;
        missileCountLevelPermanent = WeaponDataManager.instance.missileCountLevel;

        lightningSpeedLevelPermanent = WeaponDataManager.instance.lightningSpeedLevel;
        lightningLengthLevelPermanent = WeaponDataManager.instance.lightningLengthLevel;
        lightningDamageLevelPermanent = WeaponDataManager.instance.lightningDamageLevel;

        laserCountLevelPermanent = WeaponDataManager.instance.laserCountLevel;
        laserSpeedLevelPermanent = WeaponDataManager.instance.laserSpeedLevel;
        laserDamageLevelPermanent = WeaponDataManager.instance.laserDamageLevel;
        laserLengthLevelPermanent = WeaponDataManager.instance.laserLengthLevel;

        teslaTowerSpeedLevelPermanent = WeaponDataManager.instance.teslaTowerSpeedLevel;
        teslaTowerLengthLevelPermanent = WeaponDataManager.instance.teslaTowerLengthLevel;
        teslaTowerDamageLevelPermanent = WeaponDataManager.instance.teslaTowerDamageLevel;
        teslaTowerTriggerAgainLevelPermanent = WeaponDataManager.instance.teslaTowerTriggerAgainLevel;

        miningSpeedLevelPermanent = DataManager.miningSpeedLevel;
        miningGoldRobotLevelPermanent = DataManager.miningGoldRobotLevel;
        miningDiamonRobotLevelPermanent = DataManager.miningDiamondRobotLevel;
        miningMovingSpeedLevelPermanent = DataManager.miningMovingSpeedLevel;
        miningMineralValueLevelPermanent = DataManager.miningMineralValueLevel;
        miningIgnoreMonsterPermanent = DataManager.miningIgnoreMonsterUnlocked;

        diamondAutoSpawnerWaveDiamondBaseValueLevelPermanent = DataManager.diamondAutoSpawnerWaveDiamondBaseValueLevel;
        diamondAutoSpawnerSpawnerCountLevelPermanent = DataManager.diamondAutoSpawnerSpawnerCountLevel;

        baseLifeMaxLevelPermanent = DataManager.baseLifeMaxLevel;
        baseLifeRestoreLevelPermanent = DataManager.baseLifeRestoreLevel;
        baseDiamondsResetLevelPermanent = DataManager.baseDiamondsResetLevel;
        baseRelicChooseCountLevelPermanent = DataManager.baseRelicChooseCountLevel;
        baseDiamondKeepPercentLevelPermanent = DataManager.baseDiamondKeepPercentLevel;
        baseIntervalBetweenWavesLevelPermanent = DataManager.baseIntervalBetweenWavesLevel;
        baseWaveSkipperLevelPermanent = DataManager.baseWaveSkipperLevel;
        baseSetSailLevelPermanent = DataManager.baseSetSailLevel;
    }
}
