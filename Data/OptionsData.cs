using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OptionsData 
{
    public bool bgmOn;
    public bool damageValueTextOn;
    public bool effectOn;
    public bool camShakeOn;
    public float audioVolumeValue;
    public float viewRangeValue;
    public int languageIndex;

    public OptionsData()
    {
        bgmOn = OptionsManager.bgmOn;
        damageValueTextOn = OptionsManager.damageValueTextOn;
        effectOn = OptionsManager.effectOn;
        camShakeOn = OptionsManager.camShakeOn;

        audioVolumeValue = OptionsManager.audioVolumeValue;
        viewRangeValue = OptionsManager.viewRangeValue;
        languageIndex = OptionsManager.languageIndex;
    }


}
