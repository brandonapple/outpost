using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdditionalDiamondsButton : MonoBehaviour
{
    public static AdditionalDiamondsButton instance;
    public int diamondCount;
    TextMeshPro diamondCountText;
    bool show;
    private void Awake()
    {
        instance = this;
        diamondCountText = GetComponentInChildren<TextMeshPro>();
    }
    private void Start()
    {
        HideFast();
    }
    void HideFast()
    {
        transform.localScale = Vector3.zero;
        show = false;
    }
    private void OnMouseDown()
    {
        HideFast();
        ADCanvas.instance.ShowFullScreenAD();
        CoinsManager.instance.MoveAdditionalDiamondsToMotherShip(diamondCount);
    }
    public void Show()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        diamondCount = (int)(CoinsManager.instance.diamondCount * .3f);
        if (diamondCount <= 3)
        {
            diamondCount = 3;
        }
        diamondCountText.text ="+ "+ diamondCount.ToString();
        transform.localScale = Vector3.one;
        show = true;
    }
    private void Update()
    {
        if (show)
        {
            transform.localScale = Vector3.one * (1 + Mathf.Sin(Time.time *5) * .05f);
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }
}
