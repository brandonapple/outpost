using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaythroughManager : MonoBehaviour
{
    public static GamePlaythroughManager instance;
    public int playThroughTime;


    public bool miningModeAutoSpawnUnlocked;
    public bool mapBUnlocked;

    public bool weaponGroupsBUnlocked;
    public bool weaponGroupCUnlocked;
    public bool weaponGroupFreeUnlocked;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    [ContextMenu("Load data")]
    public void LoadData()
    {
        GamePlaythroughData data = SaveSystem.LoadGamePlaythroughData();
        if (data==null)
        {
            playThroughTime = 0;
        }
        else
        {
            playThroughTime = data.playThroughTime;
        }


        if (playThroughTime==0)
        {
            miningModeAutoSpawnUnlocked = false;
            mapBUnlocked = false;
            weaponGroupsBUnlocked = false;
            weaponGroupCUnlocked = false;
            weaponGroupFreeUnlocked = false;
        }
        else if (playThroughTime==1)
        {
            miningModeAutoSpawnUnlocked = false;
            mapBUnlocked = true;
            weaponGroupsBUnlocked = true;
            weaponGroupCUnlocked = false;
            weaponGroupFreeUnlocked = false;
        }
        else if (playThroughTime>=2)
        {
            weaponGroupsBUnlocked = true;
            weaponGroupCUnlocked = true;
            weaponGroupFreeUnlocked = true;
            miningModeAutoSpawnUnlocked = true;
            mapBUnlocked = true;
        }

       
    }
   
    public void SaveData()
    {
        SaveSystem.SaveGameThroughData();
    }

    public void AddGamePlayThroughTime()
    {
        playThroughTime++;
        SaveData();
    }
   
    [ContextMenu("reset data")]
    public void ResetData()
    {
        playThroughTime = 0;
        SaveData();
    }
}
