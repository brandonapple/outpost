using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StandInPanel : MonoBehaviour
{
    public static StandInPanel instance;
    public int standInCount;

    public Text lastCountText;
    public Image adIcon;
    public Image closeButton;

    float timer;
    public Image cdMask;
    public Image lightingFlowOutLine;
    public Image lightningFlow;

    public Text infoText;
    public Image battery;

    public Image pager;

    bool ready;

    private void Awake()
    {
        instance = this;
        transform.localScale = Vector3.zero;
      //  ResetData();
    }
    public void ResetData()
    {
        transform.localScale = Vector3.one;
        standInCount = 1;
        timer = 0;
        UpdateDisplay();
        RefreshRender();
        ready = true;
    }
    public void StandInButtonClick()
    {
        if (!ready) return;
        if (standInCount>0)
        {

        }
        else
        {
            ADCanvas.instance.ShowFullScreenAD();
            ClosePanelButtonClick();
        }
        StandInBullet standInBullet = Instantiate(Resources.Load<StandInBullet>("Bullets/standInBullet"));
        standInBullet.transform.position = new Vector3(0, .25f, 0);
        standInCount--;
        UpdateDisplay();
        timer = 0;
        transform.DOShakeRotation(.5f, 10, 40, 10, true);

        RefreshRender();

        pager.color = Color.grey;
        ready = false;
        StartCoroutine(ReadyIE());
        IEnumerator ReadyIE()
        {
            yield return new WaitForSeconds(2);
            ready = true;
            pager.color = Color.white;
        }

    }
    public void UpdateDisplay()
    {
        
    }

    public void ClosePanelButtonClick()
    {
        Hide();
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }

   


    void RefreshRender()
    {
        if (standInCount>0)
        {
            battery.gameObject.SetActive(true);
            infoText.text = "免费";
        }
        else
        {
            battery.gameObject.SetActive(false);
            infoText.text = "广告";
        }
    }
}
