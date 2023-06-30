using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    public static TalentManager instance;

    public bool temporaryShieldUnlocked;
    public bool bodyCollectorUnlocked;
    public bool fastRepairUnlocked;
    public bool spikeControllerUnlocked;

    public bool miningIgnoreMonstersUnlocked;
    public bool doubleSupplyBoxUnlocked;
    public bool fundsTransferLimitUnlocked;

    public bool riskForDamageUnlocked;
    public bool moneyForDamageUnlocked;
    public bool critChanceUnlocked;

    private void Awake()
    {
        instance = this;
    }

    public void GotTalent(string _talentName)
    {
        switch (_talentName)
        {
            case "temporaryShield":
                temporaryShieldUnlocked = true;
                DevicesManager.instance.ActiveDevice("temporaryShield");
                break;
            case "bodyCollector":
                bodyCollectorUnlocked = true;
                DevicesManager.instance.ActiveDevice("bodyCollector");
                break;
            case "fastRepair":
                fastRepairUnlocked = true;
                DevicesManager.instance.ActiveDevice("fastRepair");
                break;
            case "mindController":
                DevicesManager.instance.ActiveDevice("mindController");
                break;

            case "spikeController":
                DevicesManager.instance.ActiveDevice("spikeController");
                break;
            case "miningIgnoreMonsters":
                miningIgnoreMonstersUnlocked = true;
                break;
            case "supplyBoxLimit":
                doubleSupplyBoxUnlocked = true;
                break;
            case "fundsTransferLimit":
                fundsTransferLimitUnlocked = true;
                break;

            case "riskForDamage":
                riskForDamageUnlocked = true;
                Base.instance.UpdateLifeMaxValue();
                BaseBuffVisualization.instance.GetBuff("riskForDamage");
                break;
            case "critChance":
                critChanceUnlocked = true;
                BaseBuffVisualization.instance.GetBuff("critChance");
                break;
            case "moneyForDamage":
                BaseBuffVisualization.instance.GetBuff("moneyForDamage");
                break;
            case "hailStone":
                AbilityManager.instance.AddAbility("hailStone");
                break;
            case "dollTarget":
                AbilityManager.instance.AddAbility("dollTarget");
                break;
            case "lightningChain":
                AbilityManager.instance.AddAbility("lightningChain");
                break;
            case "godPoke":
                AbilityManager.instance.AddAbility("godPoke");
                break;
            case "stormHammer":
                AbilityManager.instance.AddAbility("stormHammer");
                break;
            case "timeFreezon":
                AbilityManager.instance.AddAbility("timeFreezon");
                break;
            case "meteorite":
                AbilityManager.instance.AddAbility("meteorite");
                break;
            default:
                break;



        }
    }

    public void ResetTalents()
    {
        temporaryShieldUnlocked = false;
        bodyCollectorUnlocked = false;
        miningIgnoreMonstersUnlocked = false;
        doubleSupplyBoxUnlocked = false;
        fundsTransferLimitUnlocked = false;
        riskForDamageUnlocked = false;
        moneyForDamageUnlocked = false;
        critChanceUnlocked = false;
    }



}
