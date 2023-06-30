using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

  
    public int coinCount;
    public int diamondCount;
    public int motherShipDiamondCount;
    public int diamondsInventoryCount;

    public Text goldCountText;
    public Text diamondCountText;

    public bool unlimitedDiamonds;

    [Space(20)]
    public float moneyForDamageMultiplier;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        motherShipDiamondCount = DataManager.motherShipDiamondCount;
        diamondsInventoryCount = DataManager.diamondsInventoryCount;

        if (unlimitedDiamonds)
        {
            motherShipDiamondCount =1000000;
        }

        UpdateText();

    }

    public void UpdateText()
    {
        goldCountText.text =coinCount.ToString();
        if (coinCount>99999)
        {
            float temCoinCount = coinCount * 0.0001f;
            goldCountText.text = temCoinCount.ToString("0.0")+"w";
        }

        string diamondCountString = diamondCount.ToString();
        if (diamondCount > 99999)
        {
            float temDiamondCount = diamondCount * 0.0001f;
            diamondCountString = temDiamondCount.ToString("0.0") + "w";
        }

        string motherShipDiamondCountString = motherShipDiamondCount.ToString();
        if (motherShipDiamondCount>99999)
        {
            float temMotherShipDiamondCount = motherShipDiamondCount * 0.0001f;
            motherShipDiamondCountString = temMotherShipDiamondCount.ToString("0.0") + "w";
        }

        diamondCountText.text = diamondCountString + "("+ motherShipDiamondCountString+")";
    }
    public void AddMoney(int value)
    {
        coinCount += value;
        UpdateText();
        UpgradeRootPanel.instance.CheckAllCurrentContainersMoneyEnough();

        AudioManager.PlayClip("gold");
        GameObject getDiamondValueText = GameObjectPoolManager.instance.getDiamondValueTextPool.Get(Base.instance.transform.position, 1);
        getDiamondValueText.GetComponent<GetMineralValueText>().SetValue(value, GetMineralValueText.MineralType.gold);

        if (TalentManager.instance.moneyForDamageUnlocked)
        {
          UpdateMoneyForDamageMultiplier();
        }

        GameManager.instance.UpdateValue("coinsCount", coinCount);
    }
    
    public void SpentMoney(int value)
    {
        coinCount -= value;
        UpdateText();
        UpgradeRootPanel.instance.CheckAllCurrentContainersMoneyEnough();
    }

    public void AddDiamond(int value)
    {
        diamondCount+= value;
        UpdateText();
        GameManager.instance.UpdateValue("diamondsCount", diamondCount);
    }
    public void SpentDiamond(int value)
    {
        diamondCount -= value;
        UpdateText();
    }
    public void SpentDiamondPermanent(int value)
    {
        motherShipDiamondCount -= value;
        UpdateText();
       // UpdateManager.instance.UpdateCurrentShowContainers();

        SaveDiamondData();
    }
    public void AddTestDiamondPermanent(int value)
    {
        if (motherShipDiamondCount+value>1000000000)
        {
            Debug.Log("max");
        }
        motherShipDiamondCount += value;
        diamondsInventoryCount += value;
      //  UpdateManager.instance.UpdateCurrentShowContainers();
        SaveDiamondData();
        UpdateText();
    }

    public void ResetButtonClick() 
    {
        motherShipDiamondCount = 0;
        diamondsInventoryCount = 0;
        UpdateText();
        DataManager.ResetData();
    }
    public void DiamondsReset()
    {
        motherShipDiamondCount = diamondsInventoryCount;
        UpdateText();
        DataManager.ResetData();
    }

    public void MoveDiamondsToMotherShip()
    {
        StopCoroutine(MoveDiamondsToMotherShipIE());
        StartCoroutine(MoveDiamondsToMotherShipIE());

        IEnumerator MoveDiamondsToMotherShipIE()
        {
            int temporaryDiamondCount = diamondCount;


            int d = temporaryDiamondCount / 1000;
            temporaryDiamondCount -= d * 1000;

            int a = temporaryDiamondCount / 100;
            temporaryDiamondCount -= a * 100;

            int b = temporaryDiamondCount / 10;
            temporaryDiamondCount -= b * 10;

            int c = temporaryDiamondCount;

            motherShipDiamondCount += diamondCount;
            diamondsInventoryCount += diamondCount;
            diamondCount = 0;
            UpdateText();

            if (d>10)
            {
                d = 10;
            }

            for (int i = 0; i < d; i++)
            {
                ChildShip.instance.MoveDiamondToMotherShip(1000);
                yield return new WaitForSeconds(.1f);
            }


            for (int i = 0; i < a; i++)
            {
                ChildShip.instance.MoveDiamondToMotherShip(100);
                yield return new WaitForSeconds(.1f);
            }

            for (int i = 0; i < b; i++)
            {
                ChildShip.instance.MoveDiamondToMotherShip(10);
                yield return new WaitForSeconds(.1f);
            }

            for (int i = 0; i < c; i++)
            {
                ChildShip.instance.MoveDiamondToMotherShip(1);
                yield return new WaitForSeconds(.1f);
            }

            SpaceRoot.instance.ShipReady();
            UpgradeRootPanel.instance.RefreshCurrentContainerList();
        }
        SaveDiamondData();
        UpdateText();

        if (motherShipDiamondCount>10)
        {
            AchievementManager.instance.ReachAchievement(SteamManager.AchievementType.IAmRich);
        }

    }

    public void MoveAdditionalDiamondsToMotherShip(int _additionalDiamondCount)
    {
        diamondCount += _additionalDiamondCount;
        MoveDiamondsToMotherShip();
    }

    public void LoseHalfDiamonds()
    {
        float percent = DataManager.instance.getCurrentValueByString("baseDiamondKeepPercent");
        diamondCount = Mathf.FloorToInt(diamondCount * percent);
        UpdateText();
    }
    public void LoseAllCoins()
    {
        if (RelicManager.instance.fundsTransferCount>0)
        {
            int diamondsCountAdditional =(int)(RelicManager.instance.fundsTransferCoinsToDiamondRatio * coinCount);
            int diamondsCountAdditionalMax = 10000;
            if (TalentManager.instance.fundsTransferLimitUnlocked)
            {
                diamondsCountAdditionalMax = 50000;
            }

            if (diamondsCountAdditional > diamondsCountAdditionalMax)
            {
                diamondsCountAdditional = diamondsCountAdditionalMax;
            }
            AddDiamond(diamondsCountAdditional);
        }

        coinCount = 0;
        UpdateText();
    }


    public void WaveBeginGetInterest()
    {
        if (RelicManager.instance.piggyBankCount>0)
        {
            int interestValue = (int)(RelicManager.instance.piggyBankInterestPercent * coinCount);
            if (interestValue > 10000)
            {
                interestValue = 10000;
            }
            //AddMoney(interestValue);
            AdditionalMoneyManager.instance.PiggyBankDropCoins(interestValue);
       
        }
    }
    void SaveDiamondData()
    {
        if (motherShipDiamondCount>1000000000)
        {
            motherShipDiamondCount = 1000000000;
        }
        if (diamondsInventoryCount>1000000000)
        {
            diamondsInventoryCount = 1000000000;
        }

        DataManager.motherShipDiamondCount = motherShipDiamondCount;
        DataManager.diamondsInventoryCount = diamondsInventoryCount;
        DataManager.SaveData();
    }

    public void UpdateMoneyForDamageMultiplier()
    {
        moneyForDamageMultiplier = 1;
        if (coinCount<10)
        {
            moneyForDamageMultiplier = 1;
        }
        else if (coinCount<100)
        {
            moneyForDamageMultiplier = 1.2f;
        }
        else if (coinCount<1000)
        {
            moneyForDamageMultiplier = 1.4f;
        }
        else if (coinCount<10000)
        {
            moneyForDamageMultiplier = 1.6f;
        }
        else if (coinCount<100000)
        {
            moneyForDamageMultiplier = 1.8f;
        }
        else
        {
            moneyForDamageMultiplier = 2f;
        }
    }
}
