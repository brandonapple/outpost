using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public static OptionPanel instance;
    private void Awake()
    {
        instance = this;
      //  transform.localScale = Vector3.zero;
    }
    public void CloseButtonClick()
    {
       // transform.localScale = Vector3.zero;

        MainCanvas.instance.ShowBasePanel();
        GameSpeedManager.instance.GameUnPause();
    }

    public void ShowPanel()
    {
        transform.localScale = Vector3.one;
        FindObjectOfType<OptionsManager>().ShowGameModeText();
    }

    public void QuickChooseModeButtonClick()
    {
        if (GameManager.instance.thisGameState == GameManager.GameState.playing)
        {
            TipPanel.instance.ShowTipSingle(LanguageManager.GetText("cantChangeModeWhenFightning"));
            return;
        }
        CloseButtonClick();

        GameModeChoosePanel gameModeChoosePanel = Instantiate(Resources.Load<GameModeChoosePanel>("Prefab/UI/GameModeChoosePanel"));
        gameModeChoosePanel.transform.parent = transform.parent.transform;
        gameModeChoosePanel.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPanel();
        }
    }
}
