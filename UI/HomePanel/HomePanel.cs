using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomePanel : MonoBehaviour
{

    public enum HomePageState { Option,RelicLibrary,MonsterLibrary};
    public HomePageState thisHomePageState = HomePageState.Option;

    public Button optionButton, relicLibraryButton, monsterLibraryButton;

    public OptionPanel optionPanel;
    public RelicLibraryPanel relicLibraryPanel;
    public MonsterLibraryPanel monsterLibraryPanel;

    private void Start()
    {
        ShowPage();
        ShowButtonsState();
    }
    public void CloseButtonClick()
    {
        MainCanvas.instance.ShowBasePanel();
        GameSpeedManager.instance.GameUnPause();
        gameObject.SetActive(false);
    }

    public void ShowButtonsState()
    {
        optionButton.GetComponent<Image>().color = Color.white;
        relicLibraryButton.GetComponent<Image>().color = Color.white;
        monsterLibraryButton.GetComponent<Image>().color = Color.white;
        switch (thisHomePageState)
        {
            case HomePageState.Option:
                optionButton.GetComponent<Image>().color = Color.green;
                break;
            case HomePageState.RelicLibrary:
                relicLibraryButton.GetComponent<Image>().color = Color.green;
                break;
            case HomePageState.MonsterLibrary:
                monsterLibraryButton.GetComponent<Image>().color = Color.green;
                break;
            default:
                break;
        }
    }

    public void ShowPage()
    {
        switch (thisHomePageState)
        {
            case HomePageState.Option:
                optionPanel.gameObject.SetActive(true);
                relicLibraryPanel.gameObject.SetActive(false);
                monsterLibraryPanel.gameObject.SetActive(false);
                break;
            case HomePageState.RelicLibrary:
                optionPanel.gameObject.SetActive(false);
                relicLibraryPanel.gameObject.SetActive(true);
                monsterLibraryPanel.gameObject.SetActive(false);
                break;
            case HomePageState.MonsterLibrary:
                optionPanel.gameObject.SetActive(false);
                relicLibraryPanel.gameObject.SetActive(false);
                monsterLibraryPanel.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void OptionButtonClick()
    {
        thisHomePageState = HomePageState.Option;
        ShowButtonsState();
        ShowPage();
    }
    public void RelicLibraryButtonClick()
    {
        thisHomePageState = HomePageState.RelicLibrary;
        ShowButtonsState();
        ShowPage();
    }
    public void MonsterLibraryButtonClick()
    {
        thisHomePageState = HomePageState.MonsterLibrary;
        ShowButtonsState();
        ShowPage();
    }
}
