using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameEndPanel : MonoBehaviour
{
    public Image bgImage;
    public Text[] texts;

    public enum GameEndStateEnum { Idle,Settlement,NewStuff,Buttons}
    public GameEndStateEnum thisGameEndState = GameEndStateEnum.Idle;

    public GameObject settleMentPanel;
    public GameObject newStuffPanel;
    public GameObject buttonsPanel;

    public GameObject firstTimeUnlockedPanel;
    public GameObject secondTimeUnlockedPanel;
       

    public Color deepBlueColor;
    private void Awake()
    {
        bgImage = GetComponentInChildren<Image>();
        texts = settleMentPanel.GetComponentsInChildren<Text>();
        bgImage.color = new Color(0, 0, 0, 0);
        foreach (Text text in texts)
        {
            text.color= new Color(0,0,0,0);
        }
    }
    private IEnumerator Start()
    {
        bgImage.DOColor(deepBlueColor, 2);
        yield return new WaitForSeconds(2);
        foreach (Text text in texts)
        {
            text.DOColor(Color.white, 1);
        }
        SpaceRoot.instance.DestroyGameObject();
        thisGameEndState = GameEndStateEnum.Settlement;
    }
    private void Update()
    {
        switch (thisGameEndState)
        {
            case GameEndStateEnum.Idle:
                break;
            case GameEndStateEnum.Settlement:
                if (Input.GetMouseButtonDown(0))
                {
                    thisGameEndState = GameEndStateEnum.NewStuff;
                    HidePael(settleMentPanel);
                    ShowPanel(newStuffPanel);

                    if (GamePlaythroughManager.instance.playThroughTime==0)
                    {
                        firstTimeUnlockedPanel.gameObject.SetActive(true);
                        secondTimeUnlockedPanel.gameObject.SetActive(false);
                    }
                    else if (GamePlaythroughManager.instance.playThroughTime==1)
                    {
                        firstTimeUnlockedPanel.gameObject.SetActive(false);
                        secondTimeUnlockedPanel.gameObject.SetActive(true);
                    }
                    else
                    {
                        thisGameEndState = GameEndStateEnum.Buttons;
                        HidePael(newStuffPanel);
                        ShowPanel(buttonsPanel);
                    }
                }
                break;
            case GameEndStateEnum.NewStuff:
                if (Input.GetMouseButtonDown(0))
                {
                    thisGameEndState = GameEndStateEnum.Buttons;
                    HidePael(newStuffPanel);
                    ShowPanel(buttonsPanel);
                }

                break;
            default:
                break;
        }
    }










    public void NewRoundButtonClick()
    {
        GamePlaythroughManager.instance.AddGamePlayThroughTime();
        CoinsManager.instance.ResetButtonClick();
        HistoryManager.instance.ResetData();
       
        FindObjectOfType<WeaponDataManager>().ResetData();
      
        Invoke(nameof(NewRound), 1);
        HidePael(buttonsPanel);
    }

    void NewRound()
    {
        GameManager.instance.ReloadScene();
    }


    void ShowPanel(GameObject _panel)
    {
        _panel.transform.DOLocalMoveY(0, .25f);
    }
    void HidePael(GameObject _panel)
    {
        _panel.transform.DOLocalMoveY(2000, .25f);
    }


    public void QuitButtonClick()
    {
        Application.Quit();
    }

   

}
