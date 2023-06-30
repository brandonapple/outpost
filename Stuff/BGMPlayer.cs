using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer instance;
    bool bgmOn;
    public AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        LoadData();
    }
   
    public void LoadData()
    {
        bgmOn = OptionsManager.bgmOn;
        audioSource.enabled = bgmOn;
        if (bgmOn && GameManager.instance.thisGameState == GameManager.GameState.playing)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void GetBGM()
    {
        if (DataManager.instance.miningModeIndex==0)
        {
            audioSource.clip = Resources.Load<AudioClip>("Clips/outpost2");
        }
        else 
        {
            audioSource.clip = Resources.Load<AudioClip>("Clips/outpost2fast");
        }
    }
}
