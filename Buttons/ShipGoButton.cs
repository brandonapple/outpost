using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipGoButton : MonoBehaviour
{
    bool showing;
    private void Start()
    {
        showing = true;
    }
    private void OnMouseDown()
    {
        bool atLeastOneWeaponUnlocked = UpgradeRootPanel.instance.AtLeastOneWeaponUnlocked;
        if (!atLeastOneWeaponUnlocked)
        {
            Debug.Log("at least one weapon unlock ");
            TipPanel.instance.ShowUnlockedAtLeastOneWeaponTip();
            return;
        }

        showing = false;
        transform.DOScale(0, .25f);
        FindObjectOfType<ChildShip>().ShipGO();

        MinesManager.instance.GetMines();
        BGMPlayer.instance.GetBGM();
        GameManager.instance.ShipGo();
    }


    public void Show()
    {
        showing = true;
    }

    private void Update()
    {
        if (showing)
        {
            transform.localScale = Vector3.one * (1 + Mathf.Cos(Time.time *5) * .15f);
        }
       
    }
}
