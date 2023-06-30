using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipDetailContent : MonoBehaviour
{
    public string tipOnPC;
    public string tipOnPCEn;

    public string tipOnMobile;

    public Text tipText;
    private void Awake()
    {
        tipText = GetComponentInChildren<Text>();
    }
    public void ShowOnMobile()
    {
        tipText.text = tipOnMobile;
    }
    public void ShowOnPC()
    {
        tipText.text = tipOnPC;
        if (OptionsManager.languageIndex ==0)
        {
            tipText.text = tipOnPCEn;
        }
    }

}
