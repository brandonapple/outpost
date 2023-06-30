using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        inSpace,
        playing,
        gameOver,
    }
    public GameState thisGameState = GameState.playing;


    public ShipRoundData currentShipRoundData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ADCanvas adCanvas = Instantiate(Resources.Load<ADCanvas>("Prefab/UI/ADCanvas"));
            adCanvas.gameObject.name = "ADCanvas";
        }
    }

    public void InitGame()
    {
        FindObjectOfType<MapManager>().Start();

        UpgradeRootPanel.instance.GetCurrentBookMarks();
        UpgradeRootPanel.instance.RefreshBookMarksColor();
        UpgradeRootPanel.instance.RefreshBookMarksPos();

        UpgradeRootPanel.instance.InitWeaponBranch();
        UpgradeRootPanel.instance.GetCurrentBookMarks();

        MonsterManager.instance.GetMonsterGroupsData();
        MonsterManager.instance.GetSliderReduceSpeed();

        MinesManager.instance.GetMines();
      //  RelicManager.instance.WaveClean();
      
    }
    public void GameOver()
    {
        thisGameState = GameState.gameOver;
  
        GameSpeedManager.instance.TimeScaleBackToNormal();
        SettleMentPanel.instance.ShowSettleMent();
        if (StandInPanel.instance) StandInPanel.instance.Hide();

        DiscountManager.instance.WaveEnd();
        AbilityManager.instance.EmptyAllAbilities();
       
    }

    public void GameRoundBegin()
    {
        thisGameState = GameState.playing;
    }

    public void ShipGo()
    {
        if (TemporaryShieldController.instance) TemporaryShieldController.instance.ControllerOff();
        if(BodyCollectorDevice.instance) BodyCollectorDevice.instance.DeviceSwitchOff();
        if (StandInPanel.instance) StandInPanel.instance.ResetData();

        TalentManager.instance.ResetTalents();
        TalentPanel.instance.ResetAllTalentContainers();
        AbilityManager.instance.EmptyAllAbilities();
        MonsterManager.instance.ResetData();
       // RelicManager.instance.WaveClean();
        DevicesManager.instance.SwitchOffAllDevices();
        TalentPanel.instance.WaveCleanCheckTalentPoints();
        TalentPanel.instance.ResetTalentPointsButton();

        ShipRoundData shipRoundData = new ShipRoundData();
        shipRoundData.baseLifeLowest = 100;

        currentShipRoundData = shipRoundData;
    }

    public void ShipBack()
    {
        WeaponDataManager.instance.LoadData();
        CoinsManager.instance.LoseAllCoins();
        CoinsManager.instance.MoveDiamondsToMotherShip();

        AdditionalDiamondsButton.instance.Show();
        MainCam.instance.EndFellowChildShipInSpace();
        MainCam.instance.TurnToShowMotherShipSize();


        RelicManager.instance.ResetData();
        Base.instance.Start();

        TipPanel.instance.ShowUpdateWithDiamondTip();
        UpgradeRootPanel.instance.ShowUpgradeInSpace();

        CleanScene();

        HistoryManager.instance.CheckIfNewRelicUnlocked(currentShipRoundData);

    }

    public void ShipLunch()
    {
        thisGameState = GameState.inSpace;
    }
   
    public void BackToMotherShipButtonClick()
    {
        SpaceRoot.instance.ShipLunch();
        SpaceRoot.instance.SkipBackAnimation();
    }

    public void MotherShipSetSail()
    {
        SpaceRoot.instance.MotherShipSetSail();
        MainCanvas.instance.HideBasePanel();
    }
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void CleanScene()
    {
        MushRoomBomb[] mushRoomBombs = FindObjectsOfType<MushRoomBomb>();
        foreach (MushRoomBomb mushRoomBomb in mushRoomBombs)
        {
            Destroy(mushRoomBomb.gameObject);
        }

        GoldenEgg[] goldenEggs = FindObjectsOfType<GoldenEgg>();
        foreach (GoldenEgg goldenEgg in goldenEggs)
        {
            Destroy(goldenEgg.gameObject);
        }

        TreasureBox[] treasureBoxs = FindObjectsOfType<TreasureBox>();
        foreach (TreasureBox treasureBox in treasureBoxs)
        {
            Destroy(treasureBox.gameObject);
        }

    }




   
    public void UpdateValue(string valueName,int value)
    {
        int currentValue = currentShipRoundData.GetValueByString(valueName);
        if (currentValue < value)
        {
            currentShipRoundData.SetValueByString(valueName,value);
        }
    }

    public void UpdateValueMin(string valueName,int value)
    {
        int currentValue = currentShipRoundData.GetValueByString(valueName);
        if (currentValue>value)
        {
            currentShipRoundData.SetValueByString(valueName, value);
        }
    }

    public void ValuePlusOne(string valueName)
    {
        currentShipRoundData.ValuePlusOne(valueName);
    }
}



[System.Serializable]
public class ShipRoundData
{
    public int waveIndex;
    public int coinsCount;
    public int diamondsCount;
    public int refreshTime;
    public int monsterDeadCount;
    public int baseLifeLowest;
    public int upgradeTime;
    public int flameThrowerDamageLevel;
    public int relicGotCount;
    public int baseLifeMaxLevel;
    public int activewWeaponsCount;
    public int missileExplosionRadiusLevel;
    public int lightningFenceDamageLevel;
    public int viewRangeLevel;
    public int gunSpeedLevel;

    public int GetValueByString(string valueName)
    {
        return(int)GetType().GetField(valueName).GetValue(this);
    }
    public void SetValueByString(string valueName,int value)
    {
        GetType().GetField(valueName).SetValue(this, value);
    }
    public void ValuePlusOne(string valueName)
    {
        int value = (int)GetType().GetField(valueName).GetValue(this);
        GetType().GetField(valueName).SetValue(this,value + 1);
    }

}
