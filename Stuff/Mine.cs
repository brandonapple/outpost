using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
   public enum MineType
    {
        Diamond,
        Gold
    }
    public MineType thisMineType = MineType.Diamond;


    public static Mine goleMine;
    public static Mine diamondMine;

    private void Awake()
    {
        switch (thisMineType)
        {
            case MineType.Diamond:
                diamondMine = this;
                break;
            case MineType.Gold:
                goleMine = this;
                break;
        }
    }
}
