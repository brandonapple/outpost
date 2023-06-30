using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseBuffSingle : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public SpriteRenderer textMeshContainer;
    public enum BaseBuffType
    {
        riskForDamage,
        moneyDorDamage,
        critChance,
    }
    public BaseBuffType thisBaseBuffType = BaseBuffType.riskForDamage;

    public void SetColor(Color _color) 
    {
        textMeshContainer.color = _color;
    }

    public void SetText(string _textString)
    {
        textMeshPro.text = _textString;
    }
}
