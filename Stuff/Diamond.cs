using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Coin
{
    public override void AddValue()
    {
        //base.AddValue();
        CoinsManager.instance.AddDiamond(value);
    }
}
