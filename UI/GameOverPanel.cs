using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameOverPanel : MonoBehaviour
{

    public static GameOverPanel instance;

    private void Awake()
    {
        instance = this;
        HideFast();
    }
    public void ResearchButtonClick()
    {
        GameManager.instance.BackToMotherShipButtonClick();
        Hide();
        MonsterManager.instance.DestroyAliveMonsters();
        DialoguePanel.instance.StartDialoguesWithString("lose",1);
   
    }
    public void QuitButtonClick()
    {
        Application.Quit();
    }

    public void Show()
    {
        transform.DOScale(1, .1f);
    }
    public void Hide()
    {
        transform.DOScale(0, .1f);
    }
    public void HideFast()
    {
        transform.localScale = Vector3.zero;
    }
}
