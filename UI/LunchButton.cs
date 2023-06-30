using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchButton : MonoBehaviour
{
    public static LunchButton instance;
    bool scaling;
    public void Awake()
    {
        instance = this;
        HideButtonFast();
    }

    public void ShowButton()
    {
        scaling = true;
        GetComponentInChildren<TextLoader>().GetText();

    }
    public void HideButton()
    {
        scaling = false;
    }
    public void HideButtonFast()
    {
        transform.localScale = Vector3.zero;
        scaling = false;
    }
    void Update()
    {
        if (scaling)
        {
            transform.localScale = Vector3.one * (1 + Mathf.Cos(Time.time *5) * .05f);
        }
        else
        {
            transform.localScale = Vector3.one * 0;
        }
    }
}
