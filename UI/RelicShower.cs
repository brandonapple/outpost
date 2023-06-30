using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RelicShower : MonoBehaviour
{
    public string relicName;
    public Sprite relicIcon;
    public int relicCount;

   // public Text relicNameText;
    public Text relicCountText;
    public Image relicIconImage;

    public void Refresh()
    {
        RelicDataSO[] relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");

        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            if (relicDataSO.relicName == relicName)
            {
                relicIcon = relicDataSO.relicSprite;
            }
        }

        relicCountText.text = relicCount.ToString();
        if (relicCount==1)
        {
            relicCountText.gameObject.SetActive(false);
        }
        else
        {
            relicCountText.gameObject.SetActive(true);
        }


        relicIconImage.sprite = relicIcon;
        relicIconImage.SetNativeSize();
    }

    public void OnClick()
    {
        Debug.Log("click");
        RelicDetailPanel.instance.ShowRelicShowerDetail(relicName, transform.localPosition);
    }
}
