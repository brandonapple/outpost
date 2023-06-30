using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TalentDetailPanel : MonoBehaviour
{
    public static TalentDetailPanel instance;

    public Text nameText;
    public Text descriptionText;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        HidePanelFast();
    }

    public void ShowData(string _name,string _description)
    {
        nameText.text = _name;
        descriptionText.text = _description;

    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void ShowPanel()
    {
        transform.DOScale(1, .25f).SetUpdate(true);
    }
    public void HidePanel()
    {
        transform.DOScale(0, .25f).SetUpdate(true);
    }
    public void HidePanelFast()
    {
        transform.localScale = Vector3.zero;
    }
}
