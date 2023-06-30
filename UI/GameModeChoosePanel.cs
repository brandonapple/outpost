using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameModeChoosePanel : MonoBehaviour
{
    public Color softRedColor;
    public Color softDarkColor;


    [Space(20)]
    public Button nextButton;
    public Button lastButton;

    public Text titleText;
    public Image gameModeChoosePanel;
    public Image mapChoosePanel;
    public Image weaponChoosePanel;

    public int gamePlaythroughTime;



    public int miningModeIndex;
    public int mapIndex;
    public int weaponGroupIndex;

    public Image chooseBox;


    [Space(20)]

    public GameModeOptionContainer miningModePickDiamondOptionContainer;
    public GameModeOptionContainer mapBOptionContainer;

    public GameModeOptionContainer weaponGroupBOptionContainer;
    public GameModeOptionContainer weaponGroupCOptionContainer;
    public GameModeOptionContainer weaponGroupFreeChooseOptionContainer;

    public enum ChooseEnum
    {
        miningMode,map,weapons
    }
    public ChooseEnum thisChooseEnum = ChooseEnum.miningMode;

    private void Start()
    {
        miningModeIndex = DataManager.instance.miningModeIndex;
        mapIndex = DataManager.instance.mapIndex;
        weaponGroupIndex = DataManager.instance.weaponGroupIndex;

        LoadDisplay();
    }
    public void LoadDisplay()
    {
        gamePlaythroughTime = GamePlaythroughManager.instance.playThroughTime;

        gameModeChoosePanel.gameObject.SetActive(false);
        mapChoosePanel.gameObject.SetActive(false);
        weaponChoosePanel.gameObject.SetActive(false);

        nextButton.gameObject.SetActive(true);
        lastButton.gameObject.SetActive(true);


        GameModeOptionContainer[] containers;
        switch (thisChooseEnum)
        {
            case ChooseEnum.miningMode:
                gameModeChoosePanel.gameObject.SetActive(true);
                lastButton.gameObject.SetActive(false);

              

                if (GamePlaythroughManager.instance.miningModeAutoSpawnUnlocked)
                {
                    miningModePickDiamondOptionContainer.UnlockOption();
                }
                else
                {
                    miningModePickDiamondOptionContainer.LockOption();
                }

                containers = gameModeChoosePanel.GetComponentsInChildren<GameModeOptionContainer>();
                chooseBox.transform.position = containers[miningModeIndex].transform.position;

                titleText.text =// "挖矿模式选择";
                    LanguageManager.GetText("miningModeChoose");
                break;
            case ChooseEnum.map:
                mapChoosePanel.gameObject.SetActive(true);

              

                if (GamePlaythroughManager.instance.mapBUnlocked)
                {
                    mapBOptionContainer.UnlockOption();
                }
                else
                {
                    mapBOptionContainer.LockOption();
                }


                containers = mapChoosePanel.GetComponentsInChildren<GameModeOptionContainer>();
                chooseBox.transform.position = containers[mapIndex].transform.position;

                titleText.text = //"地图选择";
                    LanguageManager.GetText("mapChoose");
                break;
            case ChooseEnum.weapons:
                weaponChoosePanel.gameObject.SetActive(true);

                if (GamePlaythroughManager.instance.weaponGroupsBUnlocked)
                {
                    weaponGroupBOptionContainer.UnlockOption();
                }
                else
                {
                    weaponGroupBOptionContainer.LockOption();
                }

                if (GamePlaythroughManager.instance.weaponGroupCUnlocked)
                {
                    weaponGroupCOptionContainer.UnlockOption();
                }
                else
                {
                    weaponGroupCOptionContainer.LockOption();
                }

                if (GamePlaythroughManager.instance.weaponGroupFreeUnlocked)
                {
                    weaponGroupFreeChooseOptionContainer.UnlockOption();
                }
                else
                {
                    weaponGroupFreeChooseOptionContainer.LockOption();
                }

             

                containers = weaponChoosePanel.GetComponentsInChildren<GameModeOptionContainer>();
                chooseBox.transform.position = containers[weaponGroupIndex].transform.position;
                titleText.text = //"武器模组选择";
                    LanguageManager.GetText("weaponGroupChoose");
                break;
            default:
                break;
        }

    }
    public void NextButtonClick()
    {
        switch (thisChooseEnum)
        {
            case ChooseEnum.miningMode:
                thisChooseEnum = ChooseEnum.map;
                break;
            case ChooseEnum.map:
                thisChooseEnum = ChooseEnum.weapons;
                break;
            case ChooseEnum.weapons:
                Destroy(gameObject);
                
                if (weaponGroupIndex != 4)
                {
                    WeaponDataManager.instance.DisableAllWeapon();


                    MainCanvas.instance.ShowBasePanel();

                    DataManager.instance.miningModeIndex = miningModeIndex;
                    DataManager.instance.mapIndex = mapIndex;
                    DataManager.instance.weaponGroupIndex = weaponGroupIndex;

                    DataManager.instance.GameModeChoose();
                    GameManager.instance.InitGame();
                }


                break;
            default:
                break;
        }

        LoadDisplay();
    }

    public void LastButtonClick()
    {
        switch (thisChooseEnum)
        {
            case ChooseEnum.miningMode:
                break;
            case ChooseEnum.map:
                thisChooseEnum = ChooseEnum.miningMode;
                break;
            case ChooseEnum.weapons:
                thisChooseEnum = ChooseEnum.map;
                break;
            default:
                break;
        }

        LoadDisplay();
    }
   

    public void MiningModeChoose(int index)
    {
        miningModeIndex = index;
        LoadDisplay();
    }
    public void MapChoose(int index)
    {
        mapIndex = index;
        LoadDisplay();
    }
    public void WeaponsChoose(int index)
    {
        weaponGroupIndex = index;
        LoadDisplay();
    }

}
