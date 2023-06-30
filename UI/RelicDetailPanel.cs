using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RelicDetailPanel : MonoBehaviour
{
    public static RelicDetailPanel instance;

    string nameString;
    string detailString;

    string specialValueNameString;

    public Text relicNameText;
    public Text relicDetailText;
    private void Awake()
    {
        instance = this;
    }


    public void ShowRelicShowerDetail(string relicName,Vector3 pos)
    {
        RelicDataSO[] dataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        foreach (RelicDataSO so in dataSOs)
        {
            if(so.relicName == relicName)
            {
                if (OptionsManager.languageIndex==0)
                {
                    detailString = so.relicDescriptionEnglish;
                    nameString = so.relicName;
                }
                else
                {
                    detailString = so.relicDescriptionChinese;
                    nameString = so.relicNameChinese;
                }
                relicNameText.text = nameString;
                relicDetailText.text = detailString;



                specialValueNameString = so.specialValueNameString;
                if (so.specialValueNameString!=null)
                {
                    if (so.specialValueNameString!="")
                    {
                        
                        string _string = RelicManager.instance.GetSpecialValueByName(so.specialValueNameString);
                        if (so.percentFormat)
                        {
                           _string= (float.Parse(_string) * 100).ToString() + "%";
                        }

                        relicDetailText.text = relicDetailText.text
                          +"\n"
                          + LanguageManager.GetText("current")
                          + _string;

                    }
                }

              
            }
        }
        ShowPanel();
        transform.localPosition = pos + new Vector3(20, -20, 0);
    }

    public void ShowPanel()
    {
        transform.DOScale(Vector3.one, .15f).SetUpdate(true);
    }
    public void HidePanel()
    {
        transform.DOScale(Vector3.zero, .15f).SetUpdate(true);
    }
}
