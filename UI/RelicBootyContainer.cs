using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class RelicBootyContainer : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Text relicNameText;
    public Text relicDetailText;
    public Image relicIconImage;
    public string relicName;
    public string specialValue;
    public bool plus;

    public Sprite normalRelicCard;
    public Sprite plusRelicCard;
    bool working;
   public void GetData(RelicDataSO _relicDataSO)
    {
        relicName = _relicDataSO.relicName;
        //relicNameText.text = LanguageManager.GetText(_relicDataSO.relicName);
        //relicDetailText.text = LanguageManager.GetText(_relicDataSO.relicName+"Detail");
        if (LanguageManager.currentLanguage=="chinese")
        {
            relicNameText.text = _relicDataSO.relicNameChinese;
            relicDetailText.text = _relicDataSO.relicDescriptionChinese;
        }
        else if (LanguageManager.currentLanguage == "english")
        {
            relicNameText.text = _relicDataSO.relicName;
            relicDetailText.text = _relicDataSO.relicDescriptionEnglish;
        }

        if (plus)
        {
           // relicNameText.color = Color.green;
            relicNameText.text = relicNameText.text + "+";
          //  relicIconImage.color = Color.green;
            GetComponent<Image>().sprite = plusRelicCard;
        }


        relicIconImage.sprite = _relicDataSO.relicSprite;
        relicIconImage.SetNativeSize();


        if (_relicDataSO.specialValueNameString!=null)
        {
            if (_relicDataSO.specialValueNameString !="")
            {
                specialValue = RelicManager.instance.GetSpecialValueByName(_relicDataSO.specialValueNameString);
                float _addValue = _relicDataSO.addValue;
                if (plus) _addValue *= 2;

                string addValueString = _addValue.ToString();
                if (_relicDataSO.percentFormat)
                {
                    specialValue = (float.Parse(specialValue) * 100).ToString() + "%";
                    addValueString = (_addValue * 100).ToString() + "%";
                }
                if (_relicDataSO.addValue==0)
                {
                    relicDetailText.text = relicDetailText.text;
                }
                else
                {
                    relicDetailText.text = relicDetailText.text + "\n" + "<color=black>" + LanguageManager.GetText("current")
                  + " (" + specialValue + ") " + "</color>" + "\n" + "<color=yellow>" + LanguageManager.GetText("add")
                  +" ("+ addValueString+") " + "</color>";
                }

              
            }
        }

    }


    public void OnPointerUp(PointerEventData pointerEventData)
    {
     //   Debug.Log("mouse up");
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        GetComponentInParent<RelicBootyPanel>().ChooseRelic(this);
        working = false;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!working) return;
        transform.DOScale(1.1f, .1f).SetUpdate(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!working) return;
        transform.DOScale(1, .1f).SetUpdate(true);
    }

   public void Choosen()
    {
        transform.DOScale(2,1).SetUpdate(true);
        GetComponent<Image>().enabled = false;
        relicIconImage.DOFade(0, 1);
        relicDetailText.DOFade(0, 1);
        relicNameText.DOFade(0, 1);
        Destroy(gameObject, 1);
        AudioManager.PlayClip("getRelic");
    }

}
