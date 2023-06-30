using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class RelicLibrarySingleDetailPanel : MonoBehaviour
{
    public static RelicLibrarySingleDetailPanel instance;
    public Text relicDescriptionText;
    private void Awake()
    {
        instance = this;
        HidePanelFast();
    }
    public void ShowPanel()
    {
        transform.DOScale(1, .15f).SetUpdate(true);
    }
    public void ShowPanel(string content,Vector3 pos)
    {
        ShowPanel();
        SetText(content);
        SetPostion(pos);
    }
    public void HidePanel()
    {
        transform.DOScale(0, .15f).SetUpdate(true);
    }
    public void HidePanelFast()
    {
        transform.localScale = Vector3.zero;
    }

    public void SetPostion(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetText(string textContent)
    {
        relicDescriptionText.text = textContent;
    }




}
