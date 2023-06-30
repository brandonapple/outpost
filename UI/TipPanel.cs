using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TipPanel : MonoBehaviour
{
    public static TipPanel instance;

    public GameObject updateWithDiamondTip;
    public GameObject updateWithGoldTip;
    public GameObject skipAnimationTip;
    public GameObject changeGameSpeedTip;
    public GameObject lunchShipTip;

    public GameObject currentShip;

    public GameObject unlockedAtLeastOneWeaponTip;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        TipDetailContent[] tipDetailContents = GetComponentsInChildren<TipDetailContent>();
        foreach (TipDetailContent tipDetailContent in tipDetailContents)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                tipDetailContent.ShowOnMobile();
            }
            else
            {
                tipDetailContent.ShowOnPC();
            }
        }
        
    }
    public void ShowTip(GameObject _tip)
    {
        if (currentShip != null) Destroy(currentShip);

        currentShip = _tip;
        currentShip.transform.localScale = Vector3.zero;
        currentShip.gameObject.SetActive(true);
        currentShip.transform.DOScale(1, .15f);


        if (currentShip.TryGetComponent(out TipDetailContent tipDetailContent))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                tipDetailContent.ShowOnMobile();
            }
            else
            {
                tipDetailContent.ShowOnPC();
            }
        }


        StartCoroutine(HideTipIE());

        IEnumerator HideTipIE()
        {
            yield return new WaitForSeconds(5);
            if (currentShip != null)
            {
              currentShip.transform.DOScale(0, .15f);
              Destroy(currentShip, .15f);
            }
            currentShip = null;
        }

    }
    public void ShowUpdateWithDiamondTip()
    {
        if (!HistoryManager.tipUpgraedeWithDiamondShowed)
        {
            HistoryManager.tipUpgraedeWithDiamondShowed = true;
            HistoryManager.instance.SaveData();

            if (updateWithDiamondTip.gameObject == null) return;
            ShowTip(updateWithDiamondTip);
        }
      
    }
    public void ShowUpdateWithGoldTip()
    {
        if (!HistoryManager.tipUpgradeWithGoldShowed)
        {
            HistoryManager.tipUpgradeWithGoldShowed = true;
            HistoryManager.instance.SaveData();
            if (updateWithGoldTip.gameObject == null) return;
            ShowTip(updateWithGoldTip);
        }
    }
    public void ShowSkipAnimationTip()
    {
        if (!HistoryManager.tipSkipAnimationShowed)
        {
            HistoryManager.tipSkipAnimationShowed = true;
            HistoryManager.instance.SaveData();
            if (skipAnimationTip == null) return;
            ShowTip(skipAnimationTip);
        }
       
    }
    public void ShowChangeGameSpeedTip()
    {
        if (!HistoryManager.tipChangeGameSpeedShowed)
        {
            HistoryManager.tipChangeGameSpeedShowed = true;
            HistoryManager.instance.SaveData();
            if (changeGameSpeedTip.gameObject == null) return;
            ShowTip(changeGameSpeedTip);
        }
      
    }
    public void ShowLunchShipTip()
    {
        if (!HistoryManager.tipLunchShipShowed)
        {
            HistoryManager.tipLunchShipShowed = true;
            HistoryManager.instance.SaveData();
            if (lunchShipTip.gameObject == null) return;
            ShowTip(lunchShipTip);
        }
    }

    public void ShowUnlockedAtLeastOneWeaponTip()
    {
        unlockedAtLeastOneWeaponTip.gameObject.SetActive(true);
        ShowTip(unlockedAtLeastOneWeaponTip);

        StopCoroutine(HideUnlockedAtLeastOneWeaponPanelIE());
        StartCoroutine(HideUnlockedAtLeastOneWeaponPanelIE());
        IEnumerator HideUnlockedAtLeastOneWeaponPanelIE()
        {
            yield return new WaitForSeconds(1.5f);
            unlockedAtLeastOneWeaponTip.gameObject.SetActive(false);
        }
    }



    public void ShowTipSingle(string tipContent)
    {
        GameObject tipText = Instantiate(Resources.Load<GameObject>("Prefab/UI/tipSingle"));
        tipText.transform.transform.parent = transform;
        tipText.transform.localPosition = Vector3.zero;
        tipText.GetComponentInChildren<Text>().text = tipContent;

        tipText.transform.localScale = Vector3.zero;
        tipText.transform.DOScale(1, .25f).SetUpdate(true);
        
        StartCoroutine(FadeOut());
        IEnumerator FadeOut()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(tipText.gameObject, .25f);
            tipText.transform.DOScale(0, .25f).SetUpdate(true);
        }
    }
}
