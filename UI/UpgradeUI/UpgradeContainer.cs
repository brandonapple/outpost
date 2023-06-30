using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UpgradeContainer : MonoBehaviour
{
    public string upgradeDetail;

    public Text upgradeText;

    public int levelMax;
    public int currentLevel;
    public int currentTemporaryLevel;

    public int price;
    public Text priceText;


    public Image levelUpgradePointPrefab;
    public List<Image> levelPoints;

    public bool oneTime;
    public bool unlimitedTime;

    public bool oneTimeUnlocked;
    public bool independent;
    public bool unlocked;

    public UpgradeContainer lastUpgradeContainer;

    public Image currentContainerImage;

    public Image diamondPriceIcon;
    public Image goldPriceIcon;
    public Image questionMark;

    public bool isFree;
    public GameObject halfPriceLabel;


    public UpgradeContainerWeaponSwitch upgradeContainerWeaponSwitch;
    private void Awake()
    {
        upgradeText = GetComponentInChildren<Text>();
        currentContainerImage = GetComponent<Image>();

        currentContainerImage.color = Color.black;
    }

    private void Start()
    {
        //LoadData();

        //Invoke(nameof(LoadData), .2f);

        transform.localScale = Vector3.one;
      //  transform.DOScale(1, .1f).SetUpdate(true);

        if (upgradeDetail.Length > 0)
        {
            Invoke(nameof(LoadData), .1f);
            //LoadData();
        }

    }

    public string upgradeDetailCompletedString
    {
        get
        {
            string a = UpgradeRootPanel.instance.currentUpgradeBranch.upgradeType.ToString();
            string b = char.ToUpper(upgradeDetail[0]) + upgradeDetail.Substring(1);
            return a + b;
        }
    }

    void GetLevel()
    {
        if (UpgradeRootPanel.instance.upgradeInSpace)
        {
            currentLevel = DataManager.instance.getLevelByString(upgradeDetailCompletedString);
            currentTemporaryLevel = DataManager.instance.getLevelByString(upgradeDetailCompletedString);
        }
        else
        {
            currentLevel = UpgradeRootPanelPermanentLevel.instance.GetLevelByString(upgradeDetailCompletedString);
            currentTemporaryLevel = DataManager.instance.getLevelByString(upgradeDetailCompletedString);
        }
    }
    public void GetLevelUpgradePoints()
    {
        levelUpgradePointPrefab.gameObject.SetActive(true);
       // levelUpgradePointPrefab.transform.localScale = Vector3.one;

        Vector2 levelSize = new Vector2(180 / levelMax 
            , 7 );
        float distanceOfLevels = 180 / levelMax +4;


        levelSize *= MainCanvas.instance.canvasScaleMultiplier;
        distanceOfLevels *= MainCanvas.instance.canvasScaleMultiplier;

        levelUpgradePointPrefab.GetComponent<RectTransform>().sizeDelta = levelSize;



        for (int i = 0; i < levelPoints.Count; i++)
        {
            if (levelPoints[i].gameObject)
            {
                Destroy(levelPoints[i].gameObject);
            }
        }
        levelPoints = new List<Image>();




        for (int i = 0; i < levelMax; i++)
        {
            Image levelPoint = Instantiate(levelUpgradePointPrefab);
            levelPoint.transform.localScale = Vector3.one ;
            levelPoint.transform.parent = levelUpgradePointPrefab.transform.parent;
            levelPoint.transform.position = levelUpgradePointPrefab.transform.position;
            levelPoint.transform.position += Vector3.right * distanceOfLevels * i + distanceOfLevels * .5f *Vector3.right;
            levelPoints.Add(levelPoint);
        }
        levelUpgradePointPrefab.gameObject.SetActive(false);

        DisplayLevels();
    }
    public void DisplayLevels()
    {
        if (oneTimeUnlocked)
        {
            currentLevel = levelMax;
        }
        if (unlimitedTime)
        {
            currentLevel = 0;
        }

        for (int i = 0; i < levelMax; i++)
        {
            if (i<currentLevel)
            {
                levelPoints[i].color = Color.green;
            }
            else if (i>=currentLevel && i< currentTemporaryLevel)
            {
                levelPoints[i].color = Color.yellow;
            }
            else
            {
                levelPoints[i].color = Color.black;
            }
          //  levelPoints[i].transform.localScale = Vector3.one;
        }
    }
    public void GetPriceText()
    {
        if (currentLevel >= levelMax || currentTemporaryLevel >= levelMax)
        {
            diamondPriceIcon.gameObject.SetActive(false);
            goldPriceIcon.gameObject.SetActive(false);
            priceText.gameObject.SetActive(false);
            
            return;
        }


        price = DataManager.instance.getPriceByString(upgradeDetailCompletedString);
        if (DiscountManager.instance.thisWaveHalfPrice)
        {
            price = (int)(price * .5f);
        }

        priceText.text = price.ToString();
        priceText.gameObject.SetActive(true);

        if (UpgradeRootPanel.instance.upgradeInSpace)
        {
            diamondPriceIcon.gameObject.SetActive(true);
            goldPriceIcon.gameObject.SetActive(false);
        }
        else
        {
            diamondPriceIcon.gameObject.SetActive(false);
            goldPriceIcon.gameObject.SetActive(true);
        }
       

        if (oneTimeUnlocked || !unlocked)
        {
            diamondPriceIcon.gameObject.SetActive(false);
            goldPriceIcon.gameObject.SetActive(false);
            priceText.gameObject.SetActive(false);
        }



       
    }
    public void GetUpgradeDetail()
    {
      //  upgradeText.text = upgradeDetail;
        string currentValue = DataManager.instance.getCurrentValueByString(upgradeDetailCompletedString).ToString();
        string nextValue = DataManager.instance.getNextValueByString(upgradeDetailCompletedString).ToString();

        upgradeText.text = LanguageManager.GetText(upgradeDetail) +" "+ //"\n" +
            currentValue + "--" + nextValue;



        if (currentLevel>=levelMax || currentTemporaryLevel>= levelMax)
        {
            upgradeText.text = LanguageManager.GetText(upgradeDetail) +// "\n" +
               LanguageManager.GetText("max") + ":"+currentValue;

            upgradeText.transform.localPosition = new Vector3(0, upgradeText.transform.localPosition.y, 0);
        }


        if (oneTime)
        {
            upgradeText.text = LanguageManager.GetText(upgradeDetail) + " "+ //"\n" + 
               LanguageManager.GetText(oneTimeUnlocked ? "unlocked" : "locked");
        }
        if (unlimitedTime)
        {
            upgradeText.text = LanguageManager.GetText(upgradeDetail);
        }
        if (!unlocked)
        {
            upgradeText.text = "";
        }
    }

    void GetState()
    {
        if (independent || lastUpgradeContainer==null)
        {
            currentContainerImage.color = Color.white;
            unlocked = true;
            questionMark.gameObject.SetActive(false);
        }
        else
        {
            if (lastUpgradeContainer.currentLevel + lastUpgradeContainer.currentTemporaryLevel>0)
            {
                currentContainerImage.color = Color.white;
                unlocked = true;
                questionMark.gameObject.SetActive(false);
            }
            else
            {
                currentContainerImage.color = Color.black;
                unlocked = false;
                questionMark.gameObject.SetActive(true);
            }
        }

        if (oneTime)
        {
            oneTimeUnlocked = DataManager.instance.getWeaponUnlockedStateByString(upgradeDetailCompletedString);
        }

        
    }
    public void OnClick()
    {
        transform.DOShakeRotation(.1f, 30, 20, 10, true);
        StartCoroutine(BackToNormalRotation());
        IEnumerator BackToNormalRotation()
        {
            yield return new WaitForSeconds(.1f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (upgradeDetail == "diamondsReset")
        {
            CoinsManager.instance.DiamondsReset();
            UpgradeRootPanel.instance.Start();

            UpgradeRootPanel.instance.LoadBranch();
            WeaponDataManager.instance.ResetData();

            return;
        }
        if (!unlocked)
        {
            return;
        }
        if (oneTimeUnlocked)
        {
            return;
        }
        if (currentLevel >= levelMax || currentTemporaryLevel>=levelMax)
        {
            return;
        }


        if (UpgradeRootPanel.instance.upgradeInSpace)
        {
            if (price <= CoinsManager.instance.motherShipDiamondCount)
            {
                DataManager.instance.UpgradeLevel(upgradeDetailCompletedString);
                WeaponsManager.instance.UpdateWeaponDatas();
                CoinsManager.instance.SpentDiamondPermanent(price);
                LoadData();
                if (oneTime)
                {
                    UpgradeRootPanel.instance.RefreshBookMarksPos();
                }
                UpgradeRootPanel.instance.RefreshCurrentContainerList();
                AudioManager.PlayClip("upgradeCompleted");
                WeaponDataManager.instance.SaveData();

                Base.instance.GetData();


                if (upgradeDetail=="setSail")
                {
                    GameManager.instance.MotherShipSetSail();
                }

                AchievementManager.instance.ReachAchievement(SteamManager.AchievementType.weaponUpgrade);
            }
            else
            {
                AudioManager.PlayClip("upgradeFail");
            }


           
        }
        else
        {
            if (price<=CoinsManager.instance.coinCount)
            {
                DataManager.instance.UpgradeLevel(upgradeDetailCompletedString);
                CoinsManager.instance.SpentMoney(price);
                LoadData();


                if (oneTime)
                {
                    UpgradeRootPanel.instance.RefreshBookMarksPos();
                }

                UpgradeRootPanel.instance.RefreshCurrentContainerList();
                AudioManager.PlayClip("upgradeCompleted");

                WeaponsManager.instance.GetWeapons();
                WeaponsManager.instance.UpdateWeaponDatas();

               

                GunRangeManager.instance.UpdateRange();
                if (LightningFenceController.instance)
                {
                  LightningFenceController.instance.UpdateLightningLine();
                }
                MainCam.instance.ShowBattleField();

                UpgradeRootPanel.instance.UpgradeByCoins();
            }
            else
            {
                AudioManager.PlayClip("upgradeFail");
            }
        }


        WeaponsManager.instance.LoadData();

        if (isFree)
        {
            isFree = false;
        }
    }


    public void LoadData()
    {
        GetState();
        GetLevel();
        GetUpgradeDetail();
        GetPriceText();
        GetLevelUpgradePoints();
        CheckMoneyEnough();
        CheckHalfPriceLabel();
        CheckWeaponSwitch();
    }
    void CheckHalfPriceLabel()
    {
        if (DiscountManager.instance.thisWaveHalfPrice)
        {
            halfPriceLabel.gameObject.SetActive(true);
        }
        else
        {
            halfPriceLabel.gameObject.SetActive(false);
        }
    }


    public void CheckMoneyEnough()
    {
        if (oneTime && oneTimeUnlocked)
        {
            return;
        }
        if (!unlocked)
        {
            return;
        }

        

        if (currentLevel >= levelMax || currentTemporaryLevel >= levelMax)
        {
            currentContainerImage.color = UpgradeRootPanel.instance.upgradeCompletedColor;
            return;
        }
        if (UpgradeRootPanel.instance.upgradeInSpace)
        {
            if (price<= CoinsManager.instance.motherShipDiamondCount)
            {
                currentContainerImage.color = Color.white;
            }
            else
            {
                currentContainerImage.color = Color.gray;
            }
        }
        else
        {
            if (price<=CoinsManager.instance.coinCount)
            {
                currentContainerImage.color = Color.white;
            }
            else
            {
                currentContainerImage.color = Color.gray;
            }
        }
    }

    public void CheckWeaponSwitch()
    {
        if (UpgradeRootPanel.instance.currentUpgradeBranch.upgradeType.ToString() == "weapon" &&
            oneTimeUnlocked == true)
        {
            upgradeContainerWeaponSwitch.gameObject.SetActive(true);
            string b = char.ToUpper(upgradeDetail[0]) + upgradeDetail.Substring(1);
            upgradeContainerWeaponSwitch.weaponOn = WeaponDataManager.instance.GetWeaponOnOffStateByString(b);
            upgradeContainerWeaponSwitch.ShowState();
        }
        else
        {
            upgradeContainerWeaponSwitch.gameObject.SetActive(false);
        }
    }

    public void SwitchWeapon(bool value)
    {
        string b = char.ToUpper(upgradeDetail[0]) + upgradeDetail.Substring(1);
        WeaponDataManager.instance.SwitchWeaponByName("weapon" + b, value);

        UpgradeRootPanel.instance.RefreshBookMarksState();
        WeaponDataManager.instance.SaveData();
    }
    public void BecomeFree()
    {
        isFree = true;
        LoadData();
        price = 0;
        priceText.text = price.ToString();
        CheckMoneyEnough();
    }
}
