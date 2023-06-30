using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffVisualization : MonoBehaviour
{
    public BuffSingle buffSinglePrefab;

    public bool showAiLearningBuff;
    public bool showEnegineOilBuff;
    public bool showGasolineBuff;
    public bool showGoldenBulletBuff;
    public bool showHourglassBuff;
    public bool showLoneWolfBuff;
    public bool showPartnerBuff;
    public bool showRangeAmplifierBuff;
    public bool showShowCaseBuff;
    public bool showSuperBatteryBuff;

    public BuffSingle aiLearningBuffSingle;
    public BuffSingle engineOilBuffSingle;
    public BuffSingle gasolineBuffSingle;
    public BuffSingle goldenBulletBuffSingle;
    public BuffSingle hourGlassBuffSingle;
    public BuffSingle loneWolfBuffSingle;
    public BuffSingle partnerBuffSingle;
    public BuffSingle rangeAmplifierBuffSingle;
    public BuffSingle showCaseBuffSingle;
    public BuffSingle superBatteryBuffSingle;

    public List<BuffSingle> buffList;
    public void Awake()
    {
        GetWeaponData();
        InitBuff();
    }
    public void InitBuff()
    {
      //  Debug.Log("init buff");

        foreach (BuffSingle buffSingle in buffList)
        {
            Destroy(buffSingle.gameObject);
        }
        buffList = new List<BuffSingle>();
        buffSinglePrefab.gameObject.SetActive(true);

        if (showAiLearningBuff && RelicManager.instance.aiLearningCount>0)
        {
            aiLearningBuffSingle = Instantiate(buffSinglePrefab,transform);
            aiLearningBuffSingle.thisBuffName = BuffSingle.BuffName.aiLearning;
            buffList.Add(aiLearningBuffSingle);
        }

        if (showEnegineOilBuff && RelicManager.instance.engineOilCount>0)
        {
            engineOilBuffSingle = Instantiate(buffSinglePrefab, transform);
            engineOilBuffSingle.thisBuffName = BuffSingle.BuffName.engineOil;
            buffList.Add(engineOilBuffSingle);
        }


        if (showGasolineBuff && RelicManager.instance.gasolineCount>0)
        {
            gasolineBuffSingle = Instantiate(buffSinglePrefab, transform);
            gasolineBuffSingle.thisBuffName = BuffSingle.BuffName.gasoline;
            buffList.Add(gasolineBuffSingle);
        }

        if (showGoldenBulletBuff && RelicManager.instance.goldenBulletCount>0)
        {
            goldenBulletBuffSingle = Instantiate(buffSinglePrefab, transform);
            goldenBulletBuffSingle.thisBuffName = BuffSingle.BuffName.goldenBullet;
            buffList.Add(goldenBulletBuffSingle);
        }
        if (showHourglassBuff && RelicManager.instance.hourglassCount>0)
        {
            hourGlassBuffSingle = Instantiate(buffSinglePrefab, transform);
            hourGlassBuffSingle.thisBuffName = BuffSingle.BuffName.hourglass;
            buffList.Add(hourGlassBuffSingle);
        }

        if (showLoneWolfBuff && RelicManager.instance.loneWolfCount>0)
        {
            loneWolfBuffSingle = Instantiate(buffSinglePrefab, transform);
            loneWolfBuffSingle.thisBuffName = BuffSingle.BuffName.loneWolf;
            buffList.Add(loneWolfBuffSingle);
        }
        if (showPartnerBuff && RelicManager.instance.partnerCount>0)
        {
            partnerBuffSingle = Instantiate(buffSinglePrefab, transform);
            partnerBuffSingle.thisBuffName = BuffSingle.BuffName.partner;
            buffList.Add(partnerBuffSingle);
        }
        if (showRangeAmplifierBuff && RelicManager.instance.rangeAmplifierCount>0)
        {
            rangeAmplifierBuffSingle = Instantiate(buffSinglePrefab, transform);
            rangeAmplifierBuffSingle.thisBuffName = BuffSingle.BuffName.rangeAmplifier;
            buffList.Add(rangeAmplifierBuffSingle);
        }


        if (showShowCaseBuff && RelicManager.instance.showCaseCount>0)
        {
            showCaseBuffSingle = Instantiate(buffSinglePrefab, transform);
            showCaseBuffSingle.thisBuffName = BuffSingle.BuffName.showCase;
            buffList.Add(showCaseBuffSingle);
        }
        if (showSuperBatteryBuff && RelicManager.instance.superBatteryCount>0)
        {
            superBatteryBuffSingle = Instantiate(buffSinglePrefab, transform);
            superBatteryBuffSingle.thisBuffName = BuffSingle.BuffName.superBattery;
            buffList.Add(superBatteryBuffSingle);
        }

        buffSinglePrefab.gameObject.SetActive(false);

      

        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].LoadData();
            buffList[i].transform.localPosition = new Vector3(0, i*.2f, 0);
        }
    }


    void GetWeaponData()
    {
        showHourglassBuff = true;
        showLoneWolfBuff = true;
        showPartnerBuff = true;
        showShowCaseBuff = true;

        if (GetComponentInParent<GunController>())
        {
            showAiLearningBuff = true;
            showEnegineOilBuff = true;
            showGoldenBulletBuff = true;
        }
        else if (GetComponentInParent<LightningFenceBullet>())
        {
            showSuperBatteryBuff = true;
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInParent<MissileLuncherController>())
        {
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInParent<LaserSpawnerController>())
        {

        }
        else if (GetComponentInParent<TeslaTowerController>())
        {
            showSuperBatteryBuff = true;
        }
        else if (GetComponentInParent<FlameThrowerController>())
        {
            showGasolineBuff = true;
        }
        else if (GetComponentInParent<SniperController>())
        {
            showAiLearningBuff = true;
            showEnegineOilBuff = true;
            showGoldenBulletBuff = true;
        }
        else if (GetComponentInParent<BiologicalBombLuncherController>())
        {
            showGasolineBuff = true;
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInChildren<ShurikenBLuncherController>())
        {

        }
        else if (GetComponentInParent<MagneticFieldLuncherController>())
        {
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInParent<TruckLuncher>())
        {
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInParent<FreezingAirLuncher>())
        {

        }
        else if (GetComponentInParent<BallLightningLuncher>())
        {
            showSuperBatteryBuff = true;
        }
        else if (GetComponentInParent<EnergyBallLuncher>())
        {
            showRangeAmplifierBuff = true;
        }
        else if (GetComponentInParent<GatlingController>())
        {
            showAiLearningBuff = true;
            showEnegineOilBuff = true;
            showGoldenBulletBuff = true;
        }
    }

}
