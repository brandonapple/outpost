using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public Color monsterConfusedColor;
    private void Awake()
    {
        instance = this;
    }
}
