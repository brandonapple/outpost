using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeRootPanel : MonoBehaviour
{
    public static UpgradeRootPanel instance;
    [Header("-------branches-------")]
    public UpgradeBranch weaponBranch;
    public UpgradeBranch temporaryWeaponBranch;

    public UpgradeBranch gunBranch;
    public UpgradeBranch lightningBranch;
    public UpgradeBranch missileBranch;
    public UpgradeBranch laserBranch;
    public UpgradeBranch teslaTowerBranch;

    public UpgradeBranch diamondAutoSpawnerBranch;
    public UpgradeBranch miningBranch;
    public UpgradeBranch baseBranch;
    [Space(10)]
    public UpgradeBranch flameThrowerBranch;
    public UpgradeBranch sniperBranch;
    public UpgradeBranch biologicalBombBranch;
    public UpgradeBranch shurikenBBranch;
    public UpgradeBranch magneticFieldBranch;
    [Space(10)]
    public UpgradeBranch truckBranch;
    public UpgradeBranch freezingAirBranch;
    public UpgradeBranch ballLightningBranch;
    public UpgradeBranch energyBallBranch;
    public UpgradeBranch gatlingBranch;


    [Header("-------")]

    [Space(30)]
    public UpgradeBranch currentUpgradeBranch;
    public UpgradeContainer upgradeContainerPrefab;
    public List<UpgradeContainer> currentUpgradeContainersList;


    public Text upgradeBranchHeaderText;
    public enum weaponType
    {
       weapon,gun,lightning,missile,laser,teslaTower,
       mining,theBase,diamondAutoSpawner,
       flameThrower,sniperController,biologicalBomb,shurikenB,magneticField,
       truck,freezingAir,ballLightning,energyBall,gatling,
    }
    public weaponType thisWeaponType = weaponType.gun;


    public bool upgradeInSpace;
    public bool showing;

    [Space(20)]
    public float distanceBetweenContainers;

    public List<Image> upgradeBookMarkList;
    public List<Image> weaponGroupABookMarkList;
    public List<Image> weaponGroupBBookMarkList;
    public List<Image> weaponGroupCBookMarkList;

    public Image weaponBookMark;
    public Image miningBookMark;
    public Image diamondAutoSpawnerBookMark;
    public Image baseBookMark;



    [Space(20)]
    public int upgradeByCoinsTime;


    [Space(20)]
    public Color upgradeCompletedColor;

    public RectTransform scrollContentRectTransform;

    private void Awake()
    {
        instance = this;
       
    }
    public void Start()
    {
        GetCurrentBookMarks();
        InitWeaponBranch();
       
        showing = false;
        Hide();
        RefreshBookMarksColor();
    }

    public void InitWeaponBranch()
    {
        LoadWeaponBranch();
        ChangeWeaponType("weapon");
    }
    public void LoadWeaponBranch()
    {
        temporaryWeaponBranch = new UpgradeBranch();
        temporaryWeaponBranch.upgradeType = "weapon";
        List<UpgradeSingle> temporaryUpgradeSingles = new List<UpgradeSingle>();


        if (DataManager.instance.weaponGroupIndex==0)
        {
            for (int i = 0; i < 5; i++)
            {
                temporaryUpgradeSingles.Add(weaponBranch.upgradeSingles[i]);
            }

        }
        else if (DataManager.instance.weaponGroupIndex ==1)
        {
            for (int i = 5; i < 10; i++)
            {
                temporaryUpgradeSingles.Add(weaponBranch.upgradeSingles[i]);
            }
        }
        else if (DataManager.instance.weaponGroupIndex ==2)
        {
            for (int i = 10; i < 15; i++)
            {
                temporaryUpgradeSingles.Add(weaponBranch.upgradeSingles[i]);
            }

        }
        else if (DataManager.instance.weaponGroupIndex==3)
        {
            for (int i = 0; i < 15; i++)
            {
                temporaryUpgradeSingles.Add(weaponBranch.upgradeSingles[i]);
            }
        }
        temporaryWeaponBranch.upgradeSingles = temporaryUpgradeSingles.ToArray();

    }
    public void GetCurrentBookMarks()
    {
        upgradeBookMarkList = new List<Image>();
        upgradeBookMarkList.Add(weaponBookMark);
        foreach (Image bookMark in weaponGroupABookMarkList)
        {
            upgradeBookMarkList.Add(bookMark);
        }
        foreach (Image bookMark in weaponGroupBBookMarkList)
        {
            upgradeBookMarkList.Add(bookMark);
        }
        foreach (Image bookMark in weaponGroupCBookMarkList)
        {
            upgradeBookMarkList.Add(bookMark);
        }




        RefreshBookMarksState();

        if (DataManager.instance.miningModeIndex==0)
        {
            upgradeBookMarkList.Add(miningBookMark);
            miningBookMark.gameObject.SetActive(true);
            diamondAutoSpawnerBookMark.gameObject.SetActive(false);
        }
        else
        {
            upgradeBookMarkList.Add(diamondAutoSpawnerBookMark);
            miningBookMark.gameObject.SetActive(false);
            diamondAutoSpawnerBookMark.gameObject.SetActive(true);
        }


        upgradeBookMarkList.Add(baseBookMark);

    }
   
    public void ChangeWeaponType(string weaponName)
    {
        switch (weaponName)
        {
            case "weapon":
                thisWeaponType = weaponType.weapon;
                currentUpgradeBranch = temporaryWeaponBranch;
                break;
            case "gun":
                thisWeaponType = weaponType.gun;
                currentUpgradeBranch = gunBranch;
                break;
            case "lightning":
                thisWeaponType = weaponType.lightning;
                currentUpgradeBranch = lightningBranch;
                break;
            case "missile":
                thisWeaponType = weaponType.missile;
                currentUpgradeBranch = missileBranch;
                break;
            case "laser":
                thisWeaponType = weaponType.laser;
                currentUpgradeBranch = laserBranch;
                break;
            case "teslaTower":
                thisWeaponType = weaponType.teslaTower;
                currentUpgradeBranch = teslaTowerBranch;
                break;
            case "mining":
                currentUpgradeBranch = miningBranch;
                thisWeaponType = weaponType.mining;
                break;
            case "base":
                currentUpgradeBranch = baseBranch;
                thisWeaponType = weaponType.theBase;
                break;
            case "diamondAutoSpawner":
                currentUpgradeBranch = diamondAutoSpawnerBranch;
                thisWeaponType = weaponType.diamondAutoSpawner;
                break;


            case "flameThrower":
                currentUpgradeBranch = flameThrowerBranch; 
                thisWeaponType = weaponType.flameThrower;
                break;
            case "sniperController":
                currentUpgradeBranch = sniperBranch;
                thisWeaponType = weaponType.sniperController;
                break;
            case "biologicalBomb":
                currentUpgradeBranch = biologicalBombBranch;
                thisWeaponType = weaponType.biologicalBomb;
                break;
            case "shurikenB":
                currentUpgradeBranch = shurikenBBranch;
                thisWeaponType = weaponType.shurikenB;
                break;
            case "magneticField":
                currentUpgradeBranch = magneticFieldBranch;
                thisWeaponType = weaponType.magneticField;
                break;

            case "truck":
                currentUpgradeBranch = truckBranch;
                thisWeaponType = weaponType.truck;
                break;
            case "freezingAir":
                currentUpgradeBranch = freezingAirBranch;
                thisWeaponType = weaponType.freezingAir;
                break;
            case "ballLightning":
                currentUpgradeBranch = ballLightningBranch;
                thisWeaponType = weaponType.ballLightning;
                break;
            case "energyBall":
                currentUpgradeBranch = energyBallBranch;
                thisWeaponType = weaponType.energyBall;
                break;
            case "gatling":
                currentUpgradeBranch = gatlingBranch;
                thisWeaponType = weaponType.gatling;
                break;
            default:
                break;
        }




        RefreshBookMarksColor();
        LoadBranch();

        AudioManager.PlayClip("bookMark");
        UpdateScrollContentRectTransfoemSize();
    }

    public void UpdateScrollContentRectTransfoemSize()
    {
        int upgradeSinglesCount = currentUpgradeBranch.upgradeSingles.Length;
        scrollContentRectTransform.sizeDelta = new Vector2(0, upgradeSinglesCount * 80);
    }


    public void RefreshBookMarksColor()
    {
        foreach (Image image in upgradeBookMarkList)
        {
            image.color = Color.gray;
        }
        switch (thisWeaponType)
        {
            case weaponType.weapon:
                upgradeBookMarkList[0].color = Color.white;
                break;


            case weaponType.gun:
                weaponGroupABookMarkList[0].color = Color.white;
                break;
            case weaponType.lightning:
                weaponGroupABookMarkList[1].color = Color.white;
                break;
            case weaponType.missile:
                weaponGroupABookMarkList[2].color = Color.white;
                break;
            case weaponType.laser:
                weaponGroupABookMarkList[3].color = Color.white;
                break;
            case weaponType.teslaTower:
                weaponGroupABookMarkList[4].color = Color.white;
                break;


            case weaponType.mining:
                upgradeBookMarkList[6].color = Color.white;
                break;
            case weaponType.diamondAutoSpawner:
                upgradeBookMarkList[6].color = Color.white;
                break;
            case weaponType.theBase:
                upgradeBookMarkList[7].color = Color.white;
                break;


            case weaponType.flameThrower:
                weaponGroupBBookMarkList[0].color = Color.white;
                break;
            case weaponType.sniperController:
                weaponGroupBBookMarkList[1].color = Color.white;
                break;
            case weaponType.biologicalBomb:
                weaponGroupBBookMarkList[2].color = Color.white;
                break;
            case weaponType.shurikenB:
                weaponGroupBBookMarkList[3].color = Color.white;
                break;
            case weaponType.magneticField:
                weaponGroupBBookMarkList[4].color = Color.white;
                break;


            case weaponType.truck:
                weaponGroupCBookMarkList[0].color = Color.white;
                break;
            case weaponType.freezingAir:
                weaponGroupCBookMarkList[1].color = Color.white;
                break;
            case weaponType.ballLightning:
                weaponGroupCBookMarkList[2].color = Color.white;
                break;
            case weaponType.energyBall:
                weaponGroupCBookMarkList[3].color = Color.white;
                break;
            case weaponType.gatling:
                weaponGroupCBookMarkList[4].color = Color.white;
                break;
            default:
                break;
        }
    }
    public void LoadBranch()
    {
        foreach (UpgradeContainer upgradeContainer in currentUpgradeContainersList)
        {
            if (upgradeContainer.gameObject!=null)
            {
                Destroy(upgradeContainer.gameObject);
            }
        }
        currentUpgradeContainersList = new List<UpgradeContainer>();

        

        StopAllCoroutines();
        StartCoroutine(SpawnContainers());
        IEnumerator SpawnContainers()
        {
            for (int i = 0; i < currentUpgradeBranch.upgradeSingles.Length; i++)
            {
                yield return new WaitForSeconds(.02f);

                UpgradeContainer upgradeContainer = Instantiate(upgradeContainerPrefab);
                upgradeContainer.gameObject.SetActive(true);
                upgradeContainer.transform.parent = upgradeContainerPrefab.transform.parent;
                currentUpgradeContainersList.Add(upgradeContainer);
                upgradeContainer.upgradeDetail = currentUpgradeBranch.upgradeSingles[i].upgradeDetail;
                upgradeContainer.levelMax = currentUpgradeBranch.upgradeSingles[i].upgradeLevelMax;
                upgradeContainer.oneTime = currentUpgradeBranch.upgradeSingles[i].oneTime;
                upgradeContainer.unlimitedTime = currentUpgradeBranch.upgradeSingles[i].unlimitedTime;
                upgradeContainer.independent = currentUpgradeBranch.upgradeSingles[i].independent;

                if (i>0)
                {
                    upgradeContainer.lastUpgradeContainer = currentUpgradeContainersList[i-1];
                }
            }
            upgradeContainerPrefab.gameObject.SetActive(false);

            yield return new WaitForSeconds(.5f);
            RefreshBookMarksPos();
        }

        upgradeBranchHeaderText.text = currentUpgradeBranch.upgradeType;
        string _string = LanguageManager.GetText(currentUpgradeBranch.upgradeType);
        upgradeBranchHeaderText.text = _string;


        UpdateScrollContentRectTransfoemSize();
    }

    public void RefreshBookMarksPos()
    {
        RefreshBookMarksState();

        if (!upgradeInSpace)
        {
            weaponBookMark.gameObject.SetActive(false);
            foreach (Image image in upgradeBookMarkList)
            {
                if (!weaponGroupABookMarkList.Contains(image) 
                    && !weaponGroupBBookMarkList.Contains(image)
                    && !weaponGroupCBookMarkList.Contains(image))
                {
                    image.gameObject.SetActive(false);
                }
            }
            baseBookMark.gameObject.SetActive(false);
        }
        else
        {
            weaponBookMark.gameObject.SetActive(true);
            baseBookMark.gameObject.SetActive(true);
        }
    }

    public void RefreshCurrentContainerList()
    {
        foreach (UpgradeContainer container in currentUpgradeContainersList)
        {
            container.LoadData();
        }
    }

    public void RefreshBookMarksState()
    {
        if (DataManager.instance.weaponGroupIndex == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                weaponGroupBBookMarkList[i].gameObject.SetActive(false);
                weaponGroupCBookMarkList[i].gameObject.SetActive(false);
            }
            CheckWeaponGroupABookMarks();
        }
        else if (DataManager.instance.weaponGroupIndex == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                weaponGroupABookMarkList[i].gameObject.SetActive(false);
                weaponGroupCBookMarkList[i].gameObject.SetActive(false);
            }
            CheckWeaponGroupBBookMarks();
        }
        else if (DataManager.instance.weaponGroupIndex == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                weaponGroupABookMarkList[i].gameObject.SetActive(false);
                weaponGroupBBookMarkList[i].gameObject.SetActive(false);
            }
            CheckWeaponGroupCBookMarks();
        }
        else if (DataManager.instance.weaponGroupIndex == 3)
        {
            CheckWeaponGroupABookMarks();
            CheckWeaponGroupBBookMarks();
            CheckWeaponGroupCBookMarks();
        }

       // CheckWeaponGroupABookMarks();
       // CheckWeaponGroupBBookMarks();
       // CheckWeaponGroupCBookMarks();

        void CheckWeaponGroupABookMarks()
        {
            weaponGroupABookMarkList[0].gameObject.SetActive(WeaponDataManager.instance.weaponGunOn);
            weaponGroupABookMarkList[1].gameObject.SetActive(WeaponDataManager.instance.weaponLightningOn);
            weaponGroupABookMarkList[2].gameObject.SetActive(WeaponDataManager.instance.weaponMissileOn);
            weaponGroupABookMarkList[3].gameObject.SetActive(WeaponDataManager.instance.weaponLaserOn);
            weaponGroupABookMarkList[4].gameObject.SetActive(WeaponDataManager.instance.weaponTeslaTowerOn);
        }
        void CheckWeaponGroupBBookMarks()
        {
            weaponGroupBBookMarkList[0].gameObject.SetActive(WeaponDataManager.instance.weaponFlameThrowerOn);
            weaponGroupBBookMarkList[1].gameObject.SetActive(WeaponDataManager.instance.weaponSniperOn);
            weaponGroupBBookMarkList[2].gameObject.SetActive(WeaponDataManager.instance.weaponBiologicalBombOn);
            weaponGroupBBookMarkList[3].gameObject.SetActive(WeaponDataManager.instance.weaponShurikenBOn);
            weaponGroupBBookMarkList[4].gameObject.SetActive(WeaponDataManager.instance.weaponMagneticFieldOn);
        }
        void CheckWeaponGroupCBookMarks()
        {
            weaponGroupCBookMarkList[0].gameObject.SetActive(WeaponDataManager.instance.weaponTruckOn);
            weaponGroupCBookMarkList[1].gameObject.SetActive(WeaponDataManager.instance.weaponFreezingAirOn);
            weaponGroupCBookMarkList[2].gameObject.SetActive(WeaponDataManager.instance.weaponBallLightningOn);
            weaponGroupCBookMarkList[3].gameObject.SetActive(WeaponDataManager.instance.weaponEnergyBallOn);
            weaponGroupCBookMarkList[4].gameObject.SetActive(WeaponDataManager.instance.weaponGatlingOn);
        }
    }
    public void Show()
    {
        transform.DOLocalMoveX(20, .25f);

        //if (upgradeInSpace)
        //{
        //    upgradeBookMarkList[6].gameObject.SetActive(true);
        //    upgradeBookMarkList[7].gameObject.SetActive(true);
        //}
        //else
        //{
        //    upgradeBookMarkList[6].gameObject.SetActive(false);
        //    upgradeBookMarkList[7].gameObject.SetActive(false);
        //}


        AudioManager.PlayClip("showUpgrade");
        LoadBranch();
    }
    public void Hide()
    {
        transform.DOLocalMoveX(-500, .25f);
        UpgradePanel.instance.HideUpgradePanel();
       
    }
   
    public void ShowOrHideButton()
    {
        showing = !showing;
        if (showing)
        {
            Show();
        }
        else
        {
            AudioManager.PlayClip("hideUpgrade");
            Hide();
        }

    }

    public void ShowUpgradeInSpace()
    {
        upgradeInSpace = true;
        WeaponDataManager.instance.LoadData();
        LoadBranch();
        GetCurrentBookMarks();
        RefreshBookMarksColor();
        RefreshBookMarksPos();
    }
    public void ShowUpgradeInPlant()
    {
        if (WeaponDataManager.instance.weaponGunOn)
        {
            thisWeaponType = weaponType.gun;
            currentUpgradeBranch = gunBranch;
        }
        else if (WeaponDataManager.instance.weaponLightningOn)
        {
            thisWeaponType = weaponType.lightning;
            currentUpgradeBranch = lightningBranch;
        }
        else if (WeaponDataManager.instance.weaponMissileOn)
        {
            thisWeaponType = weaponType.missile;
            currentUpgradeBranch = missileBranch;
        }
        else if (WeaponDataManager.instance.weaponLaserOn)
        {
            thisWeaponType = weaponType.laser;
            currentUpgradeBranch = laserBranch;
        }
        else if (WeaponDataManager.instance.weaponTeslaTowerOn)
        {
            thisWeaponType = weaponType.teslaTower;
            currentUpgradeBranch = teslaTowerBranch;
        }
        else if (WeaponDataManager.instance.weaponFlameThrowerOn)
        {
            thisWeaponType = weaponType.flameThrower;
            currentUpgradeBranch = flameThrowerBranch;
        }
        else if (WeaponDataManager.instance.weaponSniperOn)
        {
            thisWeaponType = weaponType.sniperController;
            currentUpgradeBranch = sniperBranch;
        }
        else if (WeaponDataManager.instance.weaponBiologicalBombOn)
        {
            thisWeaponType = weaponType.biologicalBomb;
            currentUpgradeBranch = biologicalBombBranch;
        }
        else if (WeaponDataManager.instance.weaponShurikenBOn)
        {
            thisWeaponType = weaponType.shurikenB;
            currentUpgradeBranch = shurikenBBranch;
        }
        else if (WeaponDataManager.instance.weaponMagneticFieldOn)
        {
            thisWeaponType = weaponType.magneticField;
            currentUpgradeBranch = magneticFieldBranch;
        }
        else if (WeaponDataManager.instance.weaponTruckOn)
        {
            thisWeaponType = weaponType.truck;
            currentUpgradeBranch = truckBranch;
        }
        else if (WeaponDataManager.instance.weaponFreezingAirOn)
        {
            thisWeaponType = weaponType.freezingAir;
            currentUpgradeBranch = freezingAirBranch;
           
        }
        else if (WeaponDataManager.instance.weaponBallLightningOn)
        {
            thisWeaponType = weaponType.ballLightning;
            currentUpgradeBranch = ballLightningBranch;
        }
        else if (WeaponDataManager.instance.weaponEnergyBallOn)
        {
            thisWeaponType = weaponType.energyBall;
            currentUpgradeBranch = energyBallBranch;
        }
        else if (WeaponDataManager.instance.weaponGatlingOn)
        {
            thisWeaponType = weaponType.gatling;
            currentUpgradeBranch = gatlingBranch;
        }

        GetCurrentBookMarks();
        RefreshBookMarksColor();
        RefreshBookMarksPos();

        upgradeInSpace = false;
        LoadBranch();

      
    }

    public void CheckAllCurrentContainersMoneyEnough()
    {
        foreach (UpgradeContainer container in currentUpgradeContainersList)
        {
            container.CheckMoneyEnough();
        }
    }


    public void TurnRandomContainerToFreeContainer()
    {
        UpgradeContainer upgradeContainer = currentUpgradeContainersList[Random.Range(0, currentUpgradeContainersList.Count)];
        upgradeContainer.BecomeFree();
    }
    public void UpgradeByCoins()
    {
        upgradeByCoinsTime++;
        if (RelicManager.instance.everySevenTimePassCount>0)
        {
            if (upgradeByCoinsTime>RelicManager.instance.everySevenPassUpgradeInterval)
            {
                TurnRandomContainerToFreeContainer();
                upgradeByCoinsTime = 0;
            }
        }
    }

    public int GetActiveWeaponsCount()
    {
        int activeWeaponsCount = 0;
        if (WeaponDataManager.instance.weaponGunOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponLightningOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponMissileOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponLaserOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponTeslaTowerOn) activeWeaponsCount++;

        if (WeaponDataManager.instance.weaponFlameThrowerOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponSniperOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponBiologicalBombOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponShurikenBOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponMagneticFieldOn) activeWeaponsCount++;

        if (WeaponDataManager.instance.weaponTruckOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponFreezingAirOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponBallLightningOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponEnergyBallOn) activeWeaponsCount++;
        if (WeaponDataManager.instance.weaponGatlingOn) activeWeaponsCount++;

        return activeWeaponsCount;

    }

    public bool AtLeastOneWeaponUnlocked
    {
        get
        {
            bool atLeastOne = false;
           
            if(WeaponDataManager.instance.weaponGunOn||
               WeaponDataManager.instance.weaponLightningOn||
               WeaponDataManager.instance.weaponMissileOn||
               WeaponDataManager.instance.weaponLaserOn||
               WeaponDataManager.instance.weaponTeslaTowerOn||

               WeaponDataManager.instance.weaponFlameThrowerOn||
               WeaponDataManager.instance.weaponSniperOn||
               WeaponDataManager.instance.weaponBiologicalBombOn||
               WeaponDataManager.instance.weaponShurikenBOn||
               WeaponDataManager.instance.weaponMagneticFieldOn||

               WeaponDataManager.instance.weaponTruckOn||
               WeaponDataManager.instance.weaponFreezingAirOn||
               WeaponDataManager.instance.weaponBallLightningOn||
               WeaponDataManager.instance.weaponEnergyBallOn||
               WeaponDataManager.instance.weaponGatlingOn
               )
            {
                atLeastOne = true;
            }





            return atLeastOne;
        }

    }
}




[System.Serializable]
public struct UpgradeBranch{
    public string upgradeType;
    public UpgradeSingle[] upgradeSingles;
}

[System.Serializable]
public struct UpgradeSingle
{
    public string upgradeDetail;
    public int upgradeLevelMax;
    public bool oneTime;
    public bool unlimitedTime;
    public bool independent;
}