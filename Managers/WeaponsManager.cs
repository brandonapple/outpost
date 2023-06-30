using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public static WeaponsManager instance;
    public GunController gunControllerPrefab;
    List<GunController> gunControllerList;

    public MissileLuncherController missileLuncherPrefab;
    List<MissileLuncherController> missileLuncherList;

    public LaserSpawnerController laserSpawnerPrefab;
    public LightningFenceController lightningFenceControllerPrefab;
    public TeslaTowerController teslaTowerControllerPrefab;

    [Space(20)]
    public FlameThrowerController flameThrowerControllerPrefab;
    public SniperController sniperControllerPrefab;
    public BiologicalBombLuncherController biologicalBombLuncherControllerPrefab;
    public ShurikenBLuncherController shurikenBLuncherControllerPrefab;
    public MagneticFieldLuncherController magneticFieldLuncherControllerPrefab;

    [Space(20)]
    public TruckLuncher truckLuncher;
    public FreezingAirLuncher freezingAirLuncher;
    public BallLightningLuncher ballLightningLuncher;
    public EnergyBallLuncher energyBallLuncher;
    public GatlingController gatlingController;


    [Space(30)]
    public WeaponControllerBase[] weaponControllerBases;
    List<WeaponControllerBase> weaponControllersList;
    List<WeaponControllerBase> temporaryWeaponControllerList;
    //[Space(10)]
    public GameObject weaponGroupARoot, weaponGroupBRoot, weaponGroupCRoot;

    private void Awake()
    {
        instance = this;
        gunControllerList = new List<GunController>();
        missileLuncherList = new List<MissileLuncherController>();
        weaponControllersList = new List<WeaponControllerBase>();
        temporaryWeaponControllerList = new List<WeaponControllerBase>();
    }

    private void Start()
    {
        GetWeapons();
    }

    public void GetWeapons()
    {
        foreach (WeaponControllerBase weaponController in temporaryWeaponControllerList)
        {
            Destroy(weaponController.gameObject);
        }

        weaponControllersList = new List<WeaponControllerBase>();
        temporaryWeaponControllerList = new List<WeaponControllerBase>();

        DisableGroupAWeapons();
        DisableGroupBWeapons();
        DisableGroupCWeapons();



        if (DataManager.instance.weaponGroupIndex ==0)
        {
            GetGroupAWeapons();
        }
        else if (DataManager.instance.weaponGroupIndex==1)
        {
            GetGroupBWeapons();
        }
        else if (DataManager.instance.weaponGroupIndex ==2)
        {
            GetGroupCWeapons();
        }
        else if (DataManager.instance.weaponGroupIndex==3)
        {
            GetGroupAWeapons();
            GetGroupBWeapons();
            GetGroupCWeapons();
        }

        
       

        float angleSegment = Mathf.PI * 2 / weaponControllersList.Count;
        for (int i = 0; i < weaponControllersList.Count; i++)
        {
            float xPos = Mathf.Cos(angleSegment * i + Mathf.PI * .25f);
            float zPos = Mathf.Sin(angleSegment * i + Mathf.PI * .25f);
            Vector3 pos = new Vector3(xPos, 0, zPos)*.8f;
            weaponControllersList[i].transform.localPosition = pos;
        }

        if (LightningFenceController.instance)
        {
            LightningFenceController.instance.UpdateLightningLine();
        }

        LoadData();
    }
    void GetGroupAWeapons()
    {
        weaponGroupARoot.gameObject.SetActive(true);
        if (WeaponDataManager.instance.weaponGunOn)
        {
            int gunCount = (int)DataManager.instance.getCurrentValueByString("gunCount");
            for (int i = 0; i < gunCount; i++)
            {
                WeaponControllerBase gunControllerBase = Instantiate(gunControllerPrefab);
                gunControllerBase.transform.parent = gunControllerPrefab.transform.parent;
                gunControllerBase.gameObject.SetActive(true);
                weaponControllersList.Add(gunControllerBase);
                temporaryWeaponControllerList.Add(gunControllerBase);
            }
        }
        if (WeaponDataManager.instance.weaponLightningOn)
        {
            lightningFenceControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(lightningFenceControllerPrefab);
        }
        if (WeaponDataManager.instance.weaponMissileOn)
        {
            int missileCount = (int)DataManager.instance.getCurrentValueByString("missileCount");
            for (int i = 0; i < missileCount; i++)
            {
                WeaponControllerBase missileControllerBase = Instantiate(missileLuncherPrefab);
                missileControllerBase.transform.parent = missileLuncherPrefab.transform.parent;
                missileControllerBase.gameObject.SetActive(true);
                weaponControllersList.Add(missileControllerBase);
                temporaryWeaponControllerList.Add(missileControllerBase);
            }

        }
        if (WeaponDataManager.instance.weaponLaserOn)
        {
            laserSpawnerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(laserSpawnerPrefab);
        }
        if (WeaponDataManager.instance.weaponTeslaTowerOn)
        {
            teslaTowerControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(teslaTowerControllerPrefab);
        }
    }
    void GetGroupBWeapons()
    {
        weaponGroupBRoot.gameObject.SetActive(true);
        if (WeaponDataManager.instance.weaponFlameThrowerOn)
        {
            int flameThrowerControllerCount = (int)WeaponDataManager.instance.GetCurrentValueByString("flameThrowerCount");
            for (int i = 0; i < flameThrowerControllerCount; i++)
            {
                WeaponControllerBase controllerBase = Instantiate(flameThrowerControllerPrefab);
                controllerBase.transform.parent = flameThrowerControllerPrefab.transform.parent;
                controllerBase.gameObject.SetActive(true);
                weaponControllersList.Add(controllerBase);
                temporaryWeaponControllerList.Add(controllerBase);
            }
        }
        if (WeaponDataManager.instance.weaponSniperOn)
        {
            sniperControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(sniperControllerPrefab);
        }
        if (WeaponDataManager.instance.weaponBiologicalBombOn)
        {
            biologicalBombLuncherControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(biologicalBombLuncherControllerPrefab);
        }
        if (WeaponDataManager.instance.weaponShurikenBOn)
        {
            shurikenBLuncherControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(shurikenBLuncherControllerPrefab);
        }
        if (WeaponDataManager.instance.weaponMagneticFieldOn)
        {
            magneticFieldLuncherControllerPrefab.gameObject.SetActive(true);
            weaponControllersList.Add(magneticFieldLuncherControllerPrefab);
        }
    }

    void GetGroupCWeapons()
    {
        weaponGroupCRoot.gameObject.SetActive(true);
        if (WeaponDataManager.instance.weaponTruckOn)
        {
            truckLuncher.gameObject.SetActive(true);
            truckLuncher.GetComponent<TruckLuncher>().EmptyTruckList();
            weaponControllersList.Add(truckLuncher);
        }
        if (WeaponDataManager.instance.weaponFreezingAirOn)
        {
            freezingAirLuncher.gameObject.SetActive(true);
            weaponControllersList.Add(freezingAirLuncher);
        }
        if (WeaponDataManager.instance.weaponBallLightningOn)
        {
            ballLightningLuncher.gameObject.SetActive(true);
            weaponControllersList.Add(ballLightningLuncher);
        }
        if (WeaponDataManager.instance.weaponEnergyBallOn)
        {
            energyBallLuncher.gameObject.SetActive(true);
            weaponControllersList.Add(energyBallLuncher);
        }
        if (WeaponDataManager.instance.weaponGatlingOn)
        {
            gatlingController.gameObject.SetActive(true);
            weaponControllersList.Add(gatlingController);
        }
    }
    
    void DisableGroupAWeapons()
    {
        gunControllerPrefab.gameObject.SetActive(false);
        lightningFenceControllerPrefab.gameObject.SetActive(false);
        missileLuncherPrefab.gameObject.SetActive(false);
        laserSpawnerPrefab.gameObject.SetActive(false);
        teslaTowerControllerPrefab.gameObject.SetActive(false);
    }
    void DisableGroupBWeapons()
    {
        flameThrowerControllerPrefab.gameObject.SetActive(false);
        sniperControllerPrefab.gameObject.SetActive(false);
        biologicalBombLuncherControllerPrefab.gameObject.SetActive(false);
        shurikenBLuncherControllerPrefab.gameObject.SetActive(false);
        magneticFieldLuncherControllerPrefab.gameObject.SetActive(false);
    }

    void DisableGroupCWeapons()
    {
        truckLuncher.gameObject.SetActive(false);
        freezingAirLuncher.gameObject.SetActive(false);
        ballLightningLuncher.gameObject.SetActive(false);
        energyBallLuncher.gameObject.SetActive(false);
        gatlingController.gameObject.SetActive(false);
    }


    public void UpdateWeaponDatas()
    {
        weaponControllerBases = GetComponentsInChildren<WeaponControllerBase>();
        foreach (WeaponControllerBase weaponControllerBase in weaponControllerBases)
        {
            weaponControllerBase.LoadData();
        }
        LoadData();
    }

    public void UpdateWeaponBuffs()
    {
        BuffVisualization[] buffVisualizations = GetComponentsInChildren<BuffVisualization>();
        foreach (BuffVisualization buffVisualization in buffVisualizations)
        {
            buffVisualization.InitBuff();
        }
    }
    public void UpdateGunControllerAILearningBuffs()
    {
        GunController[] gunControllers = GetComponentsInChildren<GunController>();
        foreach (GunController gunController in gunControllers)
        {
            if (gunController.thisBuffVisualization.aiLearningBuffSingle)
            {
                gunController.thisBuffVisualization.aiLearningBuffSingle.LoadData();
            }
        }

        SniperController[] sniperControllers = GetComponentsInChildren<SniperController>();
        foreach (SniperController sniper in sniperControllers)
        {
            if (sniper.thisBuffVisualization.aiLearningBuffSingle)
            {
                sniper.thisBuffVisualization.aiLearningBuffSingle.LoadData();
            }
        }

        GatlingController[] gatlingControllers = GetComponentsInChildren<GatlingController>();
        foreach (GatlingController controller in gatlingControllers)
        {
            if (controller.thisBuffVisualization.aiLearningBuffSingle)
            {
                controller.thisBuffVisualization.aiLearningBuffSingle.LoadData();
            }
        }
    }
    public void UpdateGunControllerEngineOilBuffs()
    {
        GunController[] gunControllers = GetComponentsInChildren<GunController>();
        foreach (GunController gunController in gunControllers)
        {
            if (gunController.thisBuffVisualization.engineOilBuffSingle)
            {
                gunController.thisBuffVisualization.engineOilBuffSingle.LoadData();
            }
        }

        SniperController[] sniperControllers = GetComponentsInChildren<SniperController>();
        foreach (SniperController sniperController in sniperControllers)
        {
            if (sniperController.thisBuffVisualization.engineOilBuffSingle)
            {
                sniperController.thisBuffVisualization.engineOilBuffSingle.LoadData();
            }
        }

        GatlingController[] gatlingControllers = GetComponentsInChildren<GatlingController>();
        foreach (GatlingController gatlingController in gatlingControllers)
        {
            if (gatlingController.thisBuffVisualization.showEnegineOilBuff)
            {
                gatlingController.thisBuffVisualization.engineOilBuffSingle.LoadData();
            }
        }
    }
   

    public float partnerWeaponDamageMultiplier;
    public float loneWolfWeaponDamageMultiplier;

    public void LoadData()
    {
        if (!RelicManager.instance) return;

        if (RelicManager.instance.partnerCount>0)
        {
            float f = RelicManager.instance.partnerWeaponDamageFactor * activeWeaponsCount;
            partnerWeaponDamageMultiplier = f+1;
        }
        if (RelicManager.instance.loneWolfCount>0)
        {
            if (activeWeaponsCount == 1)
            {
                float f = RelicManager.instance.loneWolfWeaponDamageFactor * 1;
                loneWolfWeaponDamageMultiplier = f+1;
            }
            else
            {
                loneWolfWeaponDamageMultiplier = 1;
            }
           
        }

    }
    public int activeWeaponsCount
    {
        get
        {
            return weaponControllersList.Count;
        }
    }
}
