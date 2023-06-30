using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GetMineralValueText : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Color goldColor;
    public Color diamondColor;
    public enum MineralType
    {
        gold,diamond
    }
    public MineralType thisMineralType = MineralType.gold;
    private void Awake()
    {
       
    }
    private void OnEnable()
    {
        transform.position = Base.instance.transform.position;
        transform.DOMoveY(transform.position.y + 1, 1);
    }
    public void SetValue(int value,MineralType _mineralType)
    {
        textMeshPro.text ="+ "+ value.ToString();
        switch (_mineralType)
        {
            case MineralType.gold:
                textMeshPro.color = goldColor;
                break;
            case MineralType.diamond:
                textMeshPro.color = diamondColor;
                break;
            default:
                break;
        }
    }


}
