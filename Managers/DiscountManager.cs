using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountManager : MonoBehaviour
{
    public static DiscountManager instance;
    public bool thisWaveHalfPrice;
    private void Awake()
    {
        instance = this;
    }


    public void WaveBegin()
    {

    }
    public void WaveEnd()
    {
        if (thisWaveHalfPrice)
        {
            thisWaveHalfPrice = false;
            UpgradeRootPanel.instance.RefreshCurrentContainerList();
        }

    }

    public void ThisWaveHalfPrice()
    {
        thisWaveHalfPrice = true;
        UpgradeRootPanel.instance.RefreshCurrentContainerList();
    }
}
