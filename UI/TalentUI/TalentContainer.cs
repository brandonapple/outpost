using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TalentContainer : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public bool unlocked;
    public bool got;

    public TalentContainer[] nextTalentContainers;
    [Space(10)]
    public string talentName;
    public string talentDescription;
    public Sprite talentIconSprite;
    [Space(10)]
    public Text talentNameText;
    public Text talentDescriptionText;
    public Image talentIconImage;
    public Image containerImage;

    public void Start()
    {
       // unlocked = false;
      //  got = false;
    }

    [ContextMenu("show icon")]
    public void ShowIcon()
    {
        talentIconImage.sprite = talentIconSprite;
        gameObject.name = "container"+"_" + talentName;
    }

    public void ShowState()
    {
        if (!got)
        {
            if (!unlocked)
            {
                containerImage.color = TalentPanel.instance.notGotLockedColor;
                talentIconImage.color = Color.grey;
            }
            else
            {
                containerImage.color = TalentPanel.instance.notGotUnlockedColor;
                talentIconImage.color = Color.white;
            }
        }
        else
        {
            containerImage.color = TalentPanel.instance.gotColor;
            talentIconImage.color = Color.white;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!unlocked) return;
        if (got) return;
        got = true;


        if (unlocked)
        {
          TalentPanel.instance.ChooseTalentContainer(this);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
       
        TalentDetailPanel.instance.ShowData(talentName, talentDescription);
        TalentDetailPanel.instance.SetPosition(transform.position + new Vector3(200,-200,0));
        TalentDetailPanel.instance.ShowPanel();
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TalentDetailPanel.instance.HidePanel();
    }


    public void UnlockedContainer()
    {
        got = true;
        ShowState();
    }

    private void OnDrawGizmos()
    {
        foreach (TalentContainer container in nextTalentContainers)
        {
            Gizmos.DrawLine(transform.position, container.transform.position);
        }
    }

}
