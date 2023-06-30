using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    public static bool bgmOn;
    public static bool damageValueTextOn;
    public static bool effectOn;
    public static bool camShakeOn;
    public static float audioVolumeValue;
    public static float viewRangeValue;
    public static int languageIndex;
    public static bool fullScreenOn;

    public Toggle bgmOnToggle;
    public Toggle damageValueTextOnToggle;
    public Toggle effectOnToggle;
    public Toggle camShakeOnToggle;
    public Toggle fullScreenOnToggle;

    public Slider audioVolumeValueSlider;
    public Slider viewRangeValueSlider;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Text[] languageTexts;

    public AudioMixer audioMixer;
    public Text gameModeText;

    public GameObject resolutionDropDownPanel;

    private void Awake()
    {
        instance = this;
        LoadData();
        LanguageManager.GetLanguage();
        UpdateToggles();



        if (Application.platform == RuntimePlatform.Android)
        {
            fullScreenOnToggle.transform.parent.gameObject.SetActive(false);
            resolutionDropDownPanel.gameObject.SetActive(false);
        }


    }
    private void Start()
    {
        SetAudioMixer();
        GetResolutions();
    }
    public void BGMToggleClick(bool isOn)
    {
        bgmOn = isOn;
        SaveData();
        FindObjectOfType<BGMPlayer>().LoadData();
    }
    public void DamageValueTextToggleClick(bool isOn)
    {
        damageValueTextOn = isOn;
        SaveData();
    }
    public void EffectToggleClick(bool isOn)
    {
        effectOn = isOn;
        SaveData();
    }
    public void CamShakeToggleClick(bool isOn)
    {
        camShakeOn = isOn;
        SaveData();
    }

    public void FullScreenToggleClick(bool isOn)
    {
        fullScreenOn = isOn;
        Screen.fullScreen = fullScreenOn;
    }

    public void SetResolution(int resutionIndex)
    {
        Resolution resolution = resolutions[resutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void AudioValueSliderChange(float value)
    {
        audioVolumeValue = value;
        SaveData();
        SetAudioMixer();
    }
    public void ViewRangeSliderChange(float value)
    {
        viewRangeValue = value;
        SaveData();
        if (MainCam.instance)
        {
         MainCam.instance.ChangeViewRange();
        }
    }

    public void LanguageButtonClick(int _languageIndex)
    {
       //Debug.Log("change language");
        languageIndex = _languageIndex;
        SaveData();
        UpdateToggles();
        LanguageManager.GetLanguage();

        TextLoader[] textLoaders = FindObjectsOfType<TextLoader>();
        foreach (TextLoader textLoader in textLoaders)
        {
            textLoader.GetText();
        }

        GameSpeedManager.instance.UpdateTimeText();
    }

    
    public void LoadData()
    {
        OptionsData data = SaveSystem.LoadOptionsData();
        if (data == null)
        {
            ResetData();
        }
        else
        {
            bgmOn = data.bgmOn;
            damageValueTextOn = data.damageValueTextOn;
            effectOn = data.effectOn;
            camShakeOn = data.camShakeOn;
            

            audioVolumeValue = data.audioVolumeValue;
            viewRangeValue = data.viewRangeValue;
            languageIndex = data.languageIndex;
        }
    }
    public void SaveData()
    {
        SaveSystem.SaveOptionsData();
    }
    public void UpdateToggles()
    {
        bgmOnToggle.isOn = bgmOn;
        damageValueTextOnToggle.isOn = damageValueTextOn;
        effectOnToggle.isOn = effectOn;
        camShakeOnToggle.isOn = camShakeOn;


        fullScreenOnToggle.isOn = Screen.fullScreen;

        audioVolumeValueSlider.value = audioVolumeValue;
        viewRangeValueSlider.value = viewRangeValue;

        foreach (Text text in languageTexts)
        {
            text.color = Color.white;
        }
        languageTexts[languageIndex].color = Color.yellow;


    }
    void SetAudioMixer()
    {
        audioMixer.SetFloat("effectVolume", Mathf.Lerp(-80, 20, audioVolumeValue));
    }

    public void EmptyDataButtonClick()
    {
        //CoinsManager.instance.ResetButtonClick();
        //HistoryManager.instance.ResetData();
        //ResetData();
        EmptyDataPanel emptyDataPanel = Instantiate(Resources.Load<EmptyDataPanel>("Prefab/UI/EmptyDataPanelPrefab"));
        emptyDataPanel.transform.parent = transform.parent.transform;
        emptyDataPanel.transform.localPosition = Vector3.zero;
        emptyDataPanel.transform.localScale = Vector3.one;
        gameObject.GetComponent<OptionPanel>().CloseButtonClick();
        
    }
    public void QuitButtonClick()
    {
        Application.Quit();
    }

    [ContextMenu("reset data")]
    public void ResetData()
    {
        bgmOn = true;
        damageValueTextOn = false;
        effectOn = true;
        camShakeOn = true;
        audioVolumeValue = .75f;
        viewRangeValue = 1f;
        languageIndex = 1;

        SaveData();
    }

    public void ShowGameModeText()
    {
        string gameModeString = "";
        string miningModeString = "";
        string mapString = "";
        string weaponGroupString = "";

        if (DataManager.instance.miningModeIndex==0)
        {
            miningModeString = "挖矿";
            miningModeString = LanguageManager.GetText("mining");
        }
        else
        {
            miningModeString = //"捡钱";
                LanguageManager.GetText("pickDiamond");
        }

        if (DataManager.instance.mapIndex==0)
        {
            mapString = //"火星";
                LanguageManager.GetText("Mars");
        }
        else
        {
            mapString = //"木星";
                LanguageManager.GetText("Jupiter");
        }

        if (DataManager.instance.weaponGroupIndex == 0)
        {
            weaponGroupString = //"武器模组A";
                LanguageManager.GetText("weaponGroupA");
        }
        else if (DataManager.instance.weaponGroupIndex == 1)
        {
            weaponGroupString =// "武器模组B";
                LanguageManager.GetText("weaponGroupB");
        }
        else if (DataManager.instance.weaponGroupIndex ==2)
        {
            weaponGroupString = //"自由模组";
                LanguageManager.GetText("weaponGroupFree");
        }
        gameModeString = //"当前游戏模式："
            LanguageManager.GetText("currentGameMode")+": "+
             miningModeString +","+ mapString+"," + weaponGroupString;



        gameModeText.text = gameModeString;

    }

    void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " × " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
      //  resolutionDropdown.value = currentResolutionIndex;
       // resolutionDropdown.RefreshShownValue();
    }
}
