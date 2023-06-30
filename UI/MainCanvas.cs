using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas instance;
    public GameOverPanel gameOverPanel;
    // public OptionPanel optionPanel;

    public HomePanel homePanel;

    public GameObject wavePanel;
    public GameObject weaponDamageSliderManager;
    public GameObject gameSpeedPanel;
    public GameObject coinsManager;
    public GameObject settleMentPanel;
    public GameObject dataTestPanel;

    public GameObject basePanel;
    public GameObject standInPanel;
    public TalentPanel talentPanel;
    private void Awake()
    {
        instance = this;
        gameOverPanel.gameObject.SetActive(true);
        settleMentPanel.gameObject.SetActive(true);
       // optionPanel.gameObject.SetActive(true);
        dataTestPanel.gameObject.SetActive(true);
        basePanel.gameObject.SetActive(true);
        talentPanel.gameObject.SetActive(true);
        talentPanel.HidePanelFast();
        homePanel.gameObject.SetActive(false);

        if (Application.platform != RuntimePlatform.Android)
        {
            standInPanel.gameObject.SetActive(false);
        }
    }

    public static void SpawnRelicBooty()
    {
        RelicBootyPanel relicBootyPanelPrefab = Resources.Load<RelicBootyPanel>("Prefab/UI/RelicBootyPanel_prefab");
        RelicBootyPanel relicBootyPanel = Instantiate(relicBootyPanelPrefab);
        relicBootyPanel.transform.parent = instance.transform;
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnRelicBooty();
        }
    }


    public void GameRoundBegin()
    {
        wavePanel.gameObject.SetActive(true);
        weaponDamageSliderManager.gameObject.SetActive(true);
       // gameSpeedPanel.gameObject.SetActive(true);
        GameSpeedManager.instance.ShowSpeedChanger();
        gameSpeedPanel.GetComponent<GameSpeedManager>().Start();


        if (UpgradeRootPanel.instance)
        {
           UpgradeRootPanel.instance.ShowUpgradeInPlant();
        }


        UpgradeRootPanelPermanentLevel.instance.LoadDatas();
    }

    public void GameRoundEnd()
    {
        if (GameSpeedManager.instance != null)
        {
            GameSpeedManager.instance.TimeScaleBackToNormal();
        }
        wavePanel.gameObject.SetActive(false);
        weaponDamageSliderManager.gameObject.SetActive(false);

        DataManager.LoadData();
        WeaponDataManager.instance.LoadData();
       // HideBasePanel();
    }

    public void ShipLunchButtonClick()
    {
        GameRoundEnd();
        SpaceRoot.instance.ShipLunch();
        LunchButton.instance.HideButton();
        SettleMentPanel.instance.ShowSettleMent();
       

        if (StandInPanel.instance)
        {
          StandInPanel.instance.Hide();
        }

        //  HideBasePanel();

        DiscountManager.instance.WaveEnd();

    }

    public void OptionsButtonClick()
    {
      //  optionPanel.ShowPanel();
        HideBasePanel();
        GameSpeedManager.instance.GamePause();
    }
    public void HomeButtonClick()
    {
        homePanel.gameObject.SetActive(true);
        HideBasePanel();
        GameSpeedManager.instance.GamePause();
    }

    public void TalentButtonClick()
    {
        AudioManager.PlayClip("talentPanelShow");
        TalentPanel.instance.ShowPanel();
        HideBasePanel();
        AbilityManager.instance.HidePanel();
        GameSpeedManager.instance.GamePause();
    }
    public void RelicLibraryButtonClick()
    {

    }

    public void ShowCoinManager()
    {
        coinsManager.gameObject.SetActive(true);
    }


    public void ShowBasePanel()
    {
        basePanel.gameObject.SetActive(true);
    }
    public void HideBasePanel()
    {
        basePanel.gameObject.SetActive(false);
    }

    public float canvasScaleMultiplier
    {
        get
        {
            return transform.localScale.x;
        }
    }
    public float canvasScreenWidthMultiplier
    {
        get
        {
            return Screen.width / 1920;
        }
    }
    public float canvasScreenHeightMultiplier
    {
        get
        {
            return Screen.height / 1050;
        }
    }
}
