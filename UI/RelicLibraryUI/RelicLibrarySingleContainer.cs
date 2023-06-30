using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class RelicLibrarySingleContainer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public RelicDataSO currentRelicDataSO;

    public Image containerBGImage;
    public Image relicIconImage;
    public Text relicNameText;
    public void ShowRelicData()
    {
        if (currentRelicDataSO.unlocked)
        {
            containerBGImage.color = RelicLibraryPanel.instance.unlockContainerBGColor;
            relicIconImage.sprite = currentRelicDataSO.relicSprite;
            relicNameText.text = currentRelicDataSO.relicNameChinese ;
            relicNameText.gameObject.SetActive(true);
        }
        else
        {
            containerBGImage.color = RelicLibraryPanel.instance.lockContainerBGColor;
            relicIconImage.sprite = RelicLibraryPanel.instance.lockSprite;
            relicNameText.text = "";
            relicNameText.gameObject.SetActive(false);
           
        }

      
        float a =(float) relicIconImage.sprite.texture.height /(float) relicIconImage.sprite.texture.width;
     
        if (a > 1)
        {
            relicIconImage.GetComponent<RectTransform>().sizeDelta = new Vector2(1, a)*80;
        }
        else
        {
            relicIconImage.GetComponent<RectTransform>().sizeDelta = new Vector2(1/a, 1) * 80;
        }
       
       
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (currentRelicDataSO.unlocked)
        {
            RelicLibrarySingleDetailPanel.instance.ShowPanel(currentRelicDataSO.relicDescriptionChinese,transform.position + new Vector3(200,-200));
        }
        else
        {
            RelicLibrarySingleDetailPanel.instance.ShowPanel(
                "解锁条件 ：" +"\n" + currentRelicDataSO.UnlockedConditionString()
                , transform.position + new Vector3(200, -200));
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        RelicLibrarySingleDetailPanel.instance.HidePanel();
    }
}
