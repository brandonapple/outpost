using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : BaseTargetUnit
{
    public static Base instance;
    public float lifeMax;
    public float lifeCurrent;
    public BaseLifeSlider baseLifeSlider;


    public float goldMineralValue;

    public GameObject baseSelf;
    public GameObject baseShadow;

    public GameObject domePrick;

    public GameObject weaponsRoot;
    public GameObject additionalWeaponsRoot;
    public RobotManager robotManager;
    GameObject baseDestroyedRuin;

    public float baseLifeRestoreSpeed;
    public bool invincibleShieldOn;


    bool hittedThisWave;
    public int continuousHittedWavesCount;

    public bool invincible;

    private void Awake()
    {
        instance = this;
    }
    
    public void Start()
    {
        UpdateLifeMaxValue();
        lifeCurrent = lifeMax;

        UpdateLifeSlider();
        SetLifeSliderSeparators();

        GetData();
        SwitchOff();

        if (baseDestroyedRuin!= null)
        {
            Destroy(baseDestroyedRuin.gameObject);
        }
    }
    public void GetDiamondMineral()
    {
        GetDiamondMineral(1);
    }

    public void GetDiamondMineral(float _value)
    {
        int a = Mathf.FloorToInt(goldMineralValue * _value);
        float b = goldMineralValue - a;
        if (Random.value < b) a += 1;

        MonsterManager.instance.CurrentWaveDataSettlementAddDiamondFromMine(a);
        CoinsManager.instance.AddDiamond(a);


        GameObject getDiamondValueText = GameObjectPoolManager.instance.getDiamondValueTextPool.Get(transform.position, 1);
        getDiamondValueText.GetComponent<GetMineralValueText>().SetValue(a, GetMineralValueText.MineralType.diamond);
       

       
        if (diamondClipReady)
        {
            AudioManager.PlayClip("diamond");
            diamondClipReady = false;
        }
        StopCoroutine(DiamondClipReady());
        StartCoroutine(DiamondClipReady());
        IEnumerator DiamondClipReady()
        {
            yield return new WaitForSeconds(.25f);
            diamondClipReady = true;
        }
    }
    bool diamondClipReady;
    public void GetGoldMineral()
    {
        int a = Mathf.FloorToInt(goldMineralValue);
        float b = goldMineralValue - a;
        if (Random.value < b) a += 1;

        MonsterManager.instance.CurrentWaveDataSettlementAddGoldFromMine(a);
        CoinsManager.instance.AddMoney(a);
      
    }
    public void GetGoldMineral(float _value)
    {
        int a = (int)_value;
        CoinsManager.instance.AddMoney(a);
    }

    public void Hitted(float _value,Vector3 damageFromPos)
    {
       
        if (invincible)
        {
            _value = 0;
        }

        MainCam.instance.CamShake();

        Vector3 hittedEffectPos = damageFromPos + (transform.position - damageFromPos).normalized * .5f;
        hittedEffectPos += new Vector3(Random.value - .5f, Random.Range(.25f, .75f), Random.value - .5f)*.3f;
        EffectManager.instance.SpawnEffect("baseHittedEffect", hittedEffectPos, Quaternion.identity);

        if (TemporaryShieldController.instance)
        {
            if (TemporaryShieldController.instance.shieldExist && TemporaryShieldController.instance.gameObject.activeSelf)
            {
                TemporaryShieldController.instance.Hitted(_value);
                _value = 0;
            }
        }
      


        if (GameManager.instance.thisGameState == GameManager.GameState.gameOver) return;
        if (invincibleShieldOn) return;

       
        lifeCurrent -= _value;
        UpdateLifeSlider();
        if (lifeCurrent<=0)
        {
            lifeCurrent = 0;
            Dead();
        }

       
        AudioManager.PlayClips("baseHitted");

        if (RelicManager.instance.energyShieldCount>0)
        {
            EnergyShieldController.instance.BaseHitted();
        }

        if (RelicManager.instance.compensationCount>0)
        {
            float f = Random.value;
            if (f<RelicManager.instance.compensationBaseHittedDropCoinChance)
            {
                AdditionalMoneyManager.instance.CompensationDropCoins(1);
            }
        }
        if (RelicManager.instance.insuranceCount>0)
        {
            if (!hittedThisWave) hittedThisWave = true;
        }
       

    }

    public void Heal(float _value)
    {
        lifeCurrent += _value;
        if (lifeCurrent > lifeMax)
        {
            lifeCurrent = lifeMax;
        }
        UpdateLifeSlider();
    }
   
    void UpdateLifeSlider()
    {
        if (baseLifeSlider != null)
        {
          baseLifeSlider.UpdateSlider(lifeCurrent / lifeMax);
        }
    }
    void SetLifeSliderSeparators()
    {
        if (baseLifeSlider == null) return;
        baseLifeSlider.SetSeparators(lifeMax);
    }

    void Dead()
    {
        GameManager.instance.GameOver();
        SwitchOff();

        int diamondLoseCount = Mathf.FloorToInt(CoinsManager.instance.diamondCount * .5f);
        int a = diamondLoseCount / 10;
        int b = diamondLoseCount - a * 10;

        for (int i = 0; i < a; i++)
        {
            DiamondDrop diamondDrop = Instantiate(Resources.Load<DiamondDrop>("Prefab/DiamondDrop"));
            diamondDrop.value = 10;
            diamondDrop.transform.position = Vector3.zero;
        }

        for (int i = 0; i < b; i++)
        {
            DiamondDrop diamondDrop = Instantiate(Resources.Load<DiamondDrop>("Prefab/DiamondDrop"));
            diamondDrop.value = 1;
            diamondDrop.transform.position = Vector3.zero;
        }





        baseDestroyedRuin = Instantiate(Resources.Load<GameObject>("Prefab/BaseDestroyedRuin"));
        baseDestroyedRuin.transform.position = transform.position;


        CoinsManager.instance.LoseHalfDiamonds();
        CoinsManager.instance.LoseAllCoins();
        EffectManager.instance.SpawnEffect("baseExplosionEffect", transform.position, Quaternion.identity);

        AchievementManager.instance.ReachAchievement(SteamManager.AchievementType.baseDestroyed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dead();
        }

        if (lifeCurrent<lifeMax)
        {
            lifeCurrent += Time.deltaTime * baseLifeRestoreSpeed;
            UpdateLifeSlider();
        }
        if (lifeCurrent>lifeMax)
        {
            lifeCurrent = lifeMax;
        }
    }

    public void UpdateLifeMaxValue()
    {
        lifeMax = DataManager.instance.getCurrentValueByString("baseLifeMax");

        if (TalentManager.instance.riskForDamageUnlocked)
        {
            lifeMax = 5;
        }

        if (lifeCurrent>lifeMax)
        {
            lifeCurrent = lifeMax;
        }

        if (RelicManager.instance.moreDefenceCount>0)
        {
            lifeMax *= RelicManager.instance.moreDefenceMultiplier;
        }

        SetLifeSliderSeparators();
        UpdateLifeSlider();
    }

    public void FullLifeCurrent()
    {
        lifeCurrent = lifeMax;
        UpdateLifeSlider();
    }

    public void GetData()
    {
        goldMineralValue = DataManager.instance.getCurrentValueByString("miningMineralValue");
        baseLifeRestoreSpeed = DataManager.instance.getCurrentValueByString("baseLifeRestore");

        UpdateLifeMaxValue();

        if (RelicManager.instance.bronzeScalesCount>0)
        {
            domePrick.gameObject.SetActive(true);
        }
        else
        {
            domePrick.gameObject.SetActive(false);
        }
    }
   
    public void SwitchOff()
    {
        baseSelf.gameObject.SetActive(false);
        baseShadow.gameObject.SetActive(false);
        weaponsRoot.gameObject.SetActive(false);
        additionalWeaponsRoot.gameObject.SetActive(false);
        robotManager.EmptyAllRobots();
        robotManager.gameObject.SetActive(false);
        baseLifeSlider.gameObject.SetActive(false);

         MainCanvas.instance.GameRoundEnd();
         FindObjectOfType<BGMPlayer>().LoadData();
    }
    public void SwitchOn()
    {
        baseSelf.gameObject.SetActive(true);
        baseShadow.gameObject.SetActive(true);
        weaponsRoot.gameObject.SetActive(true);
        additionalWeaponsRoot.gameObject.SetActive(true);

        robotManager.gameObject.SetActive(true);
        robotManager.GetRobots();
        baseLifeSlider.gameObject.SetActive(true);
        robotManager.SetRobotsReadyTime();
        MainCanvas.instance.GameRoundBegin();
        GameManager.instance.GameRoundBegin();

        GunRangeManager.instance.UpdateRange();
        WeaponsManager.instance.GetWeapons();
        WeaponsManager.instance.UpdateWeaponDatas();
        FindObjectOfType<BGMPlayer>().LoadData();

        FullLifeCurrent();
    }

    public void InvincibleShieldSwitchOn()
    {
        invincibleShieldOn = true;
    }
    public void InvincivleShieldSwitchOff()
    {
        invincibleShieldOn = false;
    }
    public float lifePercent
    {
        get
        {
            return lifeCurrent / lifeMax;
        }
    }


    public void WaveBegin()
    {
        hittedThisWave = false;
    }
    public void WaveEnd()
    {
        if (hittedThisWave)
        {
            continuousHittedWavesCount++;
            if (continuousHittedWavesCount>=RelicManager.instance.insuranceInterval)
            {
                continuousHittedWavesCount = 0;
                RandomRewardsManager.instance.DropRewardByValue(4);
            }
        }
        else
        {
            continuousHittedWavesCount = 0;
        }
    }
}
