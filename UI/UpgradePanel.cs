using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UpgradePanel : MonoBehaviour
{
    public static UpgradePanel instance;
    public Button upgradeButton;
    
    private void Awake()
    {
        instance = this;
        HideUpgradePanel();
    }
  
    public void UpgradeButtonClick()
    {
        upgradeButton.transform.rotation = Quaternion.Euler(0, 0, 0);
        upgradeButton.transform.DOShakeRotation(.15f, 10, 10, 10, true);
       
    }
   
    public void ShowButton()
    {
        upgradeButton.gameObject.SetActive(true);
    }
   

    public void ShowUpgradePanel()
    {
        upgradeButton.gameObject.SetActive(false);
        UpgradeRootPanel.instance.ShowOrHideButton();
    }
    public void HideUpgradePanel()
    {
        upgradeButton.gameObject.SetActive(true);
    }
    public void HideUpgradeButton()
    {
        upgradeButton.gameObject.SetActive(false);
    }

   
}
