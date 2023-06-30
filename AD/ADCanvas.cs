using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ADCanvas : MonoBehaviour
{
    public static ADCanvas instance;
    public Example example;

    public Text adChanceText;
    private void Awake()
    {
        instance = this;
        example = GetComponent<Example>();
    }
    private void Start()
    {
        Invoke(nameof(LoadFullScreenAD), .5f);

        adChance = -.5f;
    }
    public void LoadFullScreenAD()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            example.LoadFullScreenVideoAd();
        }
    }
    public void LoadRewardAD()
    {
        example.LoadRewardAd();
    }
    public void ShowFullScreenAD()
    {
        example.ShowFullScreenVideoAd();
        Invoke(nameof(LoadFullScreenAD), 5);


        adChance = 0;
        adChanceText.text = adChance.ToString();
    }
    public void LoadButtonClick()
    {
         example.ShowRewardAd();
         example.LoadFullScreenVideoAd();
    }

    public float adChance;
    public void WaveClean(int waveIndex)
    {
        return;

        if (waveIndex < 8)
        {
            adChance += .0f;
        }
        else if (waveIndex >= 8 && waveIndex < 16)
        {
            adChance += .1f;
        }
        else if (waveIndex >= 16)
        {
            adChance += .125f;
        }

        if (adChance >= 1)
        {
            adChance = 0;
            ShowFullScreenAD();
        }
        adChanceText.text = adChance.ToString();
    }
}
