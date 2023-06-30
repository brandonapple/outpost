using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static int motherShipDiamondCount;
    public static int diamondsInventoryCount;

    public int miningModeIndex;
    public int mapIndex;
    public int weaponGroupIndex;



    public static bool miningIgnoreMonsterUnlocked;




    public static int baseLifeMaxLevel = 0;
    public static int baseLifeRestoreLevel = 0;
    public static int baseDiamondsResetLevel = 0;
    public static int baseRelicChooseCountLevel = 0;
    public static int baseDiamondKeepPercentLevel = 0;
    public static int baseIntervalBetweenWavesLevel = 0;
    public static int baseWaveSkipperLevel = 0;
    public static int baseSetSailLevel = 0;
  

    public static int miningSpeedLevel = 0;
    public static int miningGoldRobotLevel = 0;
    public static int miningDiamondRobotLevel = 0;
    public static int miningMovingSpeedLevel = 0;
    public static int miningMineralValueLevel = 0;


    public static int diamondAutoSpawnerWaveDiamondBaseValueLevel = 0;
    public static int diamondAutoSpawnerSpawnerCountLevel = 0;

  
    public static int miningIgnoreMonsterPrice => 1000;

    public static void LoadData()
    {
        GameData gameData = SaveSystem.LoadGameData();
        if (gameData == null) return;

        motherShipDiamondCount = gameData.motherShipDiamondCount;
        diamondsInventoryCount = gameData.diamondsInventoryCount;


        instance.miningModeIndex = gameData.miningModeIndex;
        instance.mapIndex = gameData.mapIndex;
        instance.weaponGroupIndex = gameData.weaponGroupIndex;

        miningSpeedLevel = gameData.miningSpeedLevelPermanent;
        miningGoldRobotLevel = gameData.miningGoldRobotLevelPermanent;
        miningDiamondRobotLevel = gameData.miningDiamonRobotLevelPermanent;
        miningMovingSpeedLevel = gameData.miningMovingSpeedLevelPermanent;
        miningMineralValueLevel = gameData.miningMineralValueLevelPermanent;
        miningIgnoreMonsterUnlocked = gameData.miningIgnoreMonsterPermanent;

        diamondAutoSpawnerWaveDiamondBaseValueLevel = gameData.diamondAutoSpawnerWaveDiamondBaseValueLevelPermanent;
        diamondAutoSpawnerSpawnerCountLevel = gameData.diamondAutoSpawnerSpawnerCountLevelPermanent;

        baseLifeMaxLevel = gameData.baseLifeMaxLevelPermanent;
        baseLifeRestoreLevel = gameData.baseLifeRestoreLevelPermanent;
        baseDiamondsResetLevel = gameData.baseDiamondsResetLevelPermanent;
        baseRelicChooseCountLevel = gameData.baseRelicChooseCountLevelPermanent;
        baseDiamondKeepPercentLevel = gameData.baseDiamondKeepPercentLevelPermanent;
        baseIntervalBetweenWavesLevel = gameData.baseIntervalBetweenWavesLevelPermanent;
        baseWaveSkipperLevel = gameData.baseWaveSkipperLevelPermanent;
        baseSetSailLevel = gameData.baseSetSailLevelPermanent;
        

    }
    public static void SaveData()
    {
   //     Debug.Log("save game data");
        SaveSystem.SaveGameData();
    }
    public static void ResetData()
    {
        FindObjectOfType<DataManager>().ResetTest();
        motherShipDiamondCount = 0;
        diamondsInventoryCount = 0;

        SaveData();
    }
    public void ResetTest()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance|BindingFlags.Static);
        foreach (FieldInfo field in  fields)
        {
            if (field.Name.Contains("Level"))
            {
                field.SetValue(this, 0);
            }
            else if (field.Name.Contains("Unlocked"))
            {
                field.SetValue(this, false);
            }
        }
    }

  
    

    #region base

    public static float baseLifeMaxValueFunc(int level) => level * 5 + 10;
    public static int baseLifeMaxPrice => baseLifeMaxLevel * 6 + 3;

    public static float baseLifeRestoreValueFunc(int level) => level * .05f + .05f;
    public static int baseLifeRestorePrice => baseLifeRestoreLevel * 3 + 5;


    public static float baseDiamondsResetPrice => baseDiamondsResetLevel * 1 + 1;

    public static float baseRelicChooseCountValueFunc(int level) => level * 1 + 2;
    public static int baseRelicChooseCountPrice => baseRelicChooseCountLevel * 10 + 20;
    public static float baseDiamondKeepPercentValueFunc(int level) =>  .1f * level +.5f;
    public static int baseDiamondKeepPercentPrice => baseDiamondKeepPercentLevel * 10 + 10;
    public static float baseIntervalBetweenWavesValueFunc(int level) => level * 3f + 12;
    public static int baseIntervalBetweenWavesPrice => baseIntervalBetweenWavesLevel * 10 + 20;

    public static float baseWaveSkipperValueFunc(int level) => level * 4 + 0;
    public static float baseWaveSkipperPrice => baseWaveSkipperLevel * 100 +100;

    public static int baseSetSailPrice => 10000;
    #endregion

    #region mining
    public static int miningSpeedPrice => (int)(Math.Pow(1.65f, miningSpeedLevel) * 1.5f )*5; //miningSpeedLevel * 3 + 2;
    public static float miningSpeedValueFunc(int level) => level * .25f + 1;

    public static int miningGoldRobotPrice => (int)(Math.Pow(1.75f, miningGoldRobotLevel) * 2 )*5; // miningGoldRobotLevel * 10 + 7;
    public static int miningGoldRobotValueFunc(int level) => level + 0;

    public static int miningDiamondRobotPrice => (int)(Math.Pow(1.75f, miningDiamondRobotLevel) * 2) * 5; // MathCalculate.Accumlate(miningDiamondRobotLevel) *20 -5;
    public static int miningDiamondRobotValueFunc(int level) => level + 1;


    public static int miningMovingSpeedPrice => (int)(Math.Pow(1.65f, miningMovingSpeedLevel) * 3)*5;   //  miningMovingSpeedLevel * 15 + 15;
    public static float miningMovingSpeedValueFunc(int level) => level * .35f + 2;


   // public static int miningMineralValuePrice => miningMineralValueLevel * 15 + 20;
    public static int miningMineralValuePrice => (int)(Math.Pow(1.65f, miningMineralValueLevel) * 4)*5;
    public static float miningMineralValueValueFunc(int level) => level * .3f + 1;

    #endregion

    #region diamondAutoSpawner
    public static int diamondAutoSpawnerWaveDiamondBaseValuePrice => diamondAutoSpawnerWaveDiamondBaseValueLevel * 30 + 15;
    public static float diamondAutoSpawnerWaveDiamondBaseValueValueFunc(int level) => level * 1 + 0;

    public static int diamondAutoSpawnerSpawnerCountPrice => diamondAutoSpawnerSpawnerCountLevel * 300 + 200;
    public static float diamondAutoSpawnerSpawnerCountValueFunc(int level) => level * 1 + 1;

    #endregion

    public static DataManager instance;
    private void Awake()
    {
        instance = this;
        LoadData();
    }

    public int getLevelByString(string _string)
    {
        if (_string == "sniperRange") _string = "gunRange";
        if (_string == "truckRange") _string = "gunRange";
        string valueNameStringWithLevel = _string + "Level";
        string value = null;
        if (GetType().GetField(valueNameStringWithLevel) != null)
        {
            value = GetType().GetField(valueNameStringWithLevel).GetValue(this).ToString();
            int level = int.Parse(value);
            return level;
        }
        return WeaponDataManager.instance.GetLevelByString(_string);
        
    }
    public float getCurrentValueByString(string _string)
    {
        if (_string == "sniperRange") _string = "gunRange";
        if (_string == "truckRange") _string = "gunRange";

        string funcName = _string + "ValueFunc";
        int level = getLevelByString(_string);
        object[] levelParm = new object[] { level };
      
        if (GetType().GetMethod(funcName)!=null)
        {
            string value = GetType().GetMethod(funcName).Invoke(this, levelParm).ToString();
            return float.Parse(value);
        }
        return WeaponDataManager.instance.GetCurrentValueByString(_string);
        
    }
    public float getNextValueByString(string _string)
    {
        if (_string == "sniperRange") _string = "gunRange";
        if (_string == "truckRange") _string = "gunRange";

        string funcName = _string + "ValueFunc";
        int level = getLevelByString(_string) +1;
        object[] pars = new object[] { level };
        if (GetType().GetMethod(funcName)!=null)
        {
            string value = GetType().GetMethod(funcName).Invoke(this, pars).ToString();
            return float.Parse(value);
        }
        return WeaponDataManager.instance.GetNextValueByString(_string);
    }



    public int getPriceByString(string _string)
    {
        if (_string == "sniperRange") _string = "gunRange";
        if (_string == "truckRange") _string = "gunRange";

        string valueName = _string + "Price";
        if (GetType().GetProperty(valueName)!=null)
        {
            string value = GetType().GetProperty(valueName).GetValue(this).ToString();
            return (int.Parse(value));
        }
        else if (GetType().GetField(valueName)!=null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            return (int.Parse(value));
        }
        return WeaponDataManager.instance.GetCurrentPriceByString(_string);

        //return 0;
    }
    public void UpgradeLevel(string _string)
    {
        if (_string == "sniperRange") _string = "gunRange";
        if (_string == "truckRange") _string = "gunRange";

        string valueName = _string + "Level";


        if (GetType().GetField(valueName) != null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            int level = int.Parse(value);
            GetType().GetField(valueName).SetValue(this,level+1);
        }

        string valueNameWithUnlocked = _string + "Unlocked";
        if (GetType().GetField(valueNameWithUnlocked)!=null)
        {
            GetType().GetField(valueNameWithUnlocked).SetValue(this, true);
        }
        WeaponDataManager.instance.UpgradeLevelByString(_string);

       
    }

    public bool getWeaponUnlockedStateByString(string _string)
    {
        string valueName = _string + "Unlocked";
        if (GetType().GetField(valueName)!=null)
        {
            string value = GetType().GetField(valueName).GetValue(this).ToString();
            bool unlocked = bool.Parse(value);
            return unlocked;
        }
        return WeaponDataManager.instance.GetUnlockedByString(_string);
    }



    public void GameModeChoose()
    {
        SaveData();
    }
}
