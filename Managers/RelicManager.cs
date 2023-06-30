using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;
    public List<RelicShower> relicShowerList;
    public RelicShower relicShowerPrefab;
    public RelicDetailPanel relicDetailPanel;

    [Space(20)]
    public bool spawnRelic;

    [Space(10)]
    public int mushroomBombCount;
    public int energyShieldCount;
    public int shurikenCount;
    public int stunedMissileLuncherCount;
    public int topLaserCount;

    [Space(10)]
    public int anchorCount;
    public int bronzeScalesCount;
    public int goldenBulletCount;
    public int hammerCount;
    public int hourglassCount;
    public int moreMonsterValueCount;
    public int oldDiamondCount;
    public int superBatteryCount;
    public int supplyBoxCount;
    public int gunpowderCount;
    public int telescopeCount;
    public int aiLearningCount;
    public int engineOilCount;
    public int bodyBombCount;
    public int rangeAmplifierCount;
    public int moreDefenceCount;
    public int superChargerCount;
    public int radarCount;
    public int steelPlateCount;
    public int gasolineCount;
    public int partnerCount;
    public int loneWolfCount;
    public int showCaseCount;

    [Space(10)]
    public int compensationCount;
    public int fundsTransferCount;
    public int discountTodayCount;
    public int anotherBottleCount;
    public int everySevenTimePassCount;
    public int piggyBankCount;
    public int giveAwayCount;
    public int insuranceCount;
    public int goldenEggCount;
    public int overTimePayCount;
    public int texRefundCount;


    private void Awake()
    {
        instance = this;
        relicDetailPanel.gameObject.SetActive(true);
        relicDetailPanel.gameObject.transform.localScale = Vector3.zero;
    }
    private void Start()
    {
        relicShowerList = new List<RelicShower>();
        relicShowerPrefab.gameObject.SetActive(false);


        LoadData();

    }
    public void ResetData()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        foreach (FieldInfo info in fields)
        {
            if (info.Name.Contains("Count"))
            {
                info.SetValue(this, 0);
            }
        }

        foreach (RelicShower relicShower in relicShowerList)
        {
            Destroy(relicShower.gameObject);
        }
        relicShowerList = new List<RelicShower>();
        if (AdditionalWeaponManager.instance)
        {
           AdditionalWeaponManager.instance.CheckWeapons();
        }


        gunBulletAddValue = 0;
        sniperBulletAddValue = 0;
        gatlingBulletAddValue = 0;

        relicWaveIndex = 0;
        waveIndex = 0;
        //relicWaveInterval = 0;
        LoadData();
    }
    public void GetRelic(string relicName)
    {
        string valueNameString = relicName + "Count";
        string currentValue = GetType().GetField(valueNameString).GetValue(this).ToString();
        int count = int.Parse(currentValue);
        count += 1;
        GetType().GetField(valueNameString).SetValue(this, count);


        AddRelicShower(relicName);
        LoadData();


     
        if (GameManager.instance.thisGameState == GameManager.GameState.playing)
        {
            AdditionalWeaponManager.instance.CheckWeapons();
            Base.instance.UpdateLifeMaxValue();
            GunRangeManager.instance.UpdateRange();
            WeaponsManager.instance.LoadData();
            WeaponsManager.instance.UpdateWeaponDatas();

            WeaponsManager.instance.UpdateWeaponBuffs();
        }

        switch (relicName)
        {
            case "oldDiamond":
                AdditionalMoneyManager.instance.OldDiamondsDropDiamonds(5);
                break;
            case "discountToday":
                DiscountManager.instance.ThisWaveHalfPrice();
                break;
            case "goldenEgg":
                GoldenEgg goldenEgg = Instantiate(Resources.Load<GoldenEgg>("Prefab/GoldenEgg"));

                float angle = Random.Range(0, Mathf.PI*2);
                goldenEgg.transform.position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) *2;

                break;
            case "texRefund":

                int coinsCount =(int)(WeaponsManager.instance.activeWeaponsCount * texRefundCoinsCount);
                AdditionalMoneyManager.instance.TexRefundDropCoins(coinsCount);


                break;
            default:
                break;
        }

       
        if (giveAwayCount>0)
        {
            RandomRewardsManager.instance.DropRewardByValue(giveAwayValue);
        }
    }
    public void AddRelicShower(string relicName)
    {
        foreach (RelicShower relicShower in relicShowerList)
        {
            if (relicShower.relicName == relicName)
            {
                relicShower.relicCount++;
                relicShower.Refresh();
                return;
            }
        }
        relicShowerPrefab.gameObject.SetActive(true);
        RelicShower newRelicShower = Instantiate(relicShowerPrefab);
        newRelicShower.transform.parent = transform;
        relicShowerList.Add(newRelicShower);
        newRelicShower.transform.position = relicShowerPrefab.transform.position + Vector3.right * 50 * relicShowerList.IndexOf(newRelicShower);
        newRelicShower.relicName = relicName;
        newRelicShower.relicCount = 1;
        newRelicShower.Refresh();
        relicShowerPrefab.gameObject.SetActive(false);

        RelicsDisplay.instance.GetIcons();
      
    }

    [Space(30)]
    public float anchorKillPercentValue;
    public float bronzeScalesDamageBackValue;
    public float energyShieldRestoreSpeedValue;
    public float stunedMissileLuncherSpeedValue;
    public int stunedMissileLuncherMissileCount;
    public float goldenBulletDamageMultiplier;
    public float hammerDamageMultiplier;
    public float hourGlassCDSpeedMultiplier;
    public float mushroomSpawnerSpeed;
    public float shurikenSpawnerSpeed;
    public int shurikenSpawnerCount;
    public float electricDamageMultiplier;
    public float topLaserControllerSpeed;
    public int topLaserHitEmemiesCount;
    public float monsterValueMulplier;
    public int supplyBoxAppearIntervalRoundCount;

    public float gunpowderCritChance;
    public float telescopeDamageMultiplier;
    public float aiLearningDamageAddValue;
    public float engineOilSpeedFactor;
    public float bodyBombChance;
    public float rangeAmplifierMutiplier;
    public float moreDefenceMultiplier;
    public float superChargerChance;
    public float radarViewRadiusValue;
    public float steelPlateDamageMultiplier;
    public float gasolineBurnTimeMultiplier;
    public float partnerWeaponDamageFactor;
    public float loneWolfWeaponDamageFactor;
    public float showCaseRelicsCountDamageFactor;

    [Space(20)]
    public float compensationBaseHittedDropCoinChance;
    public float fundsTransferCoinsToDiamondRatio;
    public float anotherBottleFreeChance;
    public float everySevenPassUpgradeInterval;
    public float piggyBankInterestPercent;
    public float giveAwayValue;
    public float insuranceInterval;
    public float goldenEggChance;
    public float overTimePayDiamondCount;
    public float texRefundCoinsCount;

   

    public void LoadData()
    {
        anchorKillPercentValue = 1 - Mathf.Pow(.8f, anchorCount);  // anchorCount * .15f;
        bronzeScalesDamageBackValue = bronzeScalesCount * .1f;
        energyShieldRestoreSpeedValue = energyShieldCount * .1f;
        stunedMissileLuncherSpeedValue = stunedMissileLuncherCount * .1f;
        stunedMissileLuncherMissileCount = stunedMissileLuncherCount + 3;
        goldenBulletDamageMultiplier = 1 + goldenBulletCount * .2f;
        hammerDamageMultiplier = 1 + .5f * hammerCount;
        hourGlassCDSpeedMultiplier = 1 + hourglassCount * .2f;
        mushroomSpawnerSpeed = mushroomBombCount * .1f;
        shurikenSpawnerSpeed = shurikenCount * .1f;
        shurikenSpawnerCount = shurikenCount + 1;
        electricDamageMultiplier = superBatteryCount * .2f + 1;
        topLaserControllerSpeed = .1f * topLaserCount;
        topLaserHitEmemiesCount = topLaserCount + 0;
        monsterValueMulplier = moreMonsterValueCount * .15f + 1;

        supplyBoxAppearIntervalRoundCount = 6 - supplyBoxCount;

        gunpowderCritChance = gunpowderCount * .1f;
        telescopeDamageMultiplier = telescopeCount * .25f +1;
        aiLearningDamageAddValue = aiLearningCount * 1f;
        engineOilSpeedFactor = engineOilCount * .05f ;
        bodyBombChance = bodyBombCount * .15f;
        rangeAmplifierMutiplier = rangeAmplifierCount * .15f +1;
        moreDefenceMultiplier = moreDefenceCount * .2f +1;
        superChargerChance = superChargerCount * .05f;
        radarViewRadiusValue = radarCount * 1;
        steelPlateDamageMultiplier = steelPlateCount * .25f + 1;
        gasolineBurnTimeMultiplier = 1 + gasolineCount * .25f;
        partnerWeaponDamageFactor = partnerCount * .1f;
        loneWolfWeaponDamageFactor = loneWolfCount * .5f ;
        showCaseRelicsCountDamageFactor = showCaseCount * .05f;

        compensationBaseHittedDropCoinChance = compensationCount * .1f;
        fundsTransferCoinsToDiamondRatio = fundsTransferCount * .15f;
        anotherBottleFreeChance = anotherBottleCount * .1f +.2f;
        everySevenPassUpgradeInterval = everySevenTimePassCount * -1 + 7;
        piggyBankInterestPercent = piggyBankCount * .075f;
        giveAwayValue = giveAwayCount *1 - .5f;
        insuranceInterval = 6- insuranceCount * 1;
        overTimePayDiamondCount = overTimePayCount * 2;
        texRefundCoinsCount = texRefundCount * 3;

        showCaseRelicsCountDamageMultiplier = showCaseRelicsCountDamageFactor * relicShowerList.Count + 1;

    }

    [Space(30)]
    public float gunBulletAddValue;
    public float sniperBulletAddValue;
    public float gatlingBulletAddValue;
    public float showCaseRelicsCountDamageMultiplier;


    public string GetSpecialValueByName(string valueName)
    {
        string value= GetType().GetField(valueName).GetValue(this).ToString();
        return value;
    }



    [Space(20)]
    public int relicWaveIndex;
    public int waveIndex;
    public int relicWaveInterval;
    public void WaveClean()
    {
        CheckIfSpawnRelic();
    }
    public void AddRelicWaveIndex()
    {
        relicWaveIndex += relicWaveInterval;
        CheckIfSpawnRelic();
    }

    public void CheckIfSpawnRelic()
    {
        if (!spawnRelic) return;


        waveIndex = MonsterManager.instance.waveIndexReally +5;
        int supplyInterval =supplyBoxAppearIntervalRoundCount;
        relicWaveInterval = supplyInterval;

        if (relicWaveIndex + relicWaveInterval < waveIndex)
        {
            DialoguePanel.instance.StartDialoguesWithString("relic", 1);
        }
    }

}
