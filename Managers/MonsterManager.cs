using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;
    float timer;
    public int monsterLevelsCount;
    public float spawnTimer;
    public float spawnTimeMax = 9;
    public bool allMonsterDead;
    float sliderReduceSpeed;

    public int waveIndex;
    public int roundIndex =1;

    [Space(20)]

    [Range(0,16)]
    public int waveIndexStart;
    [Range(1,5)]
    public int roundStart;

    [Space(10)]
    public float monsterValueTotal;
    public enum GameState
    {
        MonsterTime,
        MiningTime,
    }
    public GameState thisGameState = GameState.MiningTime;



    public List<WaveDataSettlement> waveDataSettlementList;

    public Monster[] monsters;
    [Space(10)]
    public MonsterGroupsDataSO[] mapAMonsterGroupDatas;
    public MonsterGroupsDataSO[] mapBMonsterGroupDatas;

    public MonsterGroupsDataSO testMonsterGroupData;

    public MonsterGroupsDataSO currentMonsterGroupsData;
    public MonsterGroupsDataSO advancedMonsterGroupsData;


    [Space(20)]
    public MonsterLevel[] randomEventMonsterLevels;
    public bool spawnRandomEventMonsterLevel;
    public float randomEventValue;

    private void Awake()
    {
        instance = this;
        allMonsterDead = true;
        spawnTimer = spawnTimeMax;
    }
    private void Start()
    {
        waveDataSettlementList = new List<WaveDataSettlement>();
        AddWaveDataSettlement();
        GetMonsterGroupsData();
        GetSliderReduceSpeed();

        //    InvokeRepeating(nameof(GetSliderReduceSpeed),1, 1);
       randomEventValue = Random.Range(.2f, .5f);

    }


    private void FixedUpdate()
    {
        if (GameManager.instance.thisGameState == GameManager.GameState.inSpace) return;
        monsters = FindObjectsOfType<Monster>();

        if (monsters.Length == 0)
        {
            if (!allMonsterDead)
            {
                allMonsterDead = true;
                WaveClean();
            }

            spawnTimer -= Time.deltaTime * sliderReduceSpeed;
            if (spawnTimer <= 0)
            {
                WaveBegin();
            }
            TimeManager.instance.UpdateSlider((float)spawnTimer / spawnTimeMax);
            thisGameState = GameState.MiningTime;
        }
        else
        {
            allMonsterDead = false;
            TimeManager.instance.UpdateSlider(1);
            thisGameState = GameState.MonsterTime;
        }
       

        

    }

    public void GetSliderReduceSpeed()
    {
        sliderReduceSpeed = 1;
        if (DataManager.instance.miningModeIndex == 1)
        {
            sliderReduceSpeed = 4.5f;
        }
    }

    public void GetMonsterGroupsData()
    {
        if (testMonsterGroupData!=null)
        {
            currentMonsterGroupsData = testMonsterGroupData;
            return;
        }
        switch (DataManager.instance.mapIndex)
        {
            case 0:
                currentMonsterGroupsData = mapAMonsterGroupDatas[Random.Range(0, mapAMonsterGroupDatas.Length)];
                break;
            case 1:
                currentMonsterGroupsData = mapBMonsterGroupDatas[Random.Range(0, mapBMonsterGroupDatas.Length)];
                break;
         
        }
    }
    void KeepMonsterDistance()
    {
        foreach (Monster monsterA in monsters)
        {
            foreach (Monster monsterB in monsters)
            {
                if (Vector3.Distance(monsterA.transform.position,monsterB.transform.position)<.25f)
                {
                    Vector3 dir = (monsterA.transform.position - monsterB.transform.position).normalized;
                    monsterA.transform.position += dir * .02f;
                    monsterB.transform.position += dir * -.02f;
                }
            }
        }
    }
   
    void SpawnMonsterLevel(int monsterLevelIndex)
    {
        if (monsterLevelIndex>monsterLevelsCount)
        {
            waveIndex = 0;
            monsterLevelIndex = 0;
            roundIndex++;
        }


        MonsterLevel monsterLevel = GetLevelMonsterData();

       
        if (spawnRandomEventMonsterLevel)
        {
            randomEventValue += Random.Range(.02f, .1f);
            if (randomEventValue>1)
            {
                monsterLevel = randomEventMonsterLevels[Random.Range(0, randomEventMonsterLevels.Length)];
                randomEventValue = 0;
            }
        }



        currentWaveDataSettement.monsterLevel = monsterLevel;
        for (int i = 0; i < monsterLevel.monsterGroups.Length; i++)
        {
            MonsterGroup monsterGroup = monsterLevel.monsterGroups[i];
            for (int j = 0; j < roundIndex; j++)
            {
                if (monsterGroup.thisSpawnWay=="")
                {
                    StartCoroutine(SpawnMonsterGroup(monsterGroup));
                }
                else if (monsterGroup.thisSpawnWay =="ox")
                {
                    StartCoroutine(SpawnMonsterGroupOX(monsterGroup));
                }
                else if (monsterGroup.thisSpawnWay == "rain")
                {
                    StartCoroutine(SpawnRain(monsterGroup));
                }
            }
        }



        IEnumerator SpawnMonsterGroup(MonsterGroup monsterGroup)
        {
            float radius = WeaponDataManager.instance.gunRangeValueCurrent + 2.5f;

            int _monsterCount = monsterGroup.monsterCount;
            switch (MapManager.thisMapName)
            {
                case MapManager.MapName.planetA:
                    float angleC = Mathf.PI * 2 * Random.value;
                    float addValue = ((float)1 / _monsterCount) * Mathf.PI * 2;

                    for (int i = 0; i < _monsterCount; i++)
                    {
                        angleC += addValue * Random.Range(.35f,1.65f);
                        Monster monster = Instantiate(monsterGroup.monsterPrefab);
                        monster.transform.position = new Vector3(Mathf.Cos(angleC), 0, Mathf.Sin(angleC)) * radius * Random.Range(1f,1.25f);
                        yield return new WaitForSeconds(.1f);
                    }

                   
                    break;
                case MapManager.MapName.planetB:

                    angleC = Mathf.PI * 2 * Random.value;

                    Vector3 slimeMoveDir = -new Vector3(Mathf.Cos(angleC), 0, Mathf.Sin(angleC));
                    Vector3 originalPos = -slimeMoveDir * radius;
                    Vector3 dirA = -slimeMoveDir.normalized;
                    Vector3 dirB = Vector3.Cross(dirA, Vector3.up);

                    for (int i = 0; i < _monsterCount; i++)
                    {
                        Monster monster = Instantiate(monsterGroup.monsterPrefab);
                        monster.gameObject.name = monster.gameObject.name + "--" + i.ToString();

                        monster.transform.position = originalPos;
                        int horizontalRowX = i / 7;
                        int verticalRowX = i % 7 - 3;

                        float distanceBetweenMonsters = .8f;
                        if (roundIndex == 5 || roundIndex == 10) distanceBetweenMonsters = 1.6f;
                        monster.transform.position += (horizontalRowX * dirA + verticalRowX * dirB) * distanceBetweenMonsters;

                        monster.transform.position += new Vector3(Random.value - .5f, 0, Random.value - .5f) * .5f;

                        if (monster.TryGetComponent<Slime>(out Slime slime))
                        {
                            slime.moveDir = slimeMoveDir;
                        }

                        yield return new WaitForSeconds(.02f);
                    }

                    break;
                  
                default:
                    break;
            }


           

        }

        IEnumerator SpawnMonsterGroupOX(MonsterGroup oxMonsterGroup)
        {
            float radius = WeaponDataManager.instance.gunRangeValueCurrent + 2.5f;
            float angle = Random.value * Mathf.PI * 2;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 dir = pos.normalized;
            Vector3 dirNormal = Vector3.Cross(dir, Vector3.up);
            dir = (dir + dirNormal * .55f).normalized;
            dirNormal = Vector3.Cross(dir, Vector3.up);

            for (int i = 0; i < oxMonsterGroup.monsterCount; i++)
            {
                MonsterOX ox = Instantiate(oxMonsterGroup.monsterPrefab).GetComponent<MonsterOX>();
                ox.transform.position = pos;
                ox.monsterRunDir = -dir;
                int a = i / 3;
                int b = i % 3;
                ox.transform.position += (dir * a + dirNormal * b)*.6f *Random.Range(.8f,1.2f);

                yield return new WaitForSeconds(.05f);
            }
        }

        IEnumerator SpawnRain(MonsterGroup rainGroup)
        {
            Instantiate(rainGroup.monsterPrefab).transform.position = Vector3.zero ;
            yield return null;
        }
    }

   
    public void WaveClean()
    {
        RobotManager.instance.SetRobotsReadyTime();
        GunRangeManager.instance.HideRange();
       
        RelicManager.instance.WaveClean();
        TalentPanel.instance.WaveCleanCheckTalentPoints();
        LunchButton.instance.ShowButton();

        if (waveIndex ==1)
        {
            TipPanel.instance.ShowUpdateWithGoldTip();
        }
        else if (waveIndex ==2)
        {
            TipPanel.instance.ShowLunchShipTip();
        }

        AddWaveDataSettlement();
        if (DataManager.instance.miningModeIndex==1)
        {
            if (GameManager.instance.thisGameState == GameManager.GameState.playing)
            {
                MinesManager.instance.DiamondSpawnersSpawnDiamond(waveIndex + (roundIndex - 1) * 16);
            }
        }


        WeaponsManager.instance.UpdateWeaponDatas();
        Base.instance.WaveEnd();
        DiscountManager.instance.WaveEnd();


        GoldenEgg[] goldenEggs = FindObjectsOfType<GoldenEgg>();
        if (goldenEggs.Length>0)
        {
            foreach (GoldenEgg goldenEgg in goldenEggs)
            {
                goldenEgg.WaveEnd();
            }
        }

       if(TemporaryShieldController.instance)TemporaryShieldController.instance.WaveClean();

      //  Debug.Log(waveIndexReally % 32);
        if (waveIndexReally%32==1)
        {
           // Debug.Log("get monster group data");
            GetMonsterGroupsData();
        }


        GameManager.instance.UpdateValue("waveIndex", waveIndexReally);
    }
 
    public void WaveBegin()
    {
        GetIntervalBetweenWaves();
        spawnTimer = spawnTimeMax;

        SpawnMonsterLevel(waveIndex);
        TimeManager.instance.UpdateWaveIndex(waveIndex +( roundIndex-1) * 16);

        waveIndex++;

        GunRangeManager.instance.ShowRange();
        RobotManager.instance.CallBackRobots();
        LunchButton.instance.HideButton();
        Base.instance.WaveBegin();
        CoinsManager.instance.WaveBeginGetInterest();
        WeaponDamageSettlementManager.instance.ResetDamageMaxValue();

        if (waveIndex ==3)
        {
            TipPanel.instance.ShowChangeGameSpeedTip();
        }
    }

    public MonsterLevel GetLevelMonsterData()
    {
        string levelContent;
        levelContent = currentMonsterGroupsData.monsterGroups[waveIndex];
        string monsterType = currentMonsterGroupsData.monsterType;

        if (roundIndex==3 || roundIndex == 6)
        {
            levelContent = advancedMonsterGroupsData.monsterGroups[waveIndex];
            monsterType = advancedMonsterGroupsData.monsterType;
        }


        string[] groupDatas = levelContent.Split(char.Parse(","));


        MonsterLevel monsterLevel = new MonsterLevel();
        List<MonsterGroup> monsterGroups = new List<MonsterGroup>();


        foreach (string groupData in groupDatas)
        {
            string[] strings = groupData.Split(char.Parse("_"));
            string monsterName = strings[0];
            monsterName =char.ToUpper(monsterName[0]) + monsterName.Substring(1);


            //monsterName = currentMonsterGroupsData.monsterType+ monsterName;
            monsterName = monsterType + monsterName;

            string monsterCount = strings[1];
            int monsterCountInt = int.Parse(monsterCount);

            string monsterSpawnWay = "";
            if (strings.Length>2)
            {
               monsterSpawnWay= strings[2];
            }

           
            MonsterGroup monsterGroup = new MonsterGroup();
            if (monsterName == "bugBoss" || monsterName == "slimeBoss")
            {
                if (DataManager.instance.mapIndex==0)
                {
                    if (roundIndex==1)
                    {
                        monsterGroup.monsterPrefab = Resources.Load<Monster>("Monsters/" + "bossA");
                    }
                    else 
                    {
                        monsterGroup.monsterPrefab = Resources.Load<Monster>("Monsters/" + "bossB");
                    }
                }
                else if (DataManager.instance.mapIndex ==1)
                {
                    if (roundIndex==1)
                    {
                        monsterGroup.monsterPrefab = Resources.Load<Monster>("Monsters/" + "bossC");
                    }
                    else 
                    {
                        monsterGroup.monsterPrefab = Resources.Load<Monster>("Monsters/" + "bossD");
                    }
                }
            }
            else
            {
                monsterGroup.monsterPrefab = Resources.Load<Monster>("Monsters/" + monsterName);
            }
            monsterGroup.monsterCount = monsterCountInt;
            monsterGroup.thisSpawnWay = monsterSpawnWay;
            monsterGroups.Add(monsterGroup);


        }
        monsterLevel.monsterGroups = monsterGroups.ToArray();
        return monsterLevel;
    }
    public void ResetData()
    {
        spawnTimer = spawnTimeMax;
        waveIndex = waveIndexStart;
        roundIndex = roundStart;

        int waveSkipperValue =(int)DataManager.instance.getCurrentValueByString("baseWaveSkipper");

        int roundAdditional = waveSkipperValue / 16;
        int waveAdditional = waveSkipperValue - roundAdditional * 16;
      
        roundIndex += roundAdditional;
        waveIndex += waveAdditional;

        TimeManager.instance.UpdateWaveIndex(waveIndex + (roundIndex -1)*16);
        LevelManager.instance.Start();

        waveDataSettlementList = new List<WaveDataSettlement>();
        AddWaveDataSettlement();
    }
    public void MonsterDead(Vector3 pos,float monsterValue)
    {
        monsterValueTotal += monsterValue;
        int a = Mathf.FloorToInt(monsterValueTotal);
        float b = monsterValueTotal - a;

        CurrentWaveDataSettlementAddGoldFromMonster(a);
        for (int i = 0; i < a; i++)
        {
            GameObject coin = GameObjectPoolManager.instance.coinPool.Get(pos,5);
        }
        monsterValueTotal = b;
    }

    public static Monster randomMonster
    {
        get
        {
            List<Monster> _monstersInView = monstersInView;
            if (_monstersInView == null) return null;
            if (_monstersInView.Count == 0) return null;

            Monster targetMonster = _monstersInView[Random.Range(0, _monstersInView.Count)];
            return targetMonster;
        }
    }
    public static Monster cloestMonster(Vector3 pos)
    {
        List<Monster> _monstersInView = monstersInView;
        if (_monstersInView == null) return null;
        if (_monstersInView.Count == 0) return null;

        Monster cloestMonster = _monstersInView[0];
        foreach (Monster monster in _monstersInView)
        {
            if (Vector3.Distance(monster.transform.position, pos) <
                Vector3.Distance(cloestMonster.transform.position, pos))
            {
                cloestMonster = monster;
            }
        }
        return cloestMonster;


    }
    public static Monster cloestNextMonster(Vector3 pos, Monster ignoreMonster)
    {
        List<Monster> nextTargetMonsters = monstersInView;
        if (nextTargetMonsters.Count == 0) return null;

        if (nextTargetMonsters.Contains(ignoreMonster))
        {
            nextTargetMonsters.Remove(ignoreMonster);
        }


        if (nextTargetMonsters.Count == 0) return null;
        Monster cloestMonster = nextTargetMonsters[0];

        foreach (Monster monster in nextTargetMonsters)
        {
            if (Vector3.Distance(monster.transform.position,pos)
                < Vector3.Distance(cloestMonster.transform.position,pos))
            {
                if (monster!=null)
                {
                    cloestMonster = monster;
                }
              
            }
        }
        return cloestMonster;

    }
    public static List<Monster> monstersInRangeAAndB(float inSideRadius,float outSideRadius)
    {
        List<Monster> _monstersInView = monstersInView;
        List<Monster> _monstersInRange = new List<Monster>();
        foreach (Monster monster in _monstersInView)
        {
            float distance = Vector3.Distance(monster.transform.position, Vector3.zero);
            if(distance>inSideRadius && distance < outSideRadius)
            {
                _monstersInRange.Add(monster);
            }
        }
        return _monstersInRange;

    }
    public static List<Monster> monstersInView
    {
        get
        {
            float viewRange = GunRangeManager.instance.rangeRadius;
            if (instance.monsters.Length == 0) return null;

            List<Monster> monstersInViewList = new List<Monster>();
            foreach (Monster monster in instance.monsters)
            {
                if (monster!=null)
                {
                    if (Vector3.Distance(monster.transform.position, Base.instance.transform.position)< viewRange)
                    {
                        if (monster.avaliable &&
                            !monster.confused )
                        {
                            monstersInViewList.Add(monster);
                        }
                    }
                }
               
            }
            return monstersInViewList;
        }
    }
    public static List<Monster> randomEnemiesList(int count)
    {
        List<Monster> targetMonsters = monstersInView;

        if (targetMonsters == null) return null;
        if (targetMonsters.Count == 0) return null;

      
        List<Monster> randomMonsterList = new List<Monster>();

        for (int i = 0; i < count; i++)
        {
            if (targetMonsters.Count > 0)
            {
                Monster monster = targetMonsters[Random.Range(0, targetMonsters.Count)];
                targetMonsters.Remove(monster);
                randomMonsterList.Add(monster);
            }

        }
        return randomMonsterList;
    }
    public void DestroyAliveMonsters()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters)
        {
            if (monster.burnedEffect!=null)
            {
                monster.burnedEffect.GetComponent<GameObjectPoolInfo>().RemoveFast();
                monster.burnedEffect.transform.parent = GameObjectPoolManager.instance.gameObject.transform;
            }
            Destroy(monster.gameObject);
        }
        MonsterShooterBullet[] bullets = FindObjectsOfType<MonsterShooterBullet>();
        foreach (MonsterShooterBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

        DiamondDrop[] diamondDrops = FindObjectsOfType<DiamondDrop>();
        foreach (DiamondDrop diamondDrop in diamondDrops)
        {
            Destroy(diamondDrop.gameObject);
        }

        LifeSlider[] lifeSliders = FindObjectsOfType<LifeSlider>();
        foreach (LifeSlider lifeSlider in lifeSliders)
        {
            lifeSlider.GetComponent<GameObjectPoolInfo>().RemoveFast();
        }

        MushRoomBomb[] mushRoomBombs = FindObjectsOfType<MushRoomBomb>();
        foreach (MushRoomBomb mushRoomBomb in mushRoomBombs)
        {
            Destroy(mushRoomBomb.gameObject);
        }

        GoldenEgg[] goldenEggs = FindObjectsOfType<GoldenEgg>();
        if (goldenEggs.Length>0)
        {
            foreach (GoldenEgg goldenEgg in goldenEggs)
            {
                Destroy(goldenEgg.gameObject);
            }
        }
    }



    public void AddWaveDataSettlement()
    {
        WaveDataSettlement waveDataSettlement = new WaveDataSettlement();
        waveDataSettlementList.Add(waveDataSettlement);
    }

    public WaveDataSettlement currentWaveDataSettement
    {
        get 
        {
            if (waveDataSettlementList.Count==0)
            {
                return null;
            }
            return waveDataSettlementList[waveDataSettlementList.Count - 1];
        }
    }
    
    public void CurrentWaveDataSettlementAddGoldFromMonster(int value)
    {
        currentWaveDataSettement.goldFromMonster+= value;
    }
    public void CurrentWaveDataSettlementAddGoldFromMine(int value)
    {
        currentWaveDataSettement.goldFromMine+=value;
    }
    public void CurrentWaveDataSettlementAddDiamondFromMine(int value)
    {
        currentWaveDataSettement.diamondFromMine+=value;
    }

    public void GetIntervalBetweenWaves()
    {
        spawnTimeMax = DataManager.instance.getCurrentValueByString("baseIntervalBetweenWaves");
    }





    public void AllMonstersCheckTarget()
    {
        foreach (Monster monster in monsters)
        {
            monster.GetCloestTargetUnit();
        }
    }

    public void CircleDamage(Vector3 centerPos,float radius,float damage)
    {
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(centerPos,monster.transform.position)<radius)
            {
                monster.Hitted(damage);
            }
        }
    }

    public int waveIndexReally
    {
        get
        {
            return waveIndex + (roundIndex - 1) * 16;
        }
    }

}

[System.Serializable]
public class WaveDataSettlement{

    public MonsterLevel monsterLevel;
    public int goldFromMonster;
    public int goldFromMine;
    public int diamondFromMine;
}